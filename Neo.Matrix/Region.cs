using System;
using System.Collections.Specialized;
using System.Drawing;

namespace NeoMatrix
{
	/// <summary>
	/// Region Class
	/// </summary>
	public class Region
	{
		private Rectangle _rect;

		/// <summary>
		/// Is total height even
		/// </summary>
		public bool IsHeightEven => Height % 2 == 0;

		/// <summary>
		/// Is total width even
		/// </summary>
		public bool IsWidthEven => Width % 2 == 0;

		/// <summary>
		/// Is width and height even
		/// </summary>
		public bool IsEven => IsHeightEven && IsWidthEven;

		/// <summary>
		/// Center Row
		/// </summary>
		/// <example>
		/// 10 => 5
		/// 11 => 5
		/// 12 => 6
		/// ....
		/// </example>
		public int CenterRow => (Bottom - Top) / 2;

		/// <summary>
		/// Center Column
		/// </summary>
		/// <example>
		/// 10 => 5
		/// 11 => 5
		/// 12 => 6
		/// ....
		/// </example>
		public int CenterColumn => (Right - Left) / 2;

		/// <summary>
		/// Region top
		/// </summary>
		public int Top => _rect.Top;

		/// <summary>
		/// Region bottom
		/// </summary>
		public int Bottom => _rect.Bottom;

		/// <summary>
		/// Region left
		/// </summary>
		public int Left => _rect.Left;

		/// <summary>
		/// Region right
		/// </summary>
		public int Right => _rect.Right;

		/// <summary>
		/// Total height
		/// </summary>
		public int Height => Bottom - Top;

		/// <summary>
		/// Total width
		/// </summary>
		public int Width => Right - Left;

		/// <summary>
		/// Create region from top left coordinates and height / width
		/// </summary>
		/// <param name="row">top row</param>
		/// <param name="column">left column</param>
		/// <param name="height">total height</param>
		/// <param name="width">total width</param>
		/// <returns></returns>
		public static Region FromTopLeft(int row, int column, int height, int width)
		{
			var region = new Region();

			region._rect = new Rectangle(column, row, width, height);

			return region;
		}

		/// <summary>
		/// Create region from center coordinates and height / width
		/// </summary>
		/// <param name="row">center row</param>
		/// <param name="column">center column</param>
		/// <param name="height">total height</param>
		/// <param name="width">total width</param>
		/// <returns></returns>
		public static Region FromCenter(int row, int column, int height, int width)
		{
			var top = row - height / 2;
			var left = column - width / 2;

			var region = new Region();

			region._rect = new Rectangle(left, top, width, height);

			return region;
		}
	}
}