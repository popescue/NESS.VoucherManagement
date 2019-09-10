namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Linq;

	public interface IWorkingDaysProvider
	{
		int GetWorkingDays(When when);
	}
}