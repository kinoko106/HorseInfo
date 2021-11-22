using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Livet;

namespace HorseInfo
{
	public class PatternRaceListPageViewModel : ViewModel
	{
		public ObservableCollection<(int Year,string YearString)> YearItems = new ObservableCollection<(int Year, string YearString)>();

		public PatternRaceListPageViewModel()
		{
			// 2002 ～ 今年までのドロップダウン作る
			for(int y = 2002; y < DateTime.Today.Year; y++)
			{
				YearItems.Add((y, y.ToString() + "年"));
			}
		}
	}
}
