using AngleSharp.Html.Dom;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HorseInfoCore
{
	/// <summary>
	/// 手当たり次第にデータ取ってきて持っておくクラス
	/// </summary>
	public class KeibaDataGetter
	{
		private const string HorseDBUrl = "";
		private HorseScraper HorseScraper { get; set; }

		public KeibaDataGetter()
		{
			HorseScraper = new HorseScraper();
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
			for (int y = inYearFrom; y <= inYearTo; y++)
			{
				// レース場
				for (int g = 1; g <= 1; g++)
				{
					// 週　1-6ぐらい
					for (int w = 1; w <= 1; w++)
					{
						// 日 1-10ぐらい
						for (int d = 1; d <= 10; d++)
						{
							var raceId = y.ToString() + g.ToString("D2") + w.ToString("D2") + w.ToString("D2") + d.ToString("D2");

							Console.WriteLine("target : " + raceId);
							var document = await HorseScraper.GetRacePage(raceId);
							if ((document.GetElementsByClassName("race_place")?.Length ?? 0) < 1)
							{
								break;
							}
							await Task.Delay(1000);
						}
					}
				}
			}
		}

		public async Task GetOneRaceInfo(string inRaceId, string inCourceString)
		{
			var document = await HorseScraper.GetRacePage(inRaceId);
			if ((document.GetElementsByClassName("race_place")?.Length ?? 0) < 1)
			{
				return;
			}
			AnalysisRaceData(document, inCourceString);
		}

		/// <summary>
		/// 
		/// </summary>
		public Race AnalysisRaceData(IHtmlDocument inHtmlDocument, string inCourceString)
		{
			var race = new Race();
			// レース場、レース条件
			var raceInfoDocument = inHtmlDocument.GetElementsByClassName("racedata fc");
			var raceName = raceInfoDocument[0].Children[1].Children[0].TextContent;
			var raceInfos = raceInfoDocument[0].Children[1].Children[1].Children[0].Children[0].TextContent;
			var raceInfoParts = raceInfos.Split("/");
			race.Course = EnumUtility.ToCource(inCourceString);
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
				
				var rank = rankNumber;
				var	bracketNumber = int.Parse(horseResults.Children[1].TextContent);
				var	horseNumber = int.Parse(horseResults.Children[2].TextContent);
				var	horseId = horseResults.Children[3].Children[0].GetAttribute("href").ToString().Replace("/", "").Replace("horse", "");
				var	horseGender = horseResults.Children[4].TextContent.Substring(0, 1);
				var horseAge = int.Parse(horseResults.Children[4].TextContent.Substring(1, 1));
				var handicapWeight = int.Parse(horseResults.Children[5].TextContent);
				var jockeyId = horseResults.Children[6].Children[0].GetAttribute("href").ToString().Replace("/", "").Replace("jockey", "");
				var time = ParseToTimeSpan(horseResults.Children[7].TextContent);
				var margin = ParseToMargin(horseResults.Children[8].TextContent);
				var cornerRank = horseResults.Children[10].TextContent;
				var	threeFurlongSeconds = double.Parse(horseResults.Children[11].TextContent);
				var	odds = double.Parse(horseResults.Children[12].TextContent);
				var	popularityNumber = int.Parse(horseResults.Children[13].TextContent);
				var	horseWeight = int.Parse(horseResults.Children[14].TextContent.Split("(")[0]);
				var horseWeightDifference = int.Parse(horseResults.Children[14].TextContent.Split("(")[1].Replace("(", "").Replace(")", ""));

				var raceResult = new RaceResult()
				{
					Rank = int.Parse(horseResults.Children[0].TextContent),
					BracketNumber = int.Parse(horseResults.Children[1].TextContent),
					HorseNumber = int.Parse(horseResults.Children[2].TextContent),
					HorseId = horseResults.Children[3].Children[0].GetAttribute("href").ToString().Replace("/","").Replace("horse", ""),
					HorseGender = horseResults.Children[4].TextContent.Substring(0, 1),
					HorseAge = int.Parse(horseResults.Children[4].TextContent.Substring(1, 1)),
					HandicapWeight = int.Parse(horseResults.Children[5].TextContent),
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

	}
}
