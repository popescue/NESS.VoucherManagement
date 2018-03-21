using System;
using System.Collections.Generic;
using System.Linq;

namespace NESS.VoucherManagement.Persistence.Model
{
	public class ExcelEmployee
	{
		public static IEqualityComparer<ExcelEmployee> ExcelEmployeeComparer { get; } = new ExcelEmployeeEqualityComparer();

		public string SapId { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string PersonalId { get; set; }

		private sealed class ExcelEmployeeEqualityComparer : IEqualityComparer<ExcelEmployee>
		{
			public bool Equals(ExcelEmployee x, ExcelEmployee y)
			{
				if (ReferenceEquals(x, y)) return true;
				if (ReferenceEquals(x, null)) return false;
				if (ReferenceEquals(y, null)) return false;
				if (x.GetType() != y.GetType()) return false;
				return string.Equals(x.SapId, y.SapId) && string.Equals(x.LastName, y.LastName) && string.Equals(x.FirstName, y.FirstName) && string.Equals(x.PersonalId, y.PersonalId);
			}

			public int GetHashCode(ExcelEmployee obj)
			{
				unchecked
				{
					var hashCode = obj.SapId != null ?
						obj.SapId.GetHashCode() :
						0;
					hashCode = (hashCode * 397) ^ (obj.LastName != null ?
						           obj.LastName.GetHashCode() :
						           0);
					hashCode = (hashCode * 397) ^ (obj.FirstName != null ?
						           obj.FirstName.GetHashCode() :
						           0);
					hashCode = (hashCode * 397) ^ (obj.PersonalId != null ?
						           obj.PersonalId.GetHashCode() :
						           0);
					return hashCode;
				}
			}
		}
	}
}