using System;
using System.Linq;

namespace NESS.VoucherManagement.ViewModels
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Runtime.CompilerServices;
	using System.Windows.Forms;
	using System.Windows.Input;
	using System.Windows.Media.Imaging;
	using Application;
	using Core.Model;
	using Persistence;
	using Properties;
	using Utils.MVVM;

	public sealed class MainWindowViewModel : INotifyPropertyChanged
	{
		private bool isCalculating;

		public MainWindowViewModel()
		{
			TimeSheetsVm = new DropFileViewModel("Pontaj");
			BusinessTripsVm = new DropFileViewModel("Delegatii");
			EmployeesVm = new DropFileViewModel("Angajati");
			WorkingPeriodVm = new WorkingPeriodViewModel(new MonthYear(DateTime.Now.Year, DateTime.Now.Month));
		}

		public DropFileViewModel TimeSheetsVm { get; }

		public DropFileViewModel BusinessTripsVm { get; }

		public DropFileViewModel EmployeesVm { get; }

		public WorkingPeriodViewModel WorkingPeriodVm { get; }

		private string DestinationFile { get; set; }

		public bool IsCalculating
		{
			get => isCalculating;
			set
			{
				if (Equals(value, isCalculating)) return;
				isCalculating = value;
				OnPropertyChanged();
			}
		}

		public ICommand CalculateCommand
		{
			get
			{
				return new RelayCommand(parameter =>
				{
					IsCalculating = true;

					var openFileDialog = new SaveFileDialog
					{
						Filter = "Excel Files|*.xlsx"
					};

					var dialogResult = openFileDialog.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						DestinationFile = openFileDialog.FileName;
						CalculateVouchers();
					}

					IsCalculating = false;
				}, o => BusinessTripsVm.Path != null && EmployeesVm.Path != null && TimeSheetsVm.Path != null);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		internal void PopulateTimekeeping(string filePath, BitmapImage icon)
		{
			TimeSheetsVm.Path = filePath;
			TimeSheetsVm.FileIcon = icon;
			TimeSheetsVm.IsPopulated = true;
			CommandManager.InvalidateRequerySuggested();
		}

		internal void PopulateBusinessTrips(string filePath, BitmapImage icon)
		{
			BusinessTripsVm.Path = filePath;
			BusinessTripsVm.FileIcon = icon;
			BusinessTripsVm.IsPopulated = true;
			CommandManager.InvalidateRequerySuggested();
		}

		internal void PopulateEmployees(string filePath, BitmapImage icon)
		{
			EmployeesVm.Path = filePath;
			EmployeesVm.FileIcon = icon;
			EmployeesVm.IsPopulated = true;
			CommandManager.InvalidateRequerySuggested();
		}

		private void CalculateVouchers()
		{
			var outOfOfficeOperations = Settings.Default.OutOfOfficeOperations
				.Cast<string>()
				.Select(MapToOperation);

			try
			{
				IContext readContext = new EmployeeExcelContext(EmployeesVm.Path, TimeSheetsVm.Path, BusinessTripsVm.Path);
				IEmployeeReader reader = new EmployeeExcelReader(readContext);

				var writeContext = new VoucherExcelContext(DestinationFile);
				var writer = new VoucherExcelWriter(writeContext);

				var employees = reader.GetEmployees();
				var workingDays = WorkingDaysForMonth(WorkingPeriodVm.MonthYear);

				var vouchers = (IEnumerable<Voucher>) employees
					.Select(e => e.CalculateVouchers(workingDays, outOfOfficeOperations))
					.OrderBy(v => v.Employee.LastName)
					.ThenBy(v => v.Employee.FirstName);

				writer.WriteVouchers(vouchers);
			}
			catch (InvalidFileTypeException ex)
			{
				MessageBox.Show(string.Format(Resources.MainWindowViewModel_CalculateVouchers_InvalidFileTypeMessage, ex.FilePath), Resources.MainWindowViewModel_CalculateVouchers_InvalidFileTypeCaption);

				Debug.WriteLine(ex.ToString());
			}
		}

		private static int WorkingDaysForMonth(MonthYear monthYear)
		{
			var holidays = WebServiceHolidayProvider.GetHolidays(monthYear);
			return WorkingDays.Count(monthYear, holidays);
		}

		private static Operation MapToOperation(string s)
		{
			var split = s.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);

			//return new Operation(split[0].Trim(), split[1].Trim());
			return new Operation(split[0].Trim(), null);
		}

		[Annotations.NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}