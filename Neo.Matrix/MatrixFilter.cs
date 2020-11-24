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
		/// <param name="yStride">steps in y direction</param>
		/// <param name="xStride">steps in x direction</param>
		/// <returns>matrix with results</returns>
		public static Matrix<double> RectBoxedAlgo<TType>(this Matrix<TType> matrix, int rows, int columns, Func<int, int, Matrix<TType>, double> func, int yStride = 1, int xStride = 1)
		{
			EvenRowsException.Check(rows);
			EvenColumnsException.Check(columns);

			OutOfRangeException.Check(columns, 0, matrix.Columns);
			OutOfRangeException.Check(rows, 0, matrix.Rows);

			OutOfRangeException.Check(xStride, 0);
			OutOfRangeException.Check(yStride, 0);

			var rowOffset = (rows - 1) / 2;
			var colOffset = (columns - 1) / 2;

			var resRows = matrix.Rows - 2 * rowOffset;
			var resCols = matrix.Columns - 2 * colOffset;

			if (resRows % yStride != 0)
				throw new MatrixException("rows must be divisible by stride");

			if (resCols % xStride != 0)
				throw new MatrixException("columns must be divisible by stride");

			resRows /= yStride;
			resCols /= xStride;

			var returnMat = new Matrix<double>(resRows, resCols);

			// k.p. todo: save already calculated values to speedup algorithm
			var column = 0;
			var row = 0;
			for (var i = rowOffset; i < matrix.Rows - rowOffset; i += yStride, row++, column = 0)
			for (var j = colOffset; j < matrix.Columns - colOffset; j += xStride, column++)
				returnMat[row, column] = func(i, j, matrix.GetRect(i, j, columns, rows));

			return returnMat;
		}
	}
}