using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HorseInfoCore;

namespace HorseInfo
{
	/// <summary>
	/// jsonから読みだしたデータ持っておくクラス
	/// </summary>
	public sealed class KeibaDataManager
	{
		private static KeibaDataManager instance = new KeibaDataManager();

		public static KeibaDataManager Instance
		{
			get
			{
				return instance;
			}
		}

		public List<Race> Races { get; set; }
		public bool IsReaded { get; set; } = false;

		private KeibaDataManager()
		{
			Races = new List<Race>();
			Task.Run(ReadJson);
		}

		public async Task ReadJson()
		{
			IsReaded = false;
			var fileNames = Directory.GetFiles("race");
			foreach(var fileName in fileNames)
			{
				using (FileStream openStream = File.OpenRead(fileName))
				{
					var races = await JsonSerializer.DeserializeAsync<List<Race>>(openStream);
					Races.AddRange(races);
				}
			}
			IsReaded = true;
		}
	}
}
