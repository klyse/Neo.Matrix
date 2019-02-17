using System.Linq;

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
		public static double Sum<TMatrixValueType>(this Matrix<TMatrixValueType> matrix) where TMatrixValueType : IMatrixValue<double>
		{
			var v = matrix.GetFlat().Sum(c => c.GetValue());
			return v;
		}

		/// <summary>
		/// Calculates average of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>average of matrix</returns>
		public static double Average<TMatrixValueType>(this Matrix<TMatrixValueType> matrix) where TMatrixValueType : IMatrixValue<double>
		{
			var avg = matrix.GetFlat().Average(c => c.GetValue());

			return avg;
		}

		/// <summary>
		/// Calculates min of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>min of matrix</returns>
		public static double Min<TMatrixValueType>(this Matrix<TMatrixValueType> matrix) where TMatrixValueType : IMatrixValue<double>
		{
			var minV = matrix.GetFlat().Min(c => c.GetValue());
			return minV;
		}

		/// <summary>
		/// Calculates max of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>max of matrix</returns>
		public static double Max<TMatrixValueType>(this Matrix<TMatrixValueType> matrix) where TMatrixValueType : IMatrixValue<double>
		{
			var maxV = matrix.GetFlat().Max(c => c.GetValue());
			return maxV;
		}
	}
}