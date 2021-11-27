using AngleSharp.Html.Dom;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections.Concurrent;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace HorseInfoCore
{
	/// <summary>
	/// 手当たり次第にデータ取ってきて持っておくクラス
	/// </summary>
	public class KeibaDataGetter
	{
		public bool Finished { get; set; } = false;
		private const string HorseDBUrl = "";
		private string RaceURLBase { get; set; }
		private Scraper Scraper { get; set; }
		private Dictionary<string, Dictionary<string, Dictionary<string, BlockingCollection<Race>>>> BlockRaces { get; set; }

		public KeibaDataGetter(string inRaceURLBase)
		{
			Scraper = new Scraper();
			RaceURLBase = inRaceURLBase;
			BlockRaces = new Dictionary<string, Dictionary<string, Dictionary<string, BlockingCollection<Race>>>>();
		}

		/// <summary>
		/// 年単位で
		/// 必要なページから手あたり次第データを取ってくる
		/// </summary>
		public async Task GetRaceInfo(
			int inYearFrom,
			int inYearTo)
		{
			// 年
			for (int year = inYearFrom; year <= inYearTo; year++)
			{
				// レース場
				for (int cource = 1; cource <= 10; cource++)
				{
					// 週　1-6ぐらい
					for (int week = 1; week <= 1; week++)
					{
						Console.WriteLine("Year:" + year.ToString("D2") + " CourceId:" + cource.ToString("D2") + " week:" + week.ToString("D2"));
						// 日 1-10ぐらい
						for (int day = 1; day <= 3; day++)
						{
							for (int round = 1; round <= 2; round++)
							{
								var yearId = year.ToString();
								var courceId = cource.ToString("D2");
								var weekId = week.ToString("D2");
								var raceId = yearId + courceId + weekId + day.ToString("D2") + round.ToString("D2");
								var race = await GetOneRaceInfo(raceId, cource);

								await Task.Delay(1000);
								if (race == null)
								{
									break;
								}

								if(!BlockRaces.TryGetValue(yearId, out var raceToCource))
								{
									raceToCource = new Dictionary<string, Dictionary<string, BlockingCollection<Race>>>();
									var raceToWeek = new Dictionary<string, BlockingCollection<Race>>();
									var races = new BlockingCollection<Race>();

									races.Add(race);
									raceToWeek.Add(weekId, races);
									raceToCource.Add(courceId, raceToWeek);
									BlockRaces.Add(yearId, raceToCource);
								}
								else
								{
									if (!BlockRaces[yearId].TryGetValue(courceId, out var raceToWeek))
									{
										raceToWeek = new Dictionary<string, BlockingCollection<Race>>();
										var races = new BlockingCollection<Race>();
										races.Add(race);
										raceToWeek.Add(weekId, races);
										raceToCource.Add(courceId, raceToWeek);
									}
									else
									{
										if (!BlockRaces[yearId][courceId].TryGetValue(weekId, out var races))
										{
											races = new BlockingCollection<Race>();
											races.Add(race);
											raceToCource[courceId].Add(weekId, races);
										}
										else
										{
											BlockRaces[yearId][courceId][weekId].Add(race);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		public async Task<Race> GetOneRaceInfo(string inRaceId, int inCourceId)
		{
			var document = await Scraper.GetHtmlDocument(RaceURLBase + inRaceId);
			if ((document.GetElementsByClassName("mainrace_data fc")?.Length ?? 0) < 1)
			{
				return null;
			}

			var race = new Race();
			try
			{
				race = AnalysisRaceData(document, inRaceId, inCourceId);
			}
			catch(Exception ex)
			{
				AnalysisRaceData(document, inRaceId, inCourceId);
				race = null;
			}

			return race;
		}

		/// <summary>
		/// 
		/// </summary>
		public Race AnalysisRaceData(IHtmlDocument inHtmlDocument, string inRaceId, int inCourceId)
		{
			var race = new Race();
			race.RaceId = inRaceId;
			// レース場、レース条件
			var raceInfoDocument = inHtmlDocument.GetElementsByClassName("racedata fc");
			var raceName = raceInfoDocument[0].Children[1].Children[0].TextContent;
			var raceInfos = raceInfoDocument[0].Children[1].Children[1].Children[0].Children[0].TextContent;
			var raceInfoParts = raceInfos.Split("/");
			race.Course = EnumUtility.ToCource(inCourceId);
			race.CourseType = EnumUtility.ToCourceType(raceInfoParts[0].Substring(0, 1));
			race.Weather = EnumUtility.ToWeather(raceInfoParts[1].Split(":")[1]);
			race.CourseCondition = EnumUtility.ToCourseCondition(raceInfoParts[2].Split(":")[1]);
			// 着順等
			var raceResultDocument = inHtmlDocument.GetElementsByClassName("race_table_01 nk_tb_common");
			foreach (var horseResults in raceResultDocument[0].Children[0].Children.Skip(1))
			{
				// 出走取消は除外
				if(!int.TryParse(horseResults.Children[0].TextContent, System.Globalization.NumberStyles.Integer, null, out var rankNumber))
				{
					continue;
				}

				//var rank = rankNumber;
				//var bracketNumber = int.Parse(horseResults.Children[1].TextContent);
				//var horseNumber = int.Parse(horseResults.Children[2].TextContent);
				//var horseId = horseResults.Children[3].Children[0].GetAttribute("href").ToString().Replace("/", "").Replace("horse", "");
				//var horseGender = horseResults.Children[4].TextContent.Substring(0, 1);
				//var horseAge = int.Parse(horseResults.Children[4].TextContent.Substring(1, 1));
				//var handicapWeight = double.Parse(horseResults.Children[5].TextContent);
				//var jockeyId = horseResults.Children[6].Children[0].GetAttribute("href").ToString().Replace("/", "").Replace("jockey", "");
				//var time = ParseToTimeSpan(horseResults.Children[7].TextContent);
				//var margin = ParseToMargin(horseResults.Children[8].TextContent);
				//var cornerRank = horseResults.Children[10].TextContent;
				//var threeFurlongSeconds = double.Parse(horseResults.Children[11].TextContent);
				//var odds = double.Parse(horseResults.Children[12].TextContent);
				//var popularityNumber = int.Parse(horseResults.Children[13].TextContent);
				//var horseWeight = int.Parse(horseResults.Children[14].TextContent.Split("(")[0]);
				//var horseWeightDifference = int.Parse(horseResults.Children[14].TextContent.Split("(")[1].Replace("(", "").Replace(")", ""));

				var raceResult = new RaceResult()
				{
					Rank = int.Parse(horseResults.Children[0].TextContent),
					BracketNumber = int.Parse(horseResults.Children[1].TextContent),
					HorseNumber = int.Parse(horseResults.Children[2].TextContent),
					HorseName = horseResults.Children[3].TextContent.Replace("\n", ""),
					HorseId = horseResults.Children[3].Children[0].GetAttribute("href").ToString().Replace("/","").Replace("horse", ""),
					HorseGender = horseResults.Children[4].TextContent.Substring(0, 1),
					HorseAge = int.Parse(horseResults.Children[4].TextContent.Substring(1, 1)),
					HandicapWeight = double.Parse(horseResults.Children[5].TextContent),
					JockeyId = horseResults.Children[6].Children[0].GetAttribute("href").ToString().Replace("/", "").Replace("jockey", ""),
					Time = ParseToTimeSpan(horseResults.Children[7].TextContent),
					Margin = ParseToMargin(horseResults.Children[8].TextContent),
					CornerRank = horseResults.Children[10].TextContent,
					ThreeFurlongSeconds = double.Parse(horseResults.Children[11].TextContent),
					Odds = double.Parse(horseResults.Children[12].TextContent),
					PopularityNumber = int.Parse(horseResults.Children[13].TextContent),
					HorseWeight = int.Parse(horseResults.Children[14].TextContent.Split("(")[0]),
					HorseWeightDifference = int.Parse(horseResults.Children[14].TextContent.Split("(")[1].Replace("(","").Replace(")", ""))
				};
				race.RaceResults.Add(raceResult);
			}
			return race;
		}

		private TimeSpan ParseToTimeSpan(string inTime)
		{
			if (string.IsNullOrEmpty(inTime))
			{
				return new TimeSpan(99, 99, 99, 99, 99);
			}
			int minte = int.Parse(inTime.Split(":")[0]);
			int second = int.Parse(inTime.Split(":")[1].Substring(0,2));
			int milSecond = int.Parse(inTime.Split(".")[1]);
			return new TimeSpan(0, 0, minte, second, milSecond);
		}

		private double ParseToMargin(string inMargin)
		{
			double margin = 0.0d;

			if (inMargin == "ハナ")
			{
				margin = 0.01d;
			}
			else if (inMargin == "アタマ")
			{
				margin = 0.1d;
			}
			else if (inMargin == "クビ")
			{
				margin = 0.2d;
			}
			else if (inMargin == "大") // 大差
			{
				margin = 99d;
			}
			else
			{
				if (inMargin.Length == 5)
				{
					// 1.1/2 とか
					var disance = inMargin.Split(".");
					var n = double.Parse(disance[0]);
					var o = double.Parse(disance[1].Substring(0, 1));
					var p = double.Parse(disance[1].Substring(2, 1));
					margin = n + o / p;
				}
				else if (inMargin.Length == 3)
				{
					// 1/2 とか
					var o = double.Parse(inMargin.Substring(0, 1));
					var p = double.Parse(inMargin.Substring(2, 1));
					margin = o / p;
				}
				else if (inMargin.Length == 1)
				{
					// 1 とか
					margin = double.Parse(inMargin);
				}
			}
			return margin;
		}

		public async Task OutputJson(string inFileName)
		{
			var options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
				WriteIndented = true
			};
			foreach(var racesToYear in BlockRaces)
			{
				var year = racesToYear.Key;
				foreach (var racesToCource in racesToYear.Value)
				{
					var cource = racesToCource.Key;
					foreach (var racesToWeek in racesToCource.Value)
					{
						var week = racesToWeek.Key;
						var name = inFileName.Split(".");
						var fileName = name[0] + "_" + year + cource + week + "." + name[1];

						using (FileStream createStream = File.Create(@"race\" + fileName))
						{
							await JsonSerializer.SerializeAsync(createStream, racesToWeek.Value.ToArray(), options);
							await createStream.DisposeAsync();
						}
					}
				}
			}
			Finished = true;
		}

		public async Task<List<Race>> ReadJson(string inFileName)
		{
			var races = new List<Race>();
			using (FileStream openStream = File.OpenRead(inFileName))
			{
				try
				{
					races = await JsonSerializer.DeserializeAsync<List<Race>>(openStream);
				}
				catch(Exception e)
				{
					Console.WriteLine(e);
				}
			}
			Finished = true;
			return races;
		}
	}
}
