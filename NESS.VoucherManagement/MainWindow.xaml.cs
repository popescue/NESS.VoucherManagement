using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using NESS.VoucherManagement.ViewModels;
using DataFormats = System.Windows.DataFormats;
using DragEventArgs = System.Windows.DragEventArgs;

namespace NESS.VoucherManagement
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx";
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                ((MainWindowViewModel) DataContext).DestinationFile = openFileDialog.FileName;
                ((MainWindowViewModel) DataContext).CalculateVouchers();
            }
        }
    }
}