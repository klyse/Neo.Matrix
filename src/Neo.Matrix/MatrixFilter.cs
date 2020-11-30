using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
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
		/// <param name="func">function with algo</param>
		/// <param name="yStride">
		///     stride defaults to 1; must be a multiple of output array size (
		///     <paramref name="matrix.Rows" /> - <paramref name="rows" /> +1)
		/// </param>
		/// <param name="xStride">
		///     stride defaults to 1; must be a multiple of output array size (
		///     <paramref name="matrix.Columns" /> - <paramref name="columns" /> +1)
		/// </param>
		/// <returns>
		///     new <see cref="Matrix{TElement}" /> with size: (<paramref name="matrix.Rows" /> - <paramref name="rows" /> +1)
		///     x (<paramref name="matrix.Columns" /> - <paramref name="columns" /> +1) containing the avg value of the box
		/// </returns>
		public static Matrix<double> RectBoxedAlgo<TType>(this Matrix<TType> matrix, int rows, int columns, Func<int, int, Matrix<TType>, double> func, int yStride = 1, int xStride = 1)
		{
			CalculateMatrixParameters(matrix, rows, columns, yStride, xStride, out var rowOffset, out var colOffset, out var remainingRows, out var remainingColumns);

			var returnMat = new Matrix<double>(remainingRows, remainingColumns);

			var column = 0;
			var row = 0;
			for (var i = rowOffset; i < matrix.Rows - rowOffset; i += yStride, row++, column = 0)
			for (var j = colOffset; j < matrix.Columns - colOffset; j += xStride, column++)
				returnMat[row, column] = func(i, j, matrix.GetRect(i, j, rows, columns));

			return returnMat;
		}

		/// <summary>
		///     Calculate the average within a moving box
		/// </summary>
		/// <param name="matrix">self</param>
		/// <param name="rows">box rows</param>
		/// <param name="columns">box columns</param>
		/// <param name="selector">selector for property</param>
		/// <param name="yStride">
		///     stride defaults to 1; must be a multiple of output array size (
		///     <paramref name="matrix.Rows" /> - <paramref name="rows" /> +1)
		/// </param>
		/// <param name="xStride">
		///     stride defaults to 1; must be a multiple of output array size (
		///     <paramref name="matrix.Columns" /> - <paramref name="columns" /> +1)
		/// </param>
		/// <returns>
		///     new <see cref="Matrix{TElement}" /> with size: (<paramref name="matrix.Rows" /> - <paramref name="rows" /> +1)
		///     x (<paramref name="matrix.Columns" /> - <paramref name="columns" /> +1) containing the avg value of the box
		/// </returns>
		public static Matrix<double> RectBoxedAvg<TType>(this Matrix<TType> matrix, int rows, int columns, Expression<Func<TType, double>> selector, int yStride = 1, int xStride = 1)
		{
			CalculateMatrixParameters(matrix, rows, columns, yStride, xStride, out var rowOffset, out var colOffset, out var remainingRows, out var remainingColumns);

			var cacheMatrix = CalculateRowCacheMatrix(matrix, selector, rowOffset);

			var returnMat = new Matrix<double>(remainingRows, remainingColumns);
			double space = rows * columns;

			var column = 0;
			var row = 0;
			for (var i = rowOffset; i < matrix.Rows - rowOffset; i += yStride, row++, column = 0)
			for (var j = colOffset; j < matrix.Columns - colOffset; j += xStride, column++)
			{
				double sum = 0;
				for (var c = j - colOffset; c <= j + colOffset; c++)
					sum += cacheMatrix[i, c];

				returnMat[row, column] = sum / space;
			}

			return returnMat;
		}

		/// <summary>
		///     Calculate the sum within a moving box
		/// </summary>
		/// <param name="matrix">self</param>
		/// <param name="rows">box rows</param>
		/// <param name="columns">box columns</param>
		/// <param name="selector">selector for property</param>
		/// <param name="yStride">
		///     stride defaults to 1; must be a multiple of output array size (
		///     <paramref name="matrix.Rows" /> - <paramref name="rows" /> +1)
		/// </param>
		/// <param name="xStride">
		///     stride defaults to 1; must be a multiple of output array size (
		///     <paramref name="matrix.Columns" /> - <paramref name="columns" /> +1)
		/// </param>
		/// <returns>
		///     new <see cref="Matrix{TElement}" /> with size: (<paramref name="matrix.Rows" /> - <paramref name="rows" /> +1)
		///     x (<paramref name="matrix.Columns" /> - <paramref name="columns" /> +1) containing the sum value of the box
		/// </returns>
		public static Matrix<double> RectBoxedSum<TType>(this Matrix<TType> matrix, int rows, int columns, Expression<Func<TType, double>> selector, int yStride = 1, int xStride = 1, int maxDegreeOfParallelism = 1, CancellationToken cancellationToken = default)
		{
			CalculateMatrixParameters(matrix, rows, columns, yStride, xStride, out var rowOffset, out var colOffset, out var remainingRows, out var remainingColumns);

			var cacheMatrix = CalculateRowCacheMatrix(matrix, selector, rowOffset);

			var returnMat = new Matrix<double>(remainingRows, remainingColumns);

			RowCycle(rowOffset, matrix.Rows - rowOffset - 1, colOffset, matrix.Columns - colOffset - 1, yStride, xStride, maxDegreeOfParallelism, InnerCycle, cancellationToken);

			void InnerCycle(int iRow, int row, int iColumn, int column)
			{
				double sum = 0;
				for (var c = iColumn - colOffset; c <= iColumn + colOffset; c++)
					sum += cacheMatrix[iRow, c];

				returnMat[row, column] = sum;
			}

			return returnMat;
		}

		public delegate void InnerCycle(int iRow, int row, int iColumn, int column);

		private static void ColumnCycle(int fromInclusive, int toInclusive, int iRow, int row, int xStride, InnerCycle innerCycle)
		{
			var column = 0;
			for (var iColumn = fromInclusive; iColumn <= toInclusive; iColumn += xStride, column++)
			{
				innerCycle(iRow, row, iColumn, column);
			}
		}

		private static void RowCycle(int rowFromInclusive, int rowToInclusive, int columnFromInclusive, int columnToInclusive, int yStride, int xStride, int maxDegreeOfParallelism, InnerCycle innerCycle, CancellationToken cancellationToken = default)
		{
			OutOfRangeException.Check(maxDegreeOfParallelism, 0);

			var iRows = new List<int>();
			for (var iRow = rowFromInclusive; iRow <= rowToInclusive; iRow += yStride)
				iRows.Add(iRow);

			if (maxDegreeOfParallelism > 1)
			{
				Parallel.ForEach(iRows,
					new ParallelOptions
					{
						MaxDegreeOfParallelism = maxDegreeOfParallelism,
						CancellationToken = cancellationToken
					},
					iRow =>
					{
						var row = (iRow - rowFromInclusive) / yStride;
						ColumnCycle(columnFromInclusive, columnToInclusive, iRow, row, xStride, innerCycle);
					});
			}
			else
			{
				var row = 0;
				foreach (var iRow in iRows)
				{
					ColumnCycle(columnFromInclusive, columnToInclusive, iRow, row, xStride, innerCycle);
					row++;
				}
			}
		}

		/// <summary>
		///     Calculates a cache matrix to speedup algorithms to avoid multiple calculation of the same fields
		/// </summary>
		private static Matrix<double> CalculateRowCacheMatrix<TType>(Matrix<TType> matrix, Expression<Func<TType, double>> selector, int rowOffset)
		{
			var cacheMatrix = new Matrix<double>(matrix.Rows, matrix.Columns);

			var func = selector.Compile();

			for (var i = rowOffset; i < matrix.Rows - rowOffset; i++)
			for (var j = 0; j < matrix.Columns; j++)
			for (var r = i - rowOffset; r <= i + rowOffset; r++)
				cacheMatrix[i, j] += func(matrix[r, j]!);
			return cacheMatrix;
		}

		/// <summary>
		///     Check if parameters are valid and calculate important parameters for the algorithm to work
		/// </summary>
		/// <param name="matrix"></param>
		/// <param name="rows">box rows</param>
		/// <param name="columns">box columns</param>
		/// <param name="yStride">steps in y direction</param>
		/// <param name="xStride">steps in x direction</param>
		/// <param name="rowOffset">top and bottom space from box</param>
		/// <param name="colOffset">left and right space from box</param>
		/// <param name="remainingRows">new height of matrix</param>
		/// <param name="remainingColumns">new width of matrix</param>
		private static void CalculateMatrixParameters<TType>(Matrix<TType> matrix, int rows, int columns, int yStride, int xStride, out int rowOffset, out int colOffset, out int remainingRows, out int remainingColumns)
		{
			EvenRowsException.Check(rows);
			EvenColumnsException.Check(columns);

			OutOfRangeException.Check(columns, 0, matrix.Columns);
			OutOfRangeException.Check(rows, 0, matrix.Rows);

			OutOfRangeException.Check(xStride, 0);
			OutOfRangeException.Check(yStride, 0);

			if (yStride > rows)
				throw new MatrixException("yStride must be <= rows");

			if (xStride > columns)
				throw new MatrixException("xStride must be <= columns");


			rowOffset = (rows - 1) / 2;
			colOffset = (columns - 1) / 2;

			remainingRows = matrix.Rows - 2 * rowOffset;
			remainingColumns = matrix.Columns - 2 * colOffset;

			if (remainingRows % yStride != 0)
				throw new MatrixException("rows must be divisible by stride");

			if (remainingColumns % xStride != 0)
				throw new MatrixException("columns must be divisible by stride");

			remainingRows /= yStride;
			remainingColumns /= xStride;
		}
	}
}