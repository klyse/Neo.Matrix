using System;

namespace NeoMatrix.Exceptions
{
	/// <summary>
	///     Base exception class for Neo.Matrix exceptions
	/// </summary>
	public class MatrixException : Exception
	{
		/// <summary>
		///     Default constructor
		/// </summary>
		public MatrixException(string message) : base(message)
		{
		}
	}

	/// <summary>
	///     Invalid Stride
	/// </summary>
	public class StrideException : Exception
	{
		/// <summary>
		///     Default constructor
		/// </summary>
		private StrideException(string message) : base(message)
		{
		}

		public static void YStrideException()
		{
			throw new StrideException("yStride must be <= rows");
		}

		public static void XStrideException()
		{
			throw new StrideException("xStride must be <= columns");
		}
		public static void ColumnsStrideException()
		{
			throw new StrideException("columns must be divisible by stride");
		}
		public static void RowsStrideException()
		{
			throw new StrideException("rows must be divisible by stride");
		}
	}
}