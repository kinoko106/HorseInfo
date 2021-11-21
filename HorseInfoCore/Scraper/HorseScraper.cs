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
		public string RaceURLBase { get; set; }
		public HorseScraper(string inRaceURLBase)
		{
			RaceURLBase = inRaceURLBase;
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
			using (var stream = await client.GetStreamAsync(new Uri(RaceURLBase + inRaceId)))
			{
				var parser = new HtmlParser();
				document = await parser.ParseDocumentAsync(stream);
			}

			return document;
		}
	}
}
