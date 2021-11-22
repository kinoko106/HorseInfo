using System;
using System.Collections.Generic;
using System.Text;
using Livet;

namespace HorseInfo
{
	public class PageViewModelBase : ViewModel
	{
		public PageViewModelBase()
		{
			Width = 778;
			Height = 1150;
		}

		protected int _Width;
		public int Width
		{
			get
			{ return _Width; }
			set
			{
				if (_Width == value)
					return;
				_Width = value;
				RaisePropertyChanged(nameof(_Width));
			}
		}


		protected int _Height;
		public int Height
		{
			get
			{ return _Height; }
			set
			{
				if (_Height == value)
					return;
				_Height = value;
				RaisePropertyChanged(nameof(_Height));
			}
		}
	}
}
