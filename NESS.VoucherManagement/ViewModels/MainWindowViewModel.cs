using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using NESS.VoucherManagement.Application;
using NESS.VoucherManagement.Core.Model;
using NESS.VoucherManagement.Persistence;
using NESS.VoucherManagement.Properties;
using NESS.VoucherManagement.Utils;
using NESS.VoucherManagement.Utils.MVVM;

namespace NESS.VoucherManagement.ViewModels
{
	public sealed class MainWindowViewModel : INotifyPropertyChanged
	{
		private bool isCalculating;

		public MainWindowViewModel()
		{
			TimekeepingVm = new DropFileViewModel("Pontaj");
			BusinessTripsVm = new DropFileViewModel("Delegatii");
			EmployeesVm = new DropFileViewModel("Angajati");
			WorkingPeriodVm = new WorkingPeriodViewModel(DateTime.Now.Year);
		}

		public DropFileViewModel TimekeepingVm { get; }

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
					var openFileDialog = new SaveFileDialog();
					openFileDialog.Filter = "Excel Files|*.xlsx";
					var dialogResult = openFileDialog.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						DestinationFile = openFileDialog.FileName;
						await Task.Run(() =>
						{
							CalculateVouchers();
						});
					}

					IsCalculating = false;
				});
			}
		}

		public ICommand TimesheetsDropCommand
		{
			get
			{
				return new RelayCommand(e =>
				{
					var filePath = ((string[]) ((DragEventArgs) e).Data.GetData(DataFormats.FileDrop))[0];
					PopulateTimekeeping(filePath, Icon.ExtractAssociatedIcon(filePath).ToBitmapImage());
				});
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		internal void PopulateTimekeeping(string filePath, BitmapImage icon)
		{
			TimekeepingVm.Path = filePath;
			TimekeepingVm.FileIcon = icon;
			TimekeepingVm.IsPopulated = true;
		}

		internal void PopulateBusinessTrips(string filePath, BitmapImage icon)
		{
			BusinessTripsVm.Path = filePath;
			BusinessTripsVm.FileIcon = icon;
			BusinessTripsVm.IsPopulated = true;
		}

		internal void PopulateEmployees(string filePath, BitmapImage icon)
		{
			EmployeesVm.Path = filePath;
			EmployeesVm.FileIcon = icon;
			EmployeesVm.IsPopulated = true;
		}

		private void CalculateVouchers()
		{
			var readContext = new EmployeeExcelContext(EmployeesVm.Path, TimekeepingVm.Path, BusinessTripsVm.Path);

			var reader = new EmployeeExcelReader(readContext);

			var writeContext = new VoucherExcelContext(DestinationFile);

			var writer = new VoucherExcelWriter(writeContext);

			var operations = Settings.Default.OutOfOfficeOperations
				.Cast<string>()
				.Select(MapToOperation);

			IHolidayProvider holidayProvider = new WebServiceHolidayProvider();
			IWorkingDayProvider workingDayProvider = new CalendarWorkingDayProvider(holidayProvider);

			//var workingDays = b.Count(year:WorkingPeriodVm.Year, month:WorkingPeriodVm.Month.Index);

			var command = new CalculateVouchersCommand(operations, WorkingPeriodVm.Year, WorkingPeriodVm.Month.Index);

			var commandHandler = new CalculateVouchersCommandHandler(reader, writer, workingDayProvider);

			commandHandler.Handle(command);
		}

		private static Operation MapToOperation(string s)
		{
			var split = s.Split(new[]
			{
				'='
			}, StringSplitOptions.RemoveEmptyEntries);

			return new Operation(split[0].Trim(), split[1].Trim());
		}

		[Annotations.NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}