using System;

namespace NeoMatrix.Exceptions
{
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

		/// <summary>
		///     yStride exception
		/// </summary>
		/// <exception cref="StrideException"></exception>
		public static void YStrideException()
		{
			throw new StrideException("yStride must be <= rows");
		}

		/// <summary>
		///     xStride exception
		/// </summary>
		/// <exception cref="StrideException"></exception>
		public static void XStrideException()
		{
			throw new StrideException("xStride must be <= columns");
		}

		/// <summary>
		///     columns stride exception
		/// </summary>
		/// <exception cref="StrideException"></exception>
		public static void ColumnsStrideException()
		{
			throw new StrideException("columns must be divisible by stride");
		}


		/// <summary>
		///     rows stride exception
		/// </summary>
		/// <exception cref="StrideException"></exception>
		public static void RowsStrideException()
		{
			throw new StrideException("rows must be divisible by stride");
		}
	}
}