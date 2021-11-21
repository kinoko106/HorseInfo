using System;
using System.Configuration;
using System.Threading.Tasks;
using HorseInfoCore;

namespace RaceInfoGetter
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Read Configuration");
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			string baseURL = "";
			var outputFileName = "";
			var startYear = 1990;
			var finishYear = 1990;
			try
			{
				baseURL = ConfigurationManager.AppSettings["baseURL"];
				outputFileName = ConfigurationManager.AppSettings["outputFileName"];
				startYear = int.Parse(ConfigurationManager.AppSettings["startYear"]);
				finishYear = int.Parse(ConfigurationManager.AppSettings["finishYear"]);
			}
			catch(Exception ex)
			{
				Console.WriteLine("Configuration error");
				return;
			}
			Console.WriteLine("Read Configuration Finished");

			var getter = new KeibaDataGetter(baseURL);
			Task.Run(async () =>
			{
				await getter.GetRaceInfo(startYear, finishYear);
				await getter.OutputJson(outputFileName);

				// await getter.GetOneRaceInfo("202102010311", "01");
			});
			while (!getter.Finished)
			{

			}

			return;
		}
	}
}
