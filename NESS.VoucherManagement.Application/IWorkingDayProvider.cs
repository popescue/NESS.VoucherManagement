using System;
using System.Linq;

namespace NESS.VoucherManagement.Application
{
	public interface IWorkingDayProvider
	{
		int Count(int year, int month);
	}
}