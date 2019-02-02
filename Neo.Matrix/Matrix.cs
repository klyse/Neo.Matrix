using System;
using System.Collections.Generic;

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

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="data">Data array</param>
		public Matrix(TElement[,] data)
		{
			Rows = data.GetLength(0);
			Cols = data.GetLength(1);

			Mat = new TElement[Rows, Cols];

			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Cols; j++)
				Mat[i, j] = data[i, j];
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
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public TElement this[int row, int col]
		{
			get => Mat[row, col];
			set => Mat[row, col] = value;
		}

		/// <summary>
		/// Get Matrix for one column
		/// </summary>
		/// <param name="k">column index</param>
		public IEnumerable<TElement> GetCol(int k)
		{
			for (var i = 0; i < Rows; i++)
				yield return Mat[i, k];
		}

		/// <summary>
		/// Get Matrix for one row
		/// </summary>
		/// <param name="k">row index</param>
		public IEnumerable<TElement> GetRow(int k)
		{
			for (var i = 0; i < Cols; i++)
				yield return Mat[k, i];
		}

		/// <summary>
		/// Get Uni Dimensional Matrix view
		/// </summary>
		public IEnumerable<TElement> GetUniDimensionalView()
		{
			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Cols; j++)
				yield return Mat[i, j];
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
	}
}