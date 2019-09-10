namespace NESS.VoucherManagement.Settings
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Application;

	using Core.Model;

	using Properties;

	public sealed class SettingsOutOfOfficeOperationsProvider : IOutOfOfficeOperationsProvider
	{
		public IEnumerable<Operation> GetOutOfOfficeOperations() =>
			Settings.Default.OutOfOfficeOperations
				.Cast<string>()
				.Select(MapToOperation);

		private static Operation MapToOperation(string s)
		{
			var split = s.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);

			//return new Operation(split[0].Trim(), split[1].Trim());
			return new Operation(split[0].Trim(), null);
		}
	}
}