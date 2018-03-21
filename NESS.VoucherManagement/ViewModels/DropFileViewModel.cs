using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using NESS.VoucherManagement.Properties;

namespace NESS.VoucherManagement.ViewModels
{
	public sealed class DropFileViewModel : INotifyPropertyChanged
	{
		private string description;

		private BitmapImage fileIcon;

		private bool isPopulated;

		private string path;

		public DropFileViewModel(string description) => Description = description;

		public string Path
		{
			get => path;
			set
			{
				if (value == path) return;
				path = value;
				OnPropertyChanged();
			}
		}

		public string Description
		{
			get => description;
			set
			{
				if (value == description) return;
				description = value;
				OnPropertyChanged();
			}
		}

		public BitmapImage FileIcon
		{
			get => fileIcon;
			internal set
			{
				if (Equals(value, fileIcon)) return;
				fileIcon = value;
				OnPropertyChanged();
			}
		}

		public bool IsPopulated
		{
			get => isPopulated;
			internal set
			{
				if (value == isPopulated) return;
				isPopulated = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}