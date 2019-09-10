﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace NESS.VoucherManagement.Utils.Converters
{
	using System.Diagnostics.CodeAnalysis;

	[ValueConversion(typeof(bool), typeof(bool))]
	[SuppressMessage("ReSharper", "TooManyArguments")]
	public class InverseBooleanConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter,
		                      CultureInfo culture)
		{
			if (targetType != typeof(bool))
				throw new InvalidOperationException("The target must be a boolean");
			if (value == null)
				return false;
			return !(bool) value;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
		                          CultureInfo culture) => null;

		#endregion
	}
}