namespace NESS.VoucherManagement.Persistence
{
	using System;
	using System.Linq;

	public class FileInUseException : Exception
	{
		private readonly Exception innerException;

		private readonly string message;

		public FileInUseException(string filePath) => FilePath = filePath;

		public FileInUseException(string filePath, Exception innerException)
		{
			FilePath = filePath;
			this.innerException = innerException;
		}

		public FileInUseException(string filePath, string message) : base(message)
		{
			FilePath = filePath;
			this.message = message;
		}

		public string FilePath { get; }
	}
}