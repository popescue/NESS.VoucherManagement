using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Domain
{
	public struct Operation
	{
		public Operation(string id, string description)
			: this()
		{
			Id = id;
			Description = description;
		}

		public string Id { get; }

		public string Description { get; }
	}
}