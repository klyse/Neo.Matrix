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
}