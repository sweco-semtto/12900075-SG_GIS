using System;
using System.Runtime.InteropServices;

namespace SGAB.SGAB_Karta
{
	/// <summary>
	/// Summary description for Exception.
	/// </summary>
	[ComVisible(false)]
	public class MapException : Exception
	{
		public MapException() : base()
		{
		}

		public MapException(string message) : base(message)
		{
		}

		public MapException(Exception innerException, string message) : base(message, innerException)
		{
		}
	}

	[ComVisible(false)]
	public class GPSException : Exception
	{
		public GPSException() : base()
		{
		}

		public GPSException(string message) : base(message)
		{
		}

		public GPSException(Exception innerException, string message) : base(message, innerException)
		{
		}
	}

	[ComVisible(false)]
	public class DataException : Exception
	{
		public DataException() : base()
		{
		}

		public DataException(string message) : base(message)
		{
		}

		public DataException(Exception innerException, string message) : base(message, innerException)
		{
		}
	}

	[ComVisible(false)]
	public class FormatException : Exception
	{
		public FormatException() : base()
		{
		}

		public FormatException(string message) : base(message)
		{
		}

		public FormatException(Exception innerException, string message) : base(message, innerException)
		{
		}
	}

}
