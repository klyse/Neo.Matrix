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
		/// <returns>sum of matrix</returns>
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
		/// <returns>sum of matrix</returns>
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
		/// <returns>sum of matrix</returns>
		public static double GetMax<TMatrixValueType>(this Matrix<TMatrixValueType> matrix) where TMatrixValueType : IMatrixValue<double>
		{
			double maxV = double.MinValue;

			matrix.ExecuteOnAll(c => { maxV = Math.Max(maxV, c.GetValueObject()); });
			return maxV;
		}
	}
}