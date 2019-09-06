namespace NESS.VoucherManagement.Persistence.Model
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;

	// ReSharper disable once ClassNeverInstantiated.Global
	[SuppressMessage("NDepend", "ND1207:NonStaticClassesShouldBeInstantiatedOrTurnedToStatic", Justification = "Used for deserialization by 3rd party tool")]
	public sealed class ExcelBusinessTrip
	{
		public string EmployeeSapId { get; set; }

		public string CompanyCode { get; set; }

		public string Name { get; set; }

		public int DaysInBusinessTrip { get; set; }
	}
}