namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;

	public interface IWorkingDaysProvider
	{
		int GetWorkingDays(When when);

		Task<int> GetWorkingDaysAsync(When when);
	}
}