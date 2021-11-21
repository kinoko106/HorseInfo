using System;
using System.Collections.Generic;
using System.Text;

namespace HorseInfoCore
{
	public class RaceResult
	{
		/// <summary>
		/// 順位
		/// </summary>
		public int Rank { get; set; }
		/// <summary>
		/// 枠番
		/// </summary>
		public int BracketNumber { get; set; }
		/// <summary>
		/// 馬番
		/// </summary>
		public int HorseNumber { get; set; }
		/// <summary>
		/// 馬名
		/// </summary>
		public string HorseName { get; set; }
		/// <summary>
		/// 牡or牝
		/// </summary>
		public string HorseGender { get; set; }
		/// <summary>
		/// 馬齢
		/// </summary>
		public int HorseAge { get; set; }
		
		/// <summary>
		/// 斤量
		/// </summary>
		public double HandicapWeight { get; set; }
		/// <summary>
		/// 騎手ID
		/// </summary>
		public string JockeyId { get; set; }
		/// <summary>
		/// Time
		/// </summary>
		public TimeSpan Time { get; set; }
		/// <summary>
		/// 着差 ハナ：0.01 アタマ：0.1 クビ：0.2 0.25ごとに馬身差
		/// </summary>
		public double Margin { get; set; }
		/// <summary>
		/// コーナーごとの順位 2桁埋めで表記
		/// 9-11-8-8 → 09110808
		/// </summary>
		public string CornerRank { get; set; }
		/// <summary>
		/// 上がり3ハロンの秒数
		/// </summary>
		public double ThreeFurlongSeconds { get; set; }
		/// <summary>
		/// 単勝倍率
		/// </summary>
		public double Odds { get; set; }
		/// <summary>
		/// 人気
		/// </summary>
		public int PopularityNumber { get; set; }
		/// <summary>
		/// 馬体重
		/// </summary>
		public int HorseWeight { get; set; }
		/// <summary>
		/// 馬体重
		/// </summary>
		public int HorseWeightDifference { get; set; }
		/// <summary>
		/// 馬ID
		/// </summary>
		public string HorseId { get; set; }
	}
}
