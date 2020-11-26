using System.Drawing;
using NeoMatrix.Exceptions;

namespace NeoMatrix
{
	/// <summary>
	///     Rectangle Extensions
	/// </summary>
	public static class RegionHelper
	{
		/// <summary>
		///     Create rectangle from top left coordinates and height / width
		/// </summary>
		/// <param name="y">top row</param>
		/// <param name="x">left column</param>
		/// <param name="height">total height</param>
		/// <param name="width">total width</param>
		/// <returns>a new rectangle</returns>
		public static Rectangle FromTopLeft(int y, int x, int height, int width)
		{
			var region = new Rectangle(x, y, width, height);

			return region;
		}

		/// <summary>
		///     Create rectangle from center coordinates and height / width
		/// </summary>
		/// <param name="y">center row</param>
		/// <param name="x">center column</param>
		/// <param name="rows">total height</param>
		/// <param name="columns">total width</param>
		/// <returns>a new rectangle</returns>
		public static Rectangle FromCenter(int y, int x, int rows, int columns)
		{
			EvenRowsException.Check(rows);
			EvenColumnsException.Check(columns);

			OutOfRangeException.Check(rows, 1);
			OutOfRangeException.Check(columns, 1);

			var relHeight = (rows - 1) / 2;
			var relWidth = (columns - 1) / 2;

			var top = y - relHeight;
			var left = x - relWidth;

			var region = new Rectangle(left, top, columns, rows);

			return region;
		}
	}
}