using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Data;

namespace NESS.VoucherManagement.Utils.Converters
{
	internal class FilePathValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var pathAsString = value as string ?? "[No Path Selected]";

			return Path.GetFileName(pathAsString);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => "yyy";
	}
}