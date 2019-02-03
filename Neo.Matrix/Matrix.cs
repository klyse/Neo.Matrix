using System;
using System.Collections.Generic;
using System.Drawing;

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
		/// Create new matrix of element
		/// </summary>
		/// <param name="rows">rows</param>
		/// <param name="cols">columns</param>
		/// <param name="f">func to create new element</param>
		public static Matrix<TElement> NewMatrix(int rows, int cols, Func<TElement> f)
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
		public IEnumerable<TElement> GetFlat()
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

		/// <summary>
		/// Transpose Matrix (change x / y)
		/// </summary>
		/// <returns>x / y inverted copy of matrix</returns>
		public Matrix<TElement> Transpose()
		{
			var t = new Matrix<TElement>(Cols, Rows);

			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Cols; j++)
				t[j, i] = Mat[i, j];

			return t;
		}


		/// <summary>
		/// Get value one row above
		/// </summary>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public TElement GetAbove(int row, int column)
		{
			return Mat[row - 1, column];
		}

		/// <summary>
		/// Get value one row below
		/// </summary>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public TElement GetBelow(int row, int column)
		{
			return Mat[row + 1, column];
		}

		/// <summary>
		/// Get value one column left
		/// </summary>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public TElement GetLeft(int row, int column)
		{
			return Mat[row, column - 1];
		}

		/// <summary>
		/// Get value one column right
		/// </summary>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public TElement GetRight(int row, int column)
		{
			return Mat[row, column + 1];
		}


		public Matrix<TElement> GetRect(int centerRow, int centerColumn, int width, int height)
		{
			if (height % 2 == 0 || width % 2 == 0||
			    height == 1 || width == 1)
			{
				throw new Exception("Height and or width must be uneven.");
			}

			var firstRow = centerRow - (height - 1) / 2;
			var firstColumn = centerColumn - (width - 1) / 2;

			var lastRow = centerRow + (height - 1) / 2;
			var lastColumn = centerColumn + (width - 1) / 2;

			if (firstColumn < 0 || firstRow < 0 ||
				lastColumn >= Cols || lastRow >= Rows)
			{
				throw new IndexOutOfRangeException("Box size is out of range.");
			}

			var t = new Matrix<TElement>(height, width);

			var r = 0;
			var c = 0;
			for (var i = firstRow; i <= lastRow; i++, r++, c = 0)
			for (var j = firstColumn; j <= lastColumn; j++, c++)
				t[r, c] = Mat[i, j];

			return t;
		}

		public Matrix<TElement> GetRect(Rectangle rect)
		{
			return GetRect(rect.Y, rect.X, rect.Width, rect.Height);
		}
	}
}