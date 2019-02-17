using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	class RegionTest
	{
		[Test]
		public void FromTopLeft_ReturnsRegion_CheckBoarders()
		{
			var roi = Region.FromTopLeft(0, 1, 10, 5);

			Assert.AreEqual(0, roi.Top);
			Assert.AreEqual(1, roi.Left);
			Assert.AreEqual(6, roi.Right);
			Assert.AreEqual(10, roi.Bottom);
		}

		[Test]
		public void FromCenter_ReturnRegion_CheckBoarders()
		{
			var roi = Region.FromCenter(5, 10, 4, 8);
			
			Assert.AreEqual(3, roi.Top);
			Assert.AreEqual(6, roi.Left);
			Assert.AreEqual(14, roi.Right);
			Assert.AreEqual(7, roi.Bottom);
		}

		[Test]
		public void Region_CheckDimensions()
		{
			var roi = Region.FromTopLeft(0, 1, 10, 5);

			Assert.AreEqual(10, roi.Height);
			Assert.AreEqual(5, roi.Width);
		}

		[Test]
		public void Region_CheckCenter()
		{
			var roi = Region.FromTopLeft(0, 1, 10, 5);

			Assert.AreEqual(2, roi.CenterColumn);
			Assert.AreEqual(5, roi.CenterRow);
		}

		[Test]
		public void IsEven_False()
		{
			var roi = Region.FromTopLeft(5, 5, 5, 6);
			Assert.IsFalse(roi.IsHeightEven);

			roi = Region.FromTopLeft(5, 5, 6, 5);
			Assert.IsFalse(roi.IsWidthEven);

			roi = Region.FromTopLeft(5, 5, 6, 5);
			Assert.IsFalse(roi.IsEven);

			roi = Region.FromTopLeft(5, 5, 5, 6);
			Assert.IsFalse(roi.IsEven);
		}

		[Test]
		public void IsEven_True()
		{
			var roi = Region.FromTopLeft(5, 5, 6, 5);
			Assert.IsTrue(roi.IsHeightEven);

			roi = Region.FromTopLeft(5, 5, 5, 6);
			Assert.IsTrue(roi.IsWidthEven);

			roi = Region.FromTopLeft(5, 5, 6, 6);
			Assert.IsTrue(roi.IsEven);
		}

		[Test]
		public void CenterRow()
		{
			var roi = Region.FromTopLeft(0, 0, 10, 5);
			Assert.AreEqual(5, roi.CenterRow);

			roi = Region.FromTopLeft(0, 0, 11, 5);
			Assert.AreEqual(5, roi.CenterRow);
		}

		[Test]
		public void CenterColumn()
		{
			var roi = Region.FromTopLeft(0, 0, 10, 6);
			Assert.AreEqual(3, roi.CenterColumn);

			roi = Region.FromTopLeft(0, 0, 11, 7);
			Assert.AreEqual(3, roi.CenterColumn);
		}
	}
}