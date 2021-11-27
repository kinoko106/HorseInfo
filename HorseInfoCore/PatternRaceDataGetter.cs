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
	public class PatternRaceDataGetter
	{
		public bool Finished { get; set; } = false;
		private string URL{ get; set; }
		private Scraper Scraper { get; set; }

		public PatternRaceDataGetter(string inRaceURLBase)
		{
			Scraper = new Scraper();
			URL = inRaceURLBase;
		}

		public async Task<List<PatternRace>> GetPatternRaceList()
		{
			var document = await Scraper.GetHtmlDocument(URL);
			//if ((document.GetElementsByClassName("mainrace_data fc")?.Length ?? 0) < 1)
			//{
			//	return null;
			//}

			var patternRace = new List<PatternRace>();
			try
			{
				patternRace = AnalysisPatternRaceData(document);
			}
			catch (Exception ex)
			{
				AnalysisPatternRaceData(document);
				patternRace = null;
			}

			return patternRace;
		}

		/// <summary>
		/// HtmlDocumentを必要なデータ形式に変換
		/// </summary>
		public List<PatternRace> AnalysisPatternRaceData(IHtmlDocument inHtmlDocument)
		{
			var patternRaces = new List<PatternRace>();
			patternRaces.Add(new PatternRace()
			{
				Cource = Cource.Hakodate,
				CourceName = "函館",
				Date = new DateTime(2021,1,1),
				Distance = 2000,
				HandicapType = "",
				RaceGrade = RaceGrade.G1,
				RaceLimit = "",
				RaceName  = "テストレース",
				SpecialRacePageId = "test"
			});
			return patternRaces;
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
	}
}
