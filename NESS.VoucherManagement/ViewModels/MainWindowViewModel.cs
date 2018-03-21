using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using NESS.VoucherManagement.Annotations;
using NESS.VoucherManagement.Core;
using NESS.VoucherManagement.Persistence;
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

		public string DestinationFile { get; set; }

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

		public ICommand TimekeepingDropCommand
		{
			get
			{
				return new RelayCommand(e =>
				                        {
					                        var filePath = ((string[]) ((DragEventArgs) e).Data.GetData(DataFormats.FileDrop))[0];
					                        PopulateTimekeeping(
						                        filePath,
						                        Icon.ExtractAssociatedIcon(filePath).ToBitmapImage()
					                        );
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

		public void CalculateVouchers()
		{
			var context = new ExcelContext(EmployeesVm.Path, TimekeepingVm.Path, BusinessTripsVm.Path);

			var repo = new EmployeeExcelReadonlyRepository(context);
			var employees = repo.GetEmployees();

			//var vouchers = employees.Select(e => e.CalculateVouchers(20, new[] {new Operation("201", "Vacation")}));

			//var writer = new VoucherExcelWriter(DestinationFile);

			//writer.Write(vouchers);

			//IEmployeeReader er = new ExcelEmployeeReader();
			//IEnumerable<ExcelEmployee> employees = er.Read(EmployeesVm.Path);

			//IBusinessTripReader btr = new ExcelBusinessTripReader();
			//IEnumerable<ExcelBusinessTrip> businessTrips = btr.Read(BusinessTripsVm.Path);

			//ITimekeepingReader tr = new ExcelTimekeepingReader();
			//IEnumerable<ExcelTimesheet> timesheets = tr.Read(TimekeepingVm.Path);

			//IEnumerable<Employee> emps = GetEmployees(employees, businessTrips, timesheets);

			//int workingDaysInMonth = GetWorkingDaysInMonth(this.WorkingPeriodVm.Month.Index);

			//IEnumerable<Operation> outOfOfficeOperations = Configuration.Operations();

			//IEnumerable<VoucherInfo> vouchers = emps.Select(e => e.CalculateVouchers(workingDaysInMonth, outOfOfficeOperations));

			//IVoucherWriter vw = new ExcelVoucherWriter(DestinationFile);
			//vw.Write(vouchers);

			Program.Calculate(EmployeesVm.Path, BusinessTripsVm.Path, TimekeepingVm.Path, DestinationFile, WorkingPeriodVm.Month.Index, WorkingPeriodVm.Year);
		}

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}