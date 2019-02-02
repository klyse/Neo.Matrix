namespace NeoMatrix
{
	public static class MatrixExtensions
	{
		/// <summary>
		/// Transpose Matrix (change x / y)
		/// </summary>
		/// <param name="m">input matrix</param>
		/// <returns></returns>
		public static Matrix<TElement> Transpose<TElement>(this Matrix<TElement> m)
		{
			var t = new Matrix<TElement>(m.Cols, m.Rows);

			for (var i = 0; i < m.Rows; i++)
			for (var j = 0; j < m.Cols; j++)
				t[j, i] = m[i, j];

			return t;
		}

		/// <summary>
		/// Get value one row above
		/// </summary>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="m"></param>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public static TElement GetAbove<TElement>(this Matrix<TElement> m, int row, int column)
		{
			return m[row - 1, column];
		}

		/// <summary>
		/// Get value one row below
		/// </summary>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="m"></param>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public static TElement GetBelow<TElement>(this Matrix<TElement> m, int row, int column)
		{
			return m[row + 1, column];
		}

		/// <summary>
		/// Get value one column left
		/// </summary>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="m"></param>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public static TElement GetLeft<TElement>(this Matrix<TElement> m, int row, int column)
		{
			return m[row, column + 1];
		}

		/// <summary>
		/// Get value one column right
		/// </summary>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="m"></param>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public static TElement GetRight<TElement>(this Matrix<TElement> m, int row, int column)
		{
			return m[row, column - 1];
		}
	}
}