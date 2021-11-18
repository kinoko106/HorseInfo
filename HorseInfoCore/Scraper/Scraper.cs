using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace HorseInfoCore
{
	public class Scraper
	{
		public Scraper()
		{
			
		}

		public async Task<IHtmlDocument> GetHtmlDocument(string inUrl)
		{
			var document = default(IHtmlDocument);
			using (var client = new HttpClient())
			using (var stream = await client.GetStreamAsync(new Uri(inUrl)))
			{
				var parser = new HtmlParser();
				document = await parser.ParseDocumentAsync(stream);
			}

			return document;
		}
	}
}
