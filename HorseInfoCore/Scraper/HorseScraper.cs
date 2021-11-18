using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace HorseInfoCore
{
	public class HorseScraper : Scraper
	{
		public Dictionary<string, string> Races { get; set; }
		public HorseScraper()
		{
			Races = new Dictionary<string, string>();
		}

		// 馬ID
		// YYYY??NNNNNN
		// YYYY 年
		// ?? わからない 10固定？
		// NNNNNN 連番っぽい 7000ぐらい
		
		// レースID
		// YYYYNNWWXXRR
		// YYYY 年
		// NN   レース場
		// WW   第何週
		// XX   何日目(週内)
		// RR   ラウンド

		/// <summary>
		/// 必要なページから手あたり次第データを取ってくる
		/// </summary>
		public async Task GetOlderRaceIds(
			int inYearFrom,
			int inYearTo)
		{
			// レース情報
			var baseUrl = "https://db.netkeiba.com/race/";
			var tasks = new List<Task>();
			// 年
			for(int y = inYearFrom; y <= inYearTo; y++)
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
							var url = baseUrl + raceId;

							Console.WriteLine("target : " + raceId);
							var document = await GetRacePage(raceId);
							await Task.Delay(1000);
							if ((document.GetElementsByClassName("race_place")?.Length ?? 0) < 1)
							{
								break;
							}
							Console.WriteLine("Title : " + document.Title);
							Races.Add(raceId, document.Title);
						}
					}
				}
			}
		}

		public async Task<IHtmlDocument> GetLargePrizeRaceScaduleDocment()
		{
			var document = default(IHtmlDocument);
			using (var client = new HttpClient())
			using (var stream = await client.GetStreamAsync(new Uri("https://race.netkeiba.com/top/schedule.html")))
			{
				var parser = new HtmlParser();
				document = await parser.ParseDocumentAsync(stream);
			}

			return document;
		}



		public async Task<IHtmlDocument> GetRacePage(string inRaceId)
		{
			var document = default(IHtmlDocument);
			using (var client = new HttpClient())
			using (var stream = await client.GetStreamAsync(new Uri("https://db.netkeiba.com/race/" + inRaceId)))
			{
				var parser = new HtmlParser();
				document = await parser.ParseDocumentAsync(stream);
			}

			return document;
		}
	}
}
