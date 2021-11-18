using System;
using System.Collections.Generic;
using System.Text;

namespace HorseInfoCore
{
	public class Race
	{
		public Race()
		{
			RaceResults = new List<RaceResult>();
		}
		/// <summary>
		/// レース場
		/// </summary>
		public Cource Course { get; set; }
		/// <summary>
		/// 馬場 芝orダート
		/// </summary>
		public CourceType CourseType { get; set; }
		/// <summary>
		/// 天候 晴れ曇雨
		/// </summary>
		public Weather Weather { get; set; }
		/// <summary>
		/// 距離 m
		/// </summary>
		public int Distance { get; set; }
		/// <summary>
		/// 馬場状態
		/// </summary>
		public CourseCondition CourseCondition { get; set; }
		/// <summary>
		/// 出走時刻
		/// </summary>
		public DateTime RaceStartTime { get; set; }
		/// <summary>
		/// 着
		/// </summary>
		public List<RaceResult> RaceResults { get; set; }

		/// <summary>
		/// 上がり3ハロン最速タイム
		/// </summary>
		public double TopFurlong { get; set; }

	}
}
