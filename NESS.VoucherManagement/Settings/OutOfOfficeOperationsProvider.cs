namespace NESS.VoucherManagement.Settings
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Application;

	using Properties;

	public sealed class OutOfOfficeOperationsProvider : IOutOfOfficeOperationsProvider
	{
		public IEnumerable<string> GetOperations()
			=> Settings.Default.OutOfOfficeOperations.Cast<string>();

		public Task<IEnumerable<string>> GetOperationsAsync()
			=> Task.FromResult(Settings.Default.OutOfOfficeOperations.Cast<string>());
	}
}