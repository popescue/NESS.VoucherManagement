using System;
using System.Linq;
using System.Runtime.Serialization;

namespace NESS.VoucherManagement.Persistence
{
	[Serializable]
	public class InvalidFileTypeException : Exception
	{
		private const string DefaultMessage = "The file was not of the expected type.";

		public InvalidFileTypeException(string filePath)
			: base(DefaultMessage) => FilePath = filePath;

		public InvalidFileTypeException(string filePath, string message)
			: base(message) => FilePath = filePath;

		public InvalidFileTypeException(string filePath, string message, Exception inner)
			: base(message, inner) => FilePath = filePath;

		protected InvalidFileTypeException(
			SerializationInfo info,
			StreamingContext context) : base(info, context) { }

		public string FilePath { get; }
	}
}