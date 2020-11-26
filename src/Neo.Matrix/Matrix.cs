using System;
using System.Collections.Generic;
using System.Drawing;

namespace NeoMatrix
{
	/// <summary>
	///     Neo.Matrix
	/// </summary>
	/// <typeparam name="TElement">type of content</typeparam>
	public record Matrix<TElement> : IEqualityComparer<Matrix<TElement>>, IEquatable<Matrix<TElement>>
	{
		/// <summary>
		///     Matrix
		/// </summary>
		private readonly TElement?[,] _mat;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="rows"></param>
		/// <param name="columns"></param>
		public Matrix(int rows, int columns)
		{
			_mat = new TElement[rows, columns];
		}

		/// <summary>
		///     Constructor
		///     <paramref name="data" /> if formatted: Dimension 0 is Rows, Dimension 1 is Columns
		/// </summary>
		/// <param name="data">Data array</param>
		public Matrix(TElement[,] data)
		{
			_mat = new TElement[data.GetLength(0), data.GetLength(1)];

			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Columns; j++)
				_mat[i, j] = data[i, j];
		}

		/// <summary>
		///     Total Rows
		/// </summary>
		public int Rows => _mat.GetLength(0);

		/// <summary>
		///     Total Columns
		/// </summary>
		public int Columns => _mat.GetLength(1);

		/// <summary>
		///     Access matrix as 2D array
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public TElement? this[int row, int col]
		{
			get => _mat[row, col];
			set => _mat[row, col] = value;
		}

		/// <inheritdoc />
		/// <summary>
		///     Checks if tow matrix are same
		/// </summary>
		/// <param name="a">first matrix</param>
		/// <param name="b">second matrix</param>
		/// <returns></returns>
		public bool Equals(Matrix<TElement>? a, Matrix<TElement>? b)
		{
			if (b is null || a is null)
				return false;

			return a.Rows == b.Rows &&
			       a.Columns == b.Columns &&
			       a.GetHashCode() == b.GetHashCode();
		}

		/// <inheritdoc />
		/// <summary>
		///     Gets Hashcode for matrix object
		/// </summary>
		/// <param name="obj">hashed object</param>
		/// <returns>hashcode</returns>
		public int GetHashCode(Matrix<TElement> obj)
		{
			return obj.GetHashCode();
		}

		/// <summary>
		///     Checks if current instance is to another
		/// </summary>
		/// <param name="other">instance to compare</param>
		/// <returns>true if equal, false if not</returns>
		public virtual bool Equals(Matrix<TElement>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(this, other);
		}

		/// <summary>
		///     Create new matrix of element
		/// </summary>
		/// <param name="rows">rows</param>
		/// <param name="columns">columns</param>
		/// <param name="f">func to create new element</param>
		public static Matrix<TElement> NewMatrix(int rows, int columns, Func<TElement> f)
		{
			return NewMatrix(rows, columns, (_, _) => f());
		}

		/// <summary>
		///     Create new matrix of element
		/// </summary>
		/// <param name="rows">rows</param>
		/// <param name="columns">columns</param>
		/// <param name="f">func to create new element, row, column return value</param>
		public static Matrix<TElement> NewMatrix(int rows, int columns, Func<int, int, TElement> f)
		{
			var matrix = new Matrix<TElement>(rows, columns);

			for (var i = 0; i < rows; i++)
			for (var j = 0; j < columns; j++)
				matrix[i, j] = f(i, j);

			return matrix;
		}

		/// <summary>
		///     Create new matrix of element
		/// </summary>
		/// <param name="rows">rows</param>
		/// <param name="columns">columns</param>
		/// <param name="filler">fill up empty space with this value</param>
		public static Matrix<TElement> NewMatrix(int rows, int columns, TElement? filler)
		{
			var matrix = new Matrix<TElement>(rows, columns);

			for (var i = 0; i < rows; i++)
			for (var j = 0; j < columns; j++)
				matrix[i, j] = filler;

			return matrix;
		}

		/// <summary>
		///     Square Matrix
		/// </summary>
		public bool IsSquare()
		{
			return Rows == Columns;
		}

		/// <summary>
		///     Get Matrix for one column
		/// </summary>
		/// <param name="k">column index</param>
		public IEnumerable<TElement?> GetCol(int k)
		{
			for (var i = 0; i < Rows; i++)
				yield return this[i, k];
		}

		/// <summary>
		///     Get Matrix for one row
		/// </summary>
		/// <param name="k">row index</param>
		public IEnumerable<TElement?> GetRow(int k)
		{
			for (var i = 0; i < Columns; i++)
				yield return _mat[k, i];
		}

