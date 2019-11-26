using System;
using System.Linq;
using System.Linq.Expressions;

namespace NeoMatrix
{
	/// <summary>
	///     Matrix Value Extension helper
	/// </summary>
	public static class MatrixExtensions
	{
		/// <summary>
		///     Calculates total sum of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <param name="selector">selector</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>sum of matrix</returns>
		public static double Sum<TMatrixValueType>(this Matrix<TMatrixValueType> matrix, Expression<Func<TMatrixValueType, double>> selector)
		{
			var func = selector.Compile();

			var v = matrix.GetFlat().Sum(c => func(c));
			return v;
		}

		/// <summary>
		///     Calculates average of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <param name="selector">selector</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>average of matrix</returns>
		public static double Average<TMatrixValueType>(this Matrix<TMatrixValueType> matrix, Expression<Func<TMatrixValueType, double>> selector)
		{
			var func = selector.Compile();
			var avg = matrix.GetFlat().Average(c => func(c));

			return avg;
		}

		/// <summary>
		///     Calculates min of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <param name="selector">selector</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>min of matrix</returns>
		public static double Min<TMatrixValueType>(this Matrix<TMatrixValueType> matrix, Expression<Func<TMatrixValueType, double>> selector)
		{
			var func = selector.Compile();
			var minV = matrix.GetFlat().Min(c => func(c));
			return minV;
		}

		/// <summary>
		///     Calculates max of matrix
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <param name="selector">selector</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>max of matrix</returns>
		public static double Max<TMatrixValueType>(this Matrix<TMatrixValueType> matrix, Expression<Func<TMatrixValueType, double>> selector)
		{
			var func = selector.Compile();
			var maxV = matrix.GetFlat().Max(c => func(c));
			return maxV;
		}
	}
}