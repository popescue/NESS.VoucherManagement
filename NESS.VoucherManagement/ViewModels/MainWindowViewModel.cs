using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using NESS.VoucherManagement.Core;
using NESS.VoucherManagement.Properties;
using NESS.VoucherManagement.Utils;
using NESS.VoucherManagement.Utils.MVVM;

namespace NESS.VoucherManagement.ViewModels
{
    public sealed class MainWindowViewModel :INotifyPropertyChanged
    {
        private bool _isCalculating;

        public MainWindowViewModel()
        {
            TimekeepingVm = new DropFileViewModel("Pontaj");
            BusinessTripsVm = new DropFileViewModel("Delegatii");
            EmployeesVm = new DropFileViewModel("Angajati");
            WorkingPeriodVm = new WorkingPeriodViewModel(DateTime.Now.Year, DateTime.Now.Month);
        }

        public DropFileViewModel TimekeepingVm { get; }

        public DropFileViewModel BusinessTripsVm { get; }

        public DropFileViewModel EmployeesVm { get; }

        public WorkingPeriodViewModel WorkingPeriodVm { get; }

        public string DestinationFile { get; set; }

        public bool IsCalculating
        {
            get => _isCalculating;
            set
            {
                if (Equals(value, _isCalculating)) return;
                _isCalculating = value;
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
                    var filePath = ((string[])((DragEventArgs)e).Data.GetData(DataFormats.FileDrop))[0];
                    PopulateTimekeeping(
                        filePath,
                        System.Drawing.Icon.ExtractAssociatedIcon(filePath).ToBitmapImage()
                    );
                });
            }
        }

  


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
            Program.Calculate(EmployeesVm.Path, BusinessTripsVm.Path, TimekeepingVm.Path, DestinationFile, WorkingPeriodVm.Month.Index, WorkingPeriodVm.Year);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [Annotations.NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}