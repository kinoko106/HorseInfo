using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Livet;
using HorseInfoCore;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HorseInfo
{
	public class PatternRaceListPageViewModel : PageViewModelBase
	{
		#region Bind Properties

		private ObservableSynchronizedCollection<PatternRace> _PatternRaces = new ObservableSynchronizedCollection<PatternRace>();
		public ObservableSynchronizedCollection<PatternRace> PatternRaces
		{
			get
			{ return _PatternRaces; }
			set
			{
				if (_PatternRaces == value)
					return;
				_PatternRaces = value;
				RaisePropertyChanged(nameof(PatternRaceListItem));
			}
		}

		#endregion

		#region Properties

		private PatternRaceDataGetter PatternRaceDataGetter { get;  set; } = new PatternRaceDataGetter(ConfigurationManager.AppSettings["patternRaceUrl"]);

		#endregion

		// tableじゃなくてGridで表示する

		public PatternRaceListPageViewModel() : base()
		{
			BindingOperations.EnableCollectionSynchronization(PatternRaces, new object());
			// 検索して画面にレース一覧を表示
			var patternRaceList = PatternRaceDataGetter.GetPatternRaceList();

			Task.Run(async () =>
			{
				var patternRaces = await PatternRaceDataGetter.GetPatternRaceList();
				foreach (var patternRace in patternRaces)
				{
					PatternRaces.Add(patternRace);
				}
			});

			//PatternRaces.Add(new PatternRace()
			//{
			//	Cource = Cource.Hakodate,
			//	CourceName = "函館",
			//	Date = new DateTime(2021, 1, 1),
			//	Distance = 2000,
			//	HandicapType = "",
			//	RaceGrade = RaceGrade.G1,
			//	RaceLimit = "",
			//	RaceName = "テストレース",
			//	SpecialRacePageId = "test"
			//});
		}
	}
}
