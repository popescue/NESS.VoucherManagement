namespace NESS.VoucherManagement.Views
{
	using System;
	using System.Linq;
	using System.Windows;

	using Utils;

	using ViewModels;

	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void TimeSheetsDropHandler(object sender, DragEventArgs e)
		{
			var path = ExtractPath(e);

			if (path == null) return;

			((MainWindowViewModel) DataContext).PopulateTimekeeping(path, System.Drawing.Icon.ExtractAssociatedIcon(path).ToBitmapImage());
		}

		private void BusinessTripsDropHandler(object sender, DragEventArgs e)
		{
			var path = ExtractPath(e);

			if (path == null) return;

			((MainWindowViewModel) DataContext).PopulateBusinessTrips(path, System.Drawing.Icon.ExtractAssociatedIcon(path).ToBitmapImage());
		}

		private void EmployeesDropHandler(object sender, DragEventArgs e)
		{
			var path = ExtractPath(e);

			if (path == null) return;

			((MainWindowViewModel) DataContext).PopulateEmployees(path, System.Drawing.Icon.ExtractAssociatedIcon(path).ToBitmapImage());
		}

		private static string ExtractPath(DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return null;

			if (!(e.Data.GetData(DataFormats.FileDrop, true) is string[] data)) return null;

			return data[0];
		}
	}
}