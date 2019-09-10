namespace NESS.VoucherManagement.Utils.Converters
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Windows.Data;

	[SuppressMessage("NDepend", "ND1700:PotentiallyDeadTypes", Justification = "Used in WPF as resource")]
	[SuppressMessage("ReSharper", "TooManyArguments")]
	internal sealed class FilePathValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var pathAsString = value as string ?? "[No Path Selected]";

			return Path.GetFileName(pathAsString);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => "yyy";
	}
}