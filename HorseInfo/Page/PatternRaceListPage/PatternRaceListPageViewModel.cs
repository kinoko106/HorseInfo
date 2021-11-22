using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Livet;

namespace HorseInfo
{
	public class PatternRaceListPageViewModel : PageViewModelBase
	{
		#region Properties

		private ObservableCollection<YearItem> _YearItems = new ObservableCollection<YearItem>();
		public ObservableCollection<YearItem> YearItems
		{
			get
			{ return _YearItems; }
			set
			{
				if (_YearItems == value)
					return;
				_YearItems = value;
				RaisePropertyChanged(nameof(YearItems));
			}
		}

		private ObservableCollection<PatternRaceListItem> _PatternRaceListItem = new ObservableCollection<PatternRaceListItem>();
		public ObservableCollection<PatternRaceListItem> PatternRaceListItems
		{
			get
			{ return _PatternRaceListItem; }
			set
			{
				if (_PatternRaceListItem == value)
					return;
				_PatternRaceListItem = value;
				RaisePropertyChanged(nameof(PatternRaceListItem));
			}
		}

		#endregion

		public PatternRaceListPageViewModel() : base()
		{
			// 2002 ～ 今年までのドロップダウン作る
			for(int y = 2002; y < DateTime.Today.Year; y++)
			{
				_YearItems.Add(new YearItem { Year = y, YearString = y.ToString() + "年" });
			}

			PatternRaceListItems.Add(new PatternRaceListItem
			{
				Cource = "東京",
				CourceType = "芝",
				Date = new DateTime(2021, 1, 1),
				Distance = 1800,
				Grade = "G1",
				Handicap = "定量",
				Limit = "2歳",
				RaceName = "testtest"
			});
			PatternRaceListItems.Add(new PatternRaceListItem
			{
				Cource = "中京",
				CourceType = "芝",
				Date = new DateTime(2021, 1, 2),
				Distance = 1800,
				Grade = "G3",
				Handicap = "定量",
				Limit = "2歳",
				RaceName = "testtest"
			});
		}
	}
}
