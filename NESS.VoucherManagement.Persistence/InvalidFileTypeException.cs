using System;
using System.Linq;
using System.Runtime.Serialization;

namespace NESS.VoucherManagement.Persistence
{
	[Serializable]
	public class InvalidFileTypeException : Exception
	{
		public InvalidFileTypeException(string filePath, string message, Exception inner)
			: base(message, inner) => FilePath = filePath;

		protected InvalidFileTypeException(
			SerializationInfo info,
			StreamingContext context) : base(info, context) { }

		public string FilePath { get; }
	}
}