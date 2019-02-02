namespace NeoMatrix
{
	public static class MatrixExtensions
	{
		/// <summary>
		/// Transpose Matrix (change x / y)
		/// </summary>
		/// <param name="m">input matrix</param>
		/// <returns></returns>
		public static Matrix<TElement> Transpose<TElement>(Matrix<TElement> m)
		{
			var t = new Matrix<TElement>(m.Cols, m.Rows);

			for (var i = 0; i < m.Rows; i++)
			for (var j = 0; j < m.Cols; j++)
				t[j, i] = m[i, j];

			return t;
		}
	}
}