		/// <summary>
		///     Get Uni Dimensional Matrix view
		/// </summary>
		public IEnumerable<TElement?> GetFlat()
		{
			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Columns; j++)
				yield return this[i, j];
		}

		/// <summary>
		///     Matrix shallow copy
		/// </summary>
		/// <returns>copy of this matrix</returns>
		public Matrix<TElement> Duplicate()
		{
			var matrix = new Matrix<TElement>(Rows, Columns);
			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Columns; j++)
				matrix[i, j] = this[i, j];
			return matrix;
		}

		/// <summary>
		///     Transpose Matrix (change x / y)
		/// </summary>
		/// <returns>x / y inverted copy of matrix</returns>
		public Matrix<TElement> Transpose()
		{
			var t = new Matrix<TElement>(Columns, Rows);

			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Columns; j++)
				t[j, i] = this[i, j];

			return t;
		}


		/// <summary>
		///     Get value one row above
		/// </summary>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public TElement? GetAbove(int row, int column)
		{
			return this[row - 1, column];
		}

		/// <summary>
		///     Get value one row below
		/// </summary>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public TElement? GetBelow(int row, int column)
		{
			return this[row + 1, column];
		}

		/// <summary>
		///     Get value one column left
		/// </summary>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public TElement? GetLeft(int row, int column)
		{
			return this[row, column - 1];
		}

		/// <summary>
		///     Get value one column right
		/// </summary>
		/// <param name="row">current row</param>
		/// <param name="column">current column</param>
		/// <returns></returns>
		public TElement? GetRight(int row, int column)
		{
			return this[row, column + 1];
		}

		/// <summary>
		///     Returns a rectangle sized matrix of elements around the center
		/// </summary>
		/// <param name="region">region</param>
		/// <returns>Matrix according dimensions from region</returns>
		public Matrix<TElement> GetRect(Rectangle region)
		{
			var t = new Matrix<TElement>(region.Height, region.Width);

			var r1 = 0;
			var c1 = 0;
			for (var r2 = region.Top; r2 < region.Bottom; r2++, r1++, c1 = 0)
			for (var c2 = region.Left; c2 < region.Right; c2++, c1++)
				t[r1, c1] = this[r2, c2];

			return t;
		}

		/// <summary>
		///     Returns a rectangle sized matrix of elements around the center
		/// </summary>
		/// <param name="centerRow">center row</param>
		/// <param name="centerColumn">center column</param>
		/// <param name="rows">rows of rectangle</param>
		/// <param name="columns">columns of rectangle</param>
		public Matrix<TElement> GetRect(int centerRow, int centerColumn, int rows, int columns)
		{
			var reg = RegionHelper.FromCenter(centerRow, centerColumn, rows, columns);

			return GetRect(reg);
		}

		/// <summary>
		///     Returns an even sized box of elements around the center
		/// </summary>
		/// <param name="centerRow">center row</param>
		/// <param name="centerColumn">center column</param>
		/// <param name="size">size of return array</param>
		public Matrix<TElement> GetBox(int centerRow, int centerColumn, int size)
		{
			return GetRect(centerRow, centerColumn, size, size);
		}

		/// <summary>
		///     Returns an even sized box of elements around the center
		/// </summary>
		/// <param name="pt">center point</param>
		/// <param name="size">size of return array</param>
		public Matrix<TElement> GetBox(Point pt, int size)
		{
			return GetBox(pt.Y, pt.X, size);
		}

		/// <summary>
		///     Add padding around matrix
		/// </summary>
		/// <param name="padding">for all sides</param>
		/// <param name="filler">fill padding space up with this value</param>
		public Matrix<TElement> AddPadding(int padding, TElement? filler = default)
		{
			return AddPadding(padding, padding, padding, padding, filler);
		}

		/// <summary>
		///     Add padding around matrix
		/// </summary>
		/// <param name="top">padding top</param>
		/// <param name="right">padding right</param>
		/// <param name="bottom">padding bottom</param>
		/// <param name="left">padding left</param>
		/// <param name="filler">fill padding space up with this value</param>
		public Matrix<TElement> AddPadding(int top, int right, int bottom, int left, TElement? filler = default)
		{
			var columns = Columns + left + right;
			var rows = Rows + top + bottom;

			var mat = NewMatrix(rows, columns, filler);

			var r1 = top;
			var c1 = left;
			for (var r0 = 0; r0 < Rows; r0++, r1++, c1 = left)
			for (var c0 = 0; c0 < Columns; c0++, c1++)
				mat[r1, c1] = this[r0, c0];

			return mat;
		}

		/// <summary>
		///     Executes an <see cref="Action{T}" /> on every element
		/// </summary>
		/// <param name="f">Action function</param>
		public Matrix<TElement> ExecuteOnAll(Action<TElement?> f)
		{
			foreach (var element in GetFlat()) f.Invoke(element);
			return this;
		}

		/// <summary>
		///     Executes an <see cref="Action{T}" /> on every element
		/// </summary>
		/// <param name="f">Action element, row, column</param>
		public Matrix<TElement> ExecuteOnAll(Action<TElement?, int, int> f)
		{
			for (var i = 0; i < Rows; i++)
			for (var j = 0; j < Columns; j++)
				f(this[i, j], i, j);

			return this;
		}

		/// <summary>
		///     Get Hashcode for matrix. Every element in the array is evaluated.
		/// </summary>
		/// <returns>HashCode</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Rows * Columns;

				for (var i = 0; i < Rows; i++)
				for (var j = 0; j < Columns; j++)
					hashCode ^= (16777619 * i + 999999937 * j) ^ this[i, j]?.GetHashCode() ?? i * j; // todo null values???


				hashCode = (hashCode * 397) ^ Rows;
				hashCode = (hashCode * 397) ^ Columns;
				return hashCode;
			}
		}
	}
}