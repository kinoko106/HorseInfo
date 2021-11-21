using System;
using System.Collections.Generic;
using System.Text;

namespace HorseInfoCore
{
	public static class EnumUtility
	{
		public static Cource ToCource(string inCourceString)
		{
			return (Cource)Enum.Parse(typeof(Cource), inCourceString);
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
