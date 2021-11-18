using System;
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
			Task.Run(async () => {
				var getter = new KeibaDataGetter();
				await getter.GetOneRaceInfo("201005030410", "Tokyo");

			});
			Console.ReadKey();
		}
	}
}
