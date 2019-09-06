using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NESS.VoucherManagement.Views
{
	public partial class DropFileControl
	{
		// Using a DependencyProperty as the backing store for FilePath.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty FilePathProperty =
			DependencyProperty.Register("FilePath", typeof(string), typeof(DropFileControl));

		// Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DescriptionProperty =
			DependencyProperty.Register("Description", typeof(string), typeof(DropFileControl));

		// Using a DependencyProperty as the backing store for FileIcon.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty FileIconProperty =
			DependencyProperty.Register("FileIcon", typeof(BitmapImage), typeof(DropFileControl));

		// Using a DependencyProperty as the backing store for FrontFacing.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty FrontFacingProperty =
			DependencyProperty.Register("FrontFacing", typeof(bool), typeof(DropFileControl), new PropertyMetadata(true));

		public DropFileControl()
		{
			InitializeComponent();
		}

		public bool FrontFacing { get => (bool) GetValue(FrontFacingProperty); set => SetValue(FrontFacingProperty, value); }

		public BitmapImage FileIcon { get => (BitmapImage) GetValue(FileIconProperty); set => SetValue(FileIconProperty, value); }

		public string Description { get => (string) GetValue(DescriptionProperty); set => SetValue(DescriptionProperty, value); }

		public string FilePath { get => (string) GetValue(FilePathProperty); set => SetValue(FilePathProperty, value); }
	}
}