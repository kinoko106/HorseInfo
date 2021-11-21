using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HorseInfoCore;

namespace HorseInfoCoreTest
{
	class Program
	{
		// 土日の23:00ぐらいに新規レース結果取ってくるプログラム
		// 過去レースID、レース情報取ってくるプログラム
		// 馬リスト取ってくるプログラム
		// 馬データ取ってくるプログラム

		// 流れは全部
		// スクレイピングでid探索
		// スクレイピングで明細取得→json吐き出し
		//
		static void Main(string[] args)
		{
			var getter = new KeibaDataGetter();
			Task.Run(async () =>
			{
				await getter.GetRaceInfo(2021, 2021);
				await getter.OutputJson("races");
			});
			while (!getter.Finished)
			{

			}
			getter.Finished = false;

			var readedRaces = new List<Race>();
			Task.Run(async () =>
			{
				readedRaces = await getter.ReadJson("races");
			});
			
			while (!getter.Finished)
			{

			}
			return;
		}
	}
}
