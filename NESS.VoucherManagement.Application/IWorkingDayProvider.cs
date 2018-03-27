using System;
using System.Linq;

namespace NESS.VoucherManagement.Application
{
	public interface IWorkingDayProvider
	{
		int GetCount(int year, int month);
	}
}