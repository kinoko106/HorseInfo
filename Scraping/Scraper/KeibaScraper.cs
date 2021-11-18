using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Scraping;

namespace Scraping
{
	public class KeibaScraper : Scraper
	{
		public KeibaScraper(string inUrl) : base(inUrl)
		{
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

		// YYYYNNWWXXRR
		// YYYY 年
		// NN   レース場
		// WW   第何週
		// XX   何日目(週内)
		// RR   ラウンド

		public async Task<IHtmlDocument> GetRacePage(string inRaceId)
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
	}
}
