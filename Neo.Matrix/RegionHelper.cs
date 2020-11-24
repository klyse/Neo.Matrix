using System;
using System.Drawing;

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
		/// <param name="row">top row</param>
		/// <param name="column">left column</param>
		/// <param name="height">total height</param>
		/// <param name="width">total width</param>
		/// <returns>a new rectangle</returns>
		public static Rectangle FromTopLeft(int row, int column, int height, int width)
		{
			var region = new Rectangle(column, row, width, height);

			return region;
		}

		/// <summary>
		///     Create rectangle from center coordinates and height / width
		/// </summary>
		/// <param name="row">center row</param>
		/// <param name="column">center column</param>
		/// <param name="height">total height</param>
		/// <param name="width">total width</param>
		/// <returns>a new rectangle</returns>
		public static Rectangle FromCenter(int row, int column, int height, int width)
		{
			if (height % 2 == 0 || width % 2 == 0) throw new Exception("Region height or with is even.");
			if (height <= 1 || width <= 1) throw new Exception("Region height or with is to small.");

			var relHeight = (height - 1) / 2;
			var relWidth = (width - 1) / 2;

			var top = row - relHeight;
			var left = column - relWidth;

			var region = new Rectangle(left, top, width, height);

			return region;
		}
	}
}