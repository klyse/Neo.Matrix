using System;

namespace NeoMatrix
{
	public class Matrix<TElement>
	{
		public int Rows { get; }
		public int Cols { get; }
		public readonly TElement[,] Mat;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="rows"></param>
		/// <param name="cols"></param>
		public Matrix(int rows, int cols)
		{
			Rows = rows;
			Cols = cols;
			Mat = new TElement[Rows, Cols];
		}

		/// <summary>
		/// Square Matrix
		/// </summary>
		public bool IsSquare()
		{
			return Rows == Cols;
		}

		/// <summary>
		/// Access matrix as 2D array
		/// </summary>
		/// <param name="iRow"></param>
		/// <param name="iCol"></param>
		/// <returns></returns>
		public TElement this[int iRow, int iCol]
		{
			get => Mat[iRow, iCol];
			set => Mat[iRow, iCol] = value;
		}

		/// <summary>
		/// Get Matrix for one column
		/// </summary>
		/// <param name="k">column index</param>
		public Matrix<TElement> GetCol(int k)
		{
			var m = new Matrix<TElement>(Rows, 1);
			for (var i = 0; i < Rows; i++) m[i, 0] = Mat[i, k];
			return m;
		}

		/// <summary>
		/// Copy column from input matrix to this matrix
		/// </summary>
		/// <param name="v">input matrix</param>
		/// <param name="k">column</param>
		public void SetCol(Matrix<TElement> v, int k)
		{
			for (var i = 0; i < Rows; i++)
				Mat[i, k] = v[i, 0];
		}

		/// <summary>
		/// Matrix shallow copy
		/// </summary>
		/// <returns>copy of this matrix</returns>
		public Matrix<TElement> Duplicate()
		{
			var matrix = new Matrix<TElement>(Rows, Cols);
			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Cols; j++)
				matrix[i, j] = Mat[i, j];
			return matrix;
		}

		/// <summary>
		/// Create new matrix of 0
		/// </summary>
		/// <param name="rows">rows</param>
		/// <param name="cols">columns</param>
		/// <param name="f">func to create zero element</param>
		public static Matrix<TElement> ZeroMatrix(int rows, int cols, Func<TElement> f)
		{
			var matrix = new Matrix<TElement>(rows, cols);

			for (var i = 0; i < rows; i++)
			for (var j = 0; j < cols; j++)
				matrix[i, j] = f.Invoke();

			return matrix;
		}
	}
}