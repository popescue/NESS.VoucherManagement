using System;
using System.Linq;

namespace NESS.VoucherManagement.ViewModels
{
	using System.ComponentModel;
	using System.Diagnostics;
	using System.IO;
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
			TimesheetsVm = new DropFileViewModel("Pontaj");
			BusinessTripsVm = new DropFileViewModel("Delegatii");
			EmployeesVm = new DropFileViewModel("Angajati");
			WorkingPeriodVm = new WorkingPeriodViewModel(DateTime.Now.Year);
		}

		public DropFileViewModel TimesheetsVm { get; }

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
				}, o => BusinessTripsVm.Path != null && EmployeesVm.Path != null && TimesheetsVm.Path != null);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		internal void PopulateTimekeeping(string filePath, BitmapImage icon)
		{
			TimesheetsVm.Path = filePath;
			TimesheetsVm.FileIcon = icon;
			TimesheetsVm.IsPopulated = true;
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
			var operations = Settings.Default.OutOfOfficeOperations
				.Cast<string>()
				.Select(MapToOperation);

			var command = new CalculateVouchersCommand(operations, WorkingPeriodVm.Year, WorkingPeriodVm.Month.Index);

			var readContext = new EmployeeExcelContext(EmployeesVm.Path, TimesheetsVm.Path, BusinessTripsVm.Path);
			var reader = new EmployeeExcelReader(readContext);

			var writeContext = new VoucherExcelContext(DestinationFile);
			var writer = new VoucherExcelWriter(writeContext);

			IHolidayProvider holidayProvider = new WebServiceHolidayProvider();
			IWorkingDayProvider workingDayProvider = new CalendarWorkingDayProvider(holidayProvider);

			var commandHandler = new CalculateVouchersCommandHandler(reader, writer, workingDayProvider);

			try
			{
				commandHandler.Handle(command);
			}
			catch (FileInUseException ex)
			{
				MessageBox.Show(string.Format(Resources.MainWindowViewModel_CalculateVouchers_FileInUse, ex.FilePath), Resources.MainWindowViewModel_CalculateVouchers_FileInUseCaption);

				Debug.WriteLine(ex.ToString());
			}
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