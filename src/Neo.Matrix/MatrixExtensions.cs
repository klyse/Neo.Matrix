﻿using System;
using System.Drawing;
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

			var v = matrix.GetFlat().Sum(c => func(c!));
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
			var avg = matrix.GetFlat().Average(c => func(c!));

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
			var minV = matrix.GetFlat().Min(c => func(c!));
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
			var maxV = matrix.GetFlat().Max(c => func(c!));
			return maxV;
		}

		/// <summary>
		///     Matrix to bitmap
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <param name="selector">selector</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>max of matrix</returns>
		public static Bitmap ToBitmap<TMatrixValueType>(this Matrix<TMatrixValueType> matrix, Expression<Func<TMatrixValueType, double>> selector)
		{
			var bmp = new Bitmap(matrix.Columns, matrix.Rows);
			var func = selector.Compile();

			var max = matrix.Max(selector);
			var min = matrix.Min(selector);

			var offset = Math.Abs(max) + (min >= 0 ? 0 : Math.Abs(min));
			var delta = 255 / offset;

			matrix.ExecuteOnAll((t, r, c) =>
			{
				var val = 255 - (int) (delta * func(t!));
				bmp.SetPixel(c, r, Color.FromArgb(val, val, val));
			});
			return bmp;
		}

		/// <summary>
		///     Matrix to bitmap
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <param name="color">color</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>max of matrix</returns>
		public static Bitmap ToBitmap<TMatrixValueType>(this Matrix<TMatrixValueType> matrix, Func<TMatrixValueType, Color> color)
		{
			var bmp = new Bitmap(matrix.Columns, matrix.Rows);

			matrix.ExecuteOnAll((t, r, c) => { bmp.SetPixel(c, r, color(t!)); });
			return bmp;
		}

		/// <summary>
		///     Matrix to bitmap
		/// </summary>
		/// <param name="matrix">current matrix</param>
		/// <param name="color">color</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		/// <returns>max of matrix</returns>
		public static Bitmap ToBitmap<TMatrixValueType>(this Matrix<TMatrixValueType> matrix, Func<int, int, TMatrixValueType, Color> color)
		{
			var bmp = new Bitmap(matrix.Columns, matrix.Rows);

			matrix.ExecuteOnAll((t, r, c) => { bmp.SetPixel(c, r, color(r, c, t!)); });
			return bmp;
		}

		/// <summary>
		///     Read bitmap and convert it to Matrix
		/// </summary>
		/// <param name="bitmap">source bitmap</param>
		/// <param name="convertor">converter function</param>
		/// <typeparam name="TMatrixValueType">Matrix element type</typeparam>
		public static Matrix<TMatrixValueType> ToMatrix<TMatrixValueType>(this Bitmap bitmap, Func<int, int, Color, TMatrixValueType> convertor)
		{
			var matrix = new Matrix<TMatrixValueType>(bitmap.Height, bitmap.Width);

			matrix.ExecuteOnAll((_, r, c) => { matrix[r, c] = convertor(r, c, bitmap.GetPixel(c, r)); });

			return matrix;
		}
	}
}