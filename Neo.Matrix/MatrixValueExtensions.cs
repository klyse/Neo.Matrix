using System;

namespace NeoMatrix
{
	/// <summary>
	/// Matrix Value Extension helper
	/// </summary>
	public static class MatrixValueExtensions
	{
		/// <summary>
		/// Calculates total sum of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>sum of matrix</returns>
		public static double GetTotalSum<TMatrixValueType>(this Matrix<TMatrixValueType> matrix) where TMatrixValueType : IMatrixValue<double>
		{
			double v = 0;
			matrix.ExecuteOnAll(c => { v += c.GetValueObject(); });

			return v;
		}

		/// <summary>
		/// Calculates average of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>average of matrix</returns>
		public static double GetAverage<TMatrixValueType>(this Matrix<TMatrixValueType> matrix) where TMatrixValueType : IMatrixValue<double>
		{
			var totalSum = matrix.GetTotalSum();

			return totalSum / (matrix.Cols * matrix.Rows);
		}

		/// <summary>
		/// Calculates min of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>min of matrix</returns>
		public static double GetMin<TMatrixValueType>(this Matrix<TMatrixValueType> matrix) where TMatrixValueType : IMatrixValue<double>
		{
			double minV = double.MaxValue;

			matrix.ExecuteOnAll(c => { minV = Math.Min(minV, c.GetValueObject()); });
			return minV;
		}

		/// <summary>
		/// Calculates max of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>max of matrix</returns>
		public static double GetMax<TMatrixValueType>(this Matrix<TMatrixValueType> matrix) where TMatrixValueType : IMatrixValue<double>
		{
			double maxV = double.MinValue;

			matrix.ExecuteOnAll(c => { maxV = Math.Max(maxV, c.GetValueObject()); });
			return maxV;
		}

		public static Matrix<double> GetRectSum<TMatrixValueType>(this Matrix<TMatrixValueType> matrix, int rows, int columns) where TMatrixValueType : IMatrixValue<double>
		{
			if (columns % 2 == 0 || columns % 2 == 0 ||
				rows > matrix.Cols - 1 || rows > matrix.Rows - 1 ||
				columns <= 0 || rows <= 0)
			{
				throw new IndexOutOfRangeException("Rectangle is not in valid range.");
			}

			var colOffset = (columns - 1) / 2;
			var rowOffset = (rows - 1) / 2;

			var resCols = matrix.Cols - 2 * colOffset;
			var resRows = matrix.Rows - 2 * rowOffset;

			var returnMat = new Matrix<double>(resRows, resCols);


			for (var i = rowOffset; i < matrix.Rows - rowOffset; i++)
			for (var j = colOffset; j < matrix.Cols - colOffset; j++)
			{
				returnMat[i - rowOffset] = matrix.GetRect(i, j, columns, rows).GetTotalSum();
			}

			return returnMat;
		}

		public static Matrix<double> GetBoxSum<TMatrixValueType>(this Matrix<TMatrixValueType> matrix, int box) where TMatrixValueType : IMatrixValue<double>
		{
			return matrix.GetRectSum(box, box);
		}
	}
}