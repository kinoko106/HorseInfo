using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Navigation;

using MahApps;
using MahApps.Metro.Controls;

namespace HorseInfo
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		private readonly NavigationServiceEx navigationServiceEx;


		public MainWindow()
		{
			InitializeComponent();

			this.navigationServiceEx = new NavigationServiceEx();
			this.navigationServiceEx.Navigated += this.NavigationServiceEx_OnNavigated;
			this.HamburgerMenuControl.Content = this.navigationServiceEx.Frame;

			this.Loaded += (sender, args) => this.navigationServiceEx.Navigate(new Uri("../../Page/MainPage/MainPage.xaml", UriKind.RelativeOrAbsolute));

			KeibaDataManager.Instance.GetType();
		}

		private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
		{
			if (e.InvokedItem is HumburgerItem humburgerItem && humburgerItem.IsNavigation)
			{
				this.navigationServiceEx.Navigate(humburgerItem.NavigationDestination);
			}
		}

        private void NavigationServiceEx_OnNavigated(object sender, NavigationEventArgs e)
        {
			// select the menu item
			this.HamburgerMenuControl.SelectedItem = this.HamburgerMenuControl
														 .Items
														 .OfType<HumburgerItem>()
														 .FirstOrDefault(x => x.NavigationDestination == e.Uri);
			//this.HamburgerMenuControl.SelectedOptionsItem = this.HamburgerMenuControl
			//                                                    .OptionsItems
			//                                                    .OfType<MenuItem>()
			//                                                    .FirstOrDefault(x => x.NavigationDestination == e.Uri);

		}
    }
}
