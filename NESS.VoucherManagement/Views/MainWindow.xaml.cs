using System;
using System.Linq;
using System.Windows;
using NESS.VoucherManagement.Utils;
using NESS.VoucherManagement.ViewModels;

namespace NESS.VoucherManagement.Views
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void TimekeepingDropHandler(object sender, DragEventArgs e)
		{
			var filePath = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
			((MainWindowViewModel) DataContext).PopulateTimekeeping(
				filePath,
				System.Drawing.Icon.ExtractAssociatedIcon(filePath).ToBitmapImage()
			);
		}

		private void BusinessTripsDropHandler(object sender, DragEventArgs e)
		{
			var filePath = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
			((MainWindowViewModel) DataContext).PopulateBusinessTrips(
				filePath,
				System.Drawing.Icon.ExtractAssociatedIcon(filePath).ToBitmapImage()
			);
		}

		private void EmployeesDropHandler(object sender, DragEventArgs e)
		{
			var filePath = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
			((MainWindowViewModel) DataContext).PopulateEmployees(
				filePath,
				System.Drawing.Icon.ExtractAssociatedIcon(filePath).ToBitmapImage()
			);
		}

		//private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		//{
		//	ExecuteButton.IsEnabled = false;
		//}
	}
}