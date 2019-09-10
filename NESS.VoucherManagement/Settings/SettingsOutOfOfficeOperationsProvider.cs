namespace NESS.VoucherManagement.Settings
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Application;

	using Properties;

	public sealed class SettingsOutOfOfficeOperationsProvider : IOutOfOfficeOperationsProvider
	{
		public IEnumerable<string> GetOperations() =>
			Settings.Default.OutOfOfficeOperations.Cast<string>();
	}
}