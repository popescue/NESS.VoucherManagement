namespace NESS.VoucherManagement.Models
{
	using System;
	using System.Linq;

	public struct Month
	{
		public Month(int index, string name)
		{
			Index = index;
			Name = name;
		}

		public int Index { get; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		// ReSharper disable once MemberCanBePrivate.Global
		public string Name { get; }
	}
}