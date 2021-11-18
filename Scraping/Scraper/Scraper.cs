using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace Scraping
{
	public class Scraper
	{
		public Scraper(string inUrl)
		{
			Url = inUrl;
			Document = default(IHtmlDocument);
		}

		public string Url { get; set; }

		public IHtmlDocument Document { get; set; }

		public async Task GetHtmlDocument()
		{
			using (var client = new HttpClient())
			using (var stream = await client.GetStreamAsync(new Uri(Url)))
			{
				var parser = new HtmlParser();
				Document = await parser.ParseDocumentAsync(stream);
			}
		}
	}
}
