using System;
using System.Drawing;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	internal class RegionHelperTest
	{
		[Test]
		public void FromTopLeft_ReturnsRegion_CheckBoarders()
		{
			var roi = RegionHelper.FromTopLeft(0, 1, 10, 5);

			Assert.AreEqual(0, roi.Top);
			Assert.AreEqual(1, roi.Left);
			Assert.AreEqual(6, roi.Right);
			Assert.AreEqual(10, roi.Bottom);
		}

		[Test]
		public void FromCenter_ReturnRegion_CheckBoarders()
		{
			var roi = RegionHelper.FromCenter(5, 10, 5, 9);

			Assert.AreEqual(5, roi.Height);
			Assert.AreEqual(9, roi.Width);
			Assert.AreEqual(3, roi.Top);
			Assert.AreEqual(8, roi.Bottom);
			Assert.AreEqual(6, roi.Left);
			Assert.AreEqual(15, roi.Right);
		}

		[Test]
		public void FromCenter_ReturnRegion_CheckBoarders2()
		{
			var roi = RegionHelper.FromCenter(5, 6, 3, 3);

			/*  x	  0  1  2  3  4  5  6  7  8  9 x
			 * y	+--+--+--+--+--+--+--+--+--+--+
			 * 0	|  |  |  |  |  |  |  |  |  |  |
			 *  	+--+--+--+--+--+--+--+--+--+--+
			 * 1	|  |  |  |  |  |  |  |  |  |  |
			 *  	+--+--+--+--+--+--+--+--+--+--+
			 * 2	|  |  |  |  |  |  |  |  |  |  |
			 *  	+--+--+--+--+--+--+--+--+--+--+
			 * 3	|  |  |  |  |  |  |  |  |  |  |
			 *  	+--+--+--+--+--+--+--+--+--+--+
			 * 4	|  |  |  |  |  |++|++|++|  |  |
			 *  	+--+--+--+--+--+--+--+--+--+--+
			 * 5	|  |  |  |  |  |++|**|++|  |  |
			 *  	+--+--+--+--+--+--+--+--+--+--+
			 * 6	|  |  |  |  |  |++|++|++|  |  |
			 *  	+--+--+--+--+--+--+--+--+--+--+
			 * 7	|  |  |  |  |  |  |  |  |  |  |
			 *  	+--+--+--+--+--+--+--+--+--+--+
			 * 8	|  |  |  |  |  |  |  |  |  |  |
			 *  	+--+--+--+--+--+--+--+--+--+--+
			 * 9	|  |  |  |  |  |  |  |  |  |  |
			 * y	+--+--+--+--+--+--+--+--+--+--+
			 */
			
			Assert.AreEqual(3, roi.Height);
			Assert.AreEqual(3, roi.Width);
			Assert.AreEqual(4, roi.Top);
			Assert.AreEqual(7, roi.Bottom);
			Assert.AreEqual(5, roi.Left);
			Assert.AreEqual(8, roi.Right);
		}

		[Test]
		public void FromCenter_ReturnRegion_EvenWidth_ThrowsError()
		{
			Assert.Catch<Exception>(() => RegionHelper.FromCenter(5, 10, 5, 4));
		}

		[Test]
		public void FromCenter_ReturnRegion_EvenHeight_ThrowsError()
		{
			Assert.Catch<Exception>(() => RegionHelper.FromCenter(5, 10, 4, 5));
		}
		[Test]
		public void FromCenter_ReturnRegion_ToSmallWidth_ThrowsError()
		{
			Assert.Catch<Exception>(() => RegionHelper.FromCenter(5, 10, 1, 4));
		}

		[Test]
		public void FromCenter_ReturnRegion_ToSmallHeight_ThrowsError()
		{
			Assert.Catch<Exception>(() => RegionHelper.FromCenter(5, 10, 5, 1));
		}

		[Test]
		public void Region_CheckDimensions()
		{
			var roi = RegionHelper.FromTopLeft(0, 1, 10, 5);

			Assert.AreEqual(10, roi.Height);
			Assert.AreEqual(5, roi.Width);
		}
	}
}