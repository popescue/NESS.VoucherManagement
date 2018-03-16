﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using NESS.VoucherManagement.Core;
using NESS.VoucherManagement.Properties;

namespace NESS.VoucherManagement.ViewModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            TimekeepingVm = new DropFileVm("Pontaj");
            BusinessTripsVm = new DropFileVm("Delegatii");
            EmployeesVm = new DropFileVm("Angajati");
            WorkingPeriodVm = new WorkingPeriodVm(DateTime.Now.Year, DateTime.Now.Month);
        }

        public DropFileVm TimekeepingVm { get; }

        public DropFileVm BusinessTripsVm { get; }

        public DropFileVm EmployeesVm { get; }

        public WorkingPeriodVm WorkingPeriodVm { get; }

        public string DestinationFile { get; set; }

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
            Program.Calculate(EmployeesVm.Path, BusinessTripsVm.Path, TimekeepingVm.Path, DestinationFile, WorkingPeriodVm.Month.Index, WorkingPeriodVm.Year);
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}