using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using MahApps.Metro.Controls;

namespace HorseInfo
{
	public class HumburgerItem : HamburgerMenuItem
	{
        public static readonly DependencyProperty NavigationDestinationProperty = DependencyProperty.Register(
            nameof(NavigationDestination), typeof(Uri), typeof(HumburgerItem), new PropertyMetadata(default(Uri)));

        public Uri NavigationDestination
        {
            get => (Uri)this.GetValue(NavigationDestinationProperty);
            set => this.SetValue(NavigationDestinationProperty, value);
        }

        public static readonly DependencyProperty NavigationTypeProperty = DependencyProperty.Register(
          nameof(NavigationType), typeof(Type), typeof(HumburgerItem), new PropertyMetadata(default(Type)));

        public Type NavigationType
        {
            get => (Type)this.GetValue(NavigationTypeProperty);
            set => this.SetValue(NavigationTypeProperty, value);
        }

        public bool IsNavigation => this.NavigationDestination != null;
    }
}
