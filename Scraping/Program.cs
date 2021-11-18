using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace Scraping
{
	class Program
	{
		static void Main(string[] args)
		{

			var scpramer = new Scraper("https://www.netkeiba.com/");
			var keibaScraper = new KeibaScraper("https://www.netkeiba.com/");

			Task.Run(async () =>
			{
				await scpramer.GetHtmlDocument();
				Console.WriteLine("Title : " + scpramer.Document.Title);

				var document = await keibaScraper.GetLargePrizeRaceScaduleDocment();
				Console.WriteLine("Title : " + document.Title);
				var scheduleDocument = document.GetElementsByClassName("Race_Name Txt_L");
				var raceURLs = scheduleDocument.Where(x => x.LastElementChild != null).Select(x => {
					return (x.LastElementChild.GetAttribute("Href"), x.LastElementChild.TextContent);
				}).to();
				//foreach (var doc in scheduleDocument)		
				//{
				//	doc.
				//}

			});
			Console.ReadKey();
		}
	}
}
