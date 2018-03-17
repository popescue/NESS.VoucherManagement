using System;
using System.Linq;
using NESS.VoucherManagement.Core.Domain;
using Xunit;

namespace NESS.VoucherManagement.Core.Tests
{
	public class OperationTests
	{
		[Theory(DisplayName = "Operations with different properties are not equal")]
		[InlineData("1", "A", "1", "B")]
		[InlineData("1", "A", "2", "A")]
		public void Test2(string id1, string desc1, string id2, string desc2)
		{
			var o1 = new Operation(id1, desc1);
			var o2 = new Operation(id2, desc2);

			Assert.NotEqual(o1, o2);
		}

		[Fact(DisplayName = "Operations with same properties are equal")]
		public void Test1()
		{
			var o1 = new Operation("1", "A");
			var o2 = new Operation("1", "A");

			Assert.Equal(o1, o2);
		}
	}
}