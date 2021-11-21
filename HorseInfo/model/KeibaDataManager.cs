using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HorseInfoCore;

namespace HorseInfo.model
{
	/// <summary>
	/// jsonから読みだしたデータ持っておくクラス
	/// </summary>
	public class KeibaDataManager
	{
		public async Task ReadJson(string inFileName)
		{
			string fileName = "races.json";
			using (FileStream openStream = File.OpenRead(fileName))
			{
				var races = await JsonSerializer.DeserializeAsync<List<Race>>(openStream);
			}	
		}
	}
}
