namespace NESS.VoucherManagement.ViewModels
{
	using System;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Linq;
	using System.Runtime.CompilerServices;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using System.Windows.Input;
	using System.Windows.Media.Imaging;

	using Application;

	using Calendar;

	using Hollidays;

	using Persistence;

	using Properties;

	using Settings;

	using Utils.MVVM;

	public sealed class MainWindowViewModel : INotifyPropertyChanged
	{
		private bool isCalculating;

		public MainWindowViewModel()
		{
			TimeSheetsVm = new DropFileViewModel("Pontaj");
			BusinessTripsVm = new DropFileViewModel("Delegatii");
			EmployeesVm = new DropFileViewModel("Angajati");
			WorkingPeriodVm = new WorkingPeriodViewModel(new When(DateTime.Now.Year, DateTime.Now.Month));
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
				return new RelayCommand(async parameter =>
				{
					IsCalculating = true;

					var openFileDialog = new SaveFileDialog
					{
						Filter = @"Excel Files | *.xlsx"
					};

					var dialogResult = openFileDialog.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						DestinationFile = openFileDialog.FileName;

#if ASYNC
						await CalculateVouchersAsync().ConfigureAwait(false);
#else
						CalculateVouchers();
#endif
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
			try
			{
				IReadContext readContext = new EmployeeExcelContext(EmployeesVm.Path, TimeSheetsVm.Path, BusinessTripsVm.Path);
				IEmployeeReader reader = new EmployeeExcelReader(readContext);

				var writeContext = new VoucherExcelContext(DestinationFile);
				var writer = new VoucherExcelWriter(writeContext);

				var operationsProvider = new OutOfOfficeOperationsProvider();

				var workingDaysForMonthProvider = new WorkingDaysProvider(new WebServiceHolidayProvider(), new LocalWeekDaysProvider());

				var vouchersUseCase = new VouchersUseCase(reader, writer, operationsProvider, workingDaysForMonthProvider);

				vouchersUseCase.CalculateVouchers(WorkingPeriodVm.When);
			}
			catch (InvalidFileTypeException ex)
			{
				MessageBox.Show(string.Format(Resources.MainWindowViewModel_CalculateVouchers_InvalidFileTypeMessage, ex.FilePath), Resources.MainWindowViewModel_CalculateVouchers_InvalidFileTypeCaption);

				Debug.WriteLine(ex.ToString());
			}
		}

		private async Task CalculateVouchersAsync()
		{
			try
			{
				IReadContext readContext = new EmployeeExcelContext(EmployeesVm.Path, TimeSheetsVm.Path, BusinessTripsVm.Path);
				IEmployeeReader reader = new EmployeeExcelReader(readContext);

				var writeContext = new VoucherExcelContext(DestinationFile);
				var writer = new VoucherExcelWriter(writeContext);

				var operationsProvider = new OutOfOfficeOperationsProvider();

				var workingDaysForMonthProvider = new WorkingDaysProvider(new WebServiceHolidayProvider(), new LocalWeekDaysProvider());

				var vouchersUseCase = new VouchersUseCase(reader, writer, operationsProvider, workingDaysForMonthProvider);

				await vouchersUseCase.CalculateVouchersAsync(WorkingPeriodVm.When).ConfigureAwait(false);
			}
			catch (InvalidFileTypeException ex)
			{
				MessageBox.Show(string.Format(Resources.MainWindowViewModel_CalculateVouchers_InvalidFileTypeMessage, ex.FilePath), Resources.MainWindowViewModel_CalculateVouchers_InvalidFileTypeCaption);

				Debug.WriteLine(ex.ToString());
			}
		}

		[Annotations.NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}