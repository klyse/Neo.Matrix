using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NeoMatrix
{
	/// <summary>
	/// Neo.Matrix
	/// </summary>
	/// <typeparam name="TElement">type of content</typeparam>
	public class Matrix<TElement> : IEqualityComparer<Matrix<TElement>>
	{
		/// <summary>
		/// Total Rows
		/// </summary>
		public int Rows { get; }

		/// <summary>
		/// Total Columns
		/// </summary>
		public int Columns { get; }

		/// <summary>
		/// Total elements count.
		/// 
		/// <see cref="Rows"/> * <see cref="Columns"/>
		/// </summary>
		public int TotalCount => Rows * Columns;

		/// <summary>
		/// Matrix
		/// </summary>
		public readonly TElement[,] Mat;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="rows"></param>
		/// <param name="columns"></param>
		public Matrix(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
			Mat = new TElement[Rows, Columns];
		}

		/// <summary>
		/// Create new matrix of element
		/// </summary>
		/// <param name="rows">rows</param>
		/// <param name="columns">columns</param>
		/// <param name="f">func to create new element</param>
		public static Matrix<TElement> NewMatrix(int rows, int columns, Func<TElement> f)
		{
			var matrix = new Matrix<TElement>(rows, columns);

			for (var i = 0; i < rows; i++)
			for (var j = 0; j < columns; j++)
				matrix[i, j] = f.Invoke();

			return matrix;
		}

		/// <summary>
		/// Constructor
		/// <paramref name="data"/> if formatted: Dimension 0 is Rows, Dimension 1 is Columns
		/// </summary>
		/// <param name="data">Data array</param>
		public Matrix(TElement[,] data)
		{
			Rows = data.GetLength(0);
			Columns = data.GetLength(1);

			Mat = new TElement[Rows, Columns];

			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Columns; j++)
				Mat[i, j] = data[i, j];
		}

		/// <summary>
		/// Square Matrix
		/// </summary>
		public bool IsSquare()
		{
			return Rows == Columns;
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
			for (var i = 0; i < Columns; i++)
				yield return Mat[k, i];
		}

		/// <summary>
		/// Get Uni Dimensional Matrix view
		/// </summary>
		public IEnumerable<TElement> GetFlat()
		{
			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Columns; j++)
				yield return Mat[i, j];
		}

		/// <summary>
		/// Matrix shallow copy
		/// </summary>
		/// <returns>copy of this matrix</returns>
		public Matrix<TElement> Duplicate()
		{
			var matrix = new Matrix<TElement>(Rows, Columns);
			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Columns; j++)
				matrix[i, j] = Mat[i, j];
			return matrix;
		}

		/// <summary>
		/// Transpose Matrix (change x / y)
		/// </summary>
		/// <returns>x / y inverted copy of matrix</returns>
		public Matrix<TElement> Transpose()
		{
			var t = new Matrix<TElement>(Columns, Rows);

			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Columns; j++)
				t[j, i] = Mat[i, j];

			return t;
		}


		/// <summary>
		/// Get value one row above
		/// </summary>
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
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public TElement GetRight(int row, int column)
		{
			return Mat[row, column + 1];
		}

		/// <summary>
		/// Returns matrix subset of a given region
		/// </summary>
		/// <param name="region">region</param>
		/// <returns>Matrix according dimensions from region</returns>
		public Matrix<TElement> GetFromRegion(Region region)
		{
			var t = new Matrix<TElement>(region.Height, region.Width);

			var r = 0;
			var c = 0;
			for (var i = region.Top; i < region.Bottom; i++, r++, c = 0)
			for (var j = region.Left; j < region.Right; j++, c++)
				t[r, c] = Mat[i, j];

			return t;
		}

		/// <summary>
		/// Returns a rectangle sized matrix of elements around the center
		/// </summary>
		/// <param name="centerRow">center row</param>
		/// <param name="centerColumn">center column</param>
		/// <param name="rows">rows of rectangle</param>
		/// <param name="columns">columns of rectangle</param>
		public Matrix<TElement> GetRect(int centerRow, int centerColumn, int rows, int columns)
		{
			var reg = Region.FromCenter(centerRow, centerColumn, rows, columns);

			return GetFromRegion(reg);
		}

		/// <summary>
		/// Returns a rectangle sized matrix of elements around the center
		/// </summary>
		/// <param name="rect">rectangle</param>
		public Matrix<TElement> GetRect(Rectangle rect)
		{
			return GetRect(rect.Y, rect.X, rect.Height, rect.Width);
		}

		/// <summary>
		/// Returns an even sized box of elements around the center
		/// </summary>
		/// <param name="centerRow">center row</param>
		/// <param name="centerColumn">center column</param>
		/// <param name="size">size of return array</param>
		public Matrix<TElement> GetBox(int centerRow, int centerColumn, int size)
		{
			return GetRect(centerRow, centerColumn, size, size);
		}

		/// <summary>
		/// Returns an even sized box of elements around the center
		/// </summary>
		/// <param name="pt">center point</param>
		/// <param name="size">size of return array</param>
		public Matrix<TElement> GetBox(Point pt, int size)
		{
			return GetBox(pt.Y, pt.X, size);
		}

		/// <summary>
		/// Executes an <see cref="Action{T}"/> on every element
		/// </summary>
		/// <param name="f">Action function</param>
		public void ExecuteOnAll(Action<TElement> f)
		{
			var flatMatrix = GetFlat();

			foreach (var element in flatMatrix)
			{
				f.Invoke(element);
			}
		}

		/// <summary>
		/// Executes an <see cref="Action{T}"/> on every element
		/// </summary>
		/// <param name="f">Action element, row, column</param>
		public void ExecuteOnAll(Action<TElement, int, int> f)
		{
			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Columns; j++)
				f.Invoke(Mat[i, j], i, j);
		}

		/// <summary>
		/// Checks if current instance is to another
		/// </summary>
		/// <param name="obj">instance to compare</param>
		/// <returns>true if equal, false if not</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals(this, (Matrix<TElement>)obj);
		}

		/// <inheritdoc />
		/// <summary>
		/// Checks if tow matrix are same
		/// </summary>
		/// <param name="a">first matrix</param>
		/// <param name="b">second matrix</param>
		/// <returns></returns>
		public bool Equals(Matrix<TElement> a, Matrix<TElement> b)
		{
			if (b is null || a is null)
				return false;

			return a.GetHashCode() == b.GetHashCode();
		}

		/// <summary>
		/// Get Hashcode for matrix. Every element in the array is evaluated.
		/// </summary>
		/// <returns>HashCode</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				int MatHashCode()
				{
					var flatView = GetFlat().ToList();
					var len = flatView.Count;
					var hc = len;
					for (var i = 0; i < len; ++i)
					{
						hc = unchecked(hc * 314159 + flatView[i].GetHashCode());
					}

					return hc;
				}

				var hashCode = (Mat != null ? MatHashCode() : 0);
				hashCode = (hashCode * 397) ^ Rows;
				hashCode = (hashCode * 397) ^ Columns;
				return hashCode;
			}
		}

		/// <inheritdoc />
		/// <summary>
		/// Gets Hashcode for matrix object
		/// </summary>
		/// <param name="obj">hashed object</param>
		/// <returns>hashcode</returns>
		public int GetHashCode(Matrix<TElement> obj)
		{
			return obj.GetHashCode();
		}
	}
}