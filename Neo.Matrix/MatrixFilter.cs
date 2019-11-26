﻿using System;

namespace NeoMatrix
{
	/// <summary>
	/// Matrix Filter class
	/// </summary>
	public static class MatrixFilter
	{
		/// <summary>
		/// Sum of a rectangle
		/// </summary>
		/// <param name="matrix">matrix</param>
		/// <param name="rows">rows</param>
		/// <param name="columns">columns</param>
		/// <returns>matrix with results</returns>
		public static Matrix<double> RectSumFilter<TType>(this Matrix<TType> matrix, int rows, int columns)
		{
			return null;}
		public static Matrix<double> RectSumFilter(this Matrix<double> matrix, int rows, int columns)
		{
			// k.p. better api design required
			if (rows % 2 == 0 || columns % 2 == 0)
			{
				throw new Exception("Matrix rectangle rows or columns are even.");
			}

			if (columns > matrix.Columns - 1 ||
				rows > matrix.Rows - 1 ||
				columns <= 0 || rows <= 0)
			{
				throw new IndexOutOfRangeException("Rectangle is not in valid range.");
			}

			var colOffset = (columns - 1) / 2;
			var rowOffset = (rows - 1) / 2;

			var resCols = matrix.Columns - 2 * colOffset;
			var resRows = matrix.Rows - 2 * rowOffset;

			var returnMat = new Matrix<double>(resRows, resCols);


			for (var i = rowOffset; i < matrix.Rows - rowOffset; i++)
			for (var j = colOffset; j < matrix.Columns - colOffset; j++)
			{
				returnMat[i - rowOffset, j - colOffset] = matrix.GetRect(i, j, columns, rows).Sum();
			}

			return returnMat;
		}
	}
}