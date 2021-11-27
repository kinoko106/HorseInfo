using System;
using System.Collections.Generic;
using System.Text;

namespace HorseInfoCore
{
	/// <summary>
	/// 重賞レース一覧の1行分データ
	/// </summary>
	public class PatternRace
	{
		public PatternRace()
		{
			
		}
		/// <summary>
		/// レース日
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// レース名
		/// </summary>
		public string RaceName { get; set; }
		/// <summary>
		/// 特集ページId
		/// </summary>
		public string SpecialRacePageId { get; set; }
		/// <summary>
		/// グレード
		/// </summary>
		public RaceGrade RaceGrade { get; set; }
		/// <summary>
		/// コースID
		/// </summary>
		public Cource Cource { get; set; }
		/// コース名
		/// </summary>
		public string CourceName { get; set; }
		/// <summary>
		/// 距離 m
		/// </summary>
		public int Distance { get; set; }
		/// <summary>
		/// 条件
		/// </summary>
		public string RaceLimit { get; set; }
		/// <summary>
		/// 重量の条件タイプ
		/// </summary>
		public string HandicapType { get; set; }
	}
}
