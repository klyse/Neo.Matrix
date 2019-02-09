using System;

namespace NeoMatrix
{
	public static class MatrixFilter
	{
		public static Matrix<double> RectSumFilter<TMatrixValueType>(this Matrix<TMatrixValueType> matrix, int rows, int columns) where TMatrixValueType : IMatrixValue<double>
		{
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
				returnMat[i - rowOffset, j - colOffset] = matrix.GetRect(i, j, columns, rows).GetTotalSum();
			}

			return returnMat;
		}
	}
}