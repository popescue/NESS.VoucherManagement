namespace NESS.VoucherManagement.Persistence.Model
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;

	// ReSharper disable once ClassNeverInstantiated.Global
	[SuppressMessage("NDepend", "ND1207:NonStaticClassesShouldBeInstantiatedOrTurnedToStatic", Justification = "Used for deserialization by 3rd party tool")]
	public sealed class ExcelEmployee
	{
		public string SapId { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string PersonalId { get; set; }
	}
}