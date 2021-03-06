﻿namespace NeoMatrix.Exceptions
{
	/// <summary>
	///     Even rows are not permitted
	/// </summary>
	public class UnevenRowsException : MatrixException
	{
		/// <summary>
		///     Default constructor
		/// </summary>
		public UnevenRowsException() : base("uneven row number is not permitted")
		{
		}

		/// <summary>
		///     check and throw exception
		/// </summary>
		public static void Check(int n)
		{
			if (n % 2 != 0)
				throw new UnevenRowsException();
		}
	}
}