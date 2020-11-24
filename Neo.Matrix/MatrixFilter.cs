using System;
using NeoMatrix.Exceptions;

namespace NeoMatrix
{
	/// <summary>
	///     Matrix Filter class
	/// </summary>
	public static class MatrixFilter
	{
		/// <summary>
		///     Boxed sum of a matrix
		/// </summary>
		/// <param name="matrix">matrix</param>
		/// <param name="rows">rows</param>
		/// <param name="columns">columns</param>
		/// <param name="func">function for filter</param>
		/// <returns>matrix with results</returns>
		public static Matrix<double> RectBoxedSum<TType>(this Matrix<TType> matrix, int rows, int columns, Func<Matrix<TType>, double> func)
		{
			EvenRowsException.Check(rows);
			EvenColumnsException.Check(columns);

			OutOfRangeException.Check(columns, 0, matrix.Columns);
			OutOfRangeException.Check(rows, 0, matrix.Rows);

			var colOffset = (columns - 1) / 2;
			var rowOffset = (rows - 1) / 2;

			var resCols = matrix.Columns - 2 * colOffset;
			var resRows = matrix.Rows - 2 * rowOffset;

			var returnMat = new Matrix<double>(resRows, resCols);

			// k.p. todo: save already calculated values to speedup algorithm
			for (var i = rowOffset; i < matrix.Rows - rowOffset; i++)
			for (var j = colOffset; j < matrix.Columns - colOffset; j++)
				returnMat[i - rowOffset, j - colOffset] = func(matrix.GetRect(i, j, columns, rows));

			return returnMat;
		}
	}
}