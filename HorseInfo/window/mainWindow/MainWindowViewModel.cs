using System;
using System.Collections.Generic;
using System.Text;
using Livet;
using Livet.Commands;
using MahApps.Metro.Controls;

namespace HorseInfo
{
	public class MainWindowViewModel : ViewModel
	{
		public List<HumburgerItem> HumbergerItems { get; set; }
		public HumburgerItem SelectedItem { get; set; }

		public MainWindowViewModel()
		{
			HumbergerItems = new List<HumburgerItem>();

			HumbergerItems.Add(new HumburgerItem()
			{
				Label = "One",
				NavigationType = typeof(MainPage),
				NavigationDestination = new Uri("../../Page/MainPage/MainPage.xaml", UriKind.RelativeOrAbsolute)
			});
			HumbergerItems.Add(new HumburgerItem()
			{
				Label = "Two",
				NavigationType = typeof(TestPage),
				NavigationDestination = new Uri("../../Page/TestPage.xaml", UriKind.RelativeOrAbsolute)
			});
			HumbergerItems.Add(new HumburgerItem()
			{
				Label = "PatternRaceListPage",
				NavigationType = typeof(PatternRaceListPage),
				NavigationDestination = new Uri("../../Page/PatternRaceListPage/PatternRaceListPage.xaml", UriKind.RelativeOrAbsolute)
			});

		}
	}
}
