using System;
using System.Collections.Generic;
using System.Text;

namespace HorseInfoCore
{
	public static class EnumUtility
	{
		public static Cource ToCource(int inCourceId)
		{
			return (Cource)Enum.Parse(typeof(Cource), inCourceId.ToString());
		}

		public static Cource ToCource(string inCourceName)
		{
			var cource = Cource.Hakodate;

			switch (inCourceName)
			{
				case "札幌":
				case "Sapporo":
					cource = Cource.Sapporo;
					break;
				case "函館":
				case "HakoDate":
					cource = Cource.Hakodate;
					break;
				case "福島":
				case "Hukushima":
					cource = Cource.Hukushima;
					break;
				case "新潟":
				case "Nigata":
					cource = Cource.Nigata;
					break;
				case "東京":
				case "Tokyo":
					cource = Cource.Tokyo;
					break;
				case "中山":
				case "Nakayama":
					cource = Cource.Nakayama;
					break;
				case "中京":
				case "Tyukyo":
					cource = Cource.Tyukyo;
					break;
				case "京都":
				case "Kyoto":
					cource = Cource.Kyoto;
					break;
				case "阪神":
				case "Hanshin":
					cource = Cource.Hanshin;
					break;
				case "小倉":
				case "Kokura":
					cource = Cource.Kokura;
					break;
			}

			return cource;
		}

		public static CourceType ToCourceType(string inCourceTypeString)
		{
			var type = CourceType.Tarf;
			if(inCourceTypeString == "ダ")
			{
				type = CourceType.Dart;
			}
			return type;
		}

		public static CourseCondition ToCourseCondition(string inCourseConditionString)
		{
			var condition = CourseCondition.Good;
			switch (inCourseConditionString)
			{
				case "稍重":
					condition = CourseCondition.LittleHeavy;
					break;
				case "重":
					condition = CourseCondition.Heavy;
					break;
				case "不":
					condition = CourseCondition.Bad;
					break;
			}
			return condition;
		}

		public static Weather ToWeather(string inWeatherString)
		{
			var weatherString = inWeatherString.Replace(" ", "").Substring(0, 2);
			var weather = Weather.Sunny;
			switch (weatherString)
			{
				case "曇":
					weather = Weather.Croud;
					break;
				case "小雨":
					weather = Weather.LlightRain;
					break;
				case "雨":
					weather = Weather.Rain;
					break;
				case "雪":
					weather = Weather.Snow;
					break;
			}
			return weather;
		}
	}
}
