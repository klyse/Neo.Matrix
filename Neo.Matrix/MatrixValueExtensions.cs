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
	}
}