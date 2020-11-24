using System;
using System.Drawing;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixBigStartupTest
	{
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var i = 0;
			Mat = Matrix<int>.NewMatrix(9, 9, () =>
			{
				i++;
				return i;
			});
		}

		public Matrix<int> Mat { get; set; }

		[Test]
		[TestCase(33)]
		[TestCase(11)]
		[TestCase(5)]
		public void GetBox_OutOfRange_ThrowsException(int size)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetBox(8, 8, size));
		}

		[Test]
		public void GetBox_ReturnsBox()
		{
			var box = Mat.GetBox(2, 2, 3);

			var expectedBox = new Matrix<int>(new[,]
			{
				{11, 12, 13},
				{20, 21, 22},
				{29, 30, 31}
			});

			Assert.AreEqual(expectedBox, box);
		}

		[Test]
		public void GetBox_ReturnsBox_TakesPointInput2()
		{
			var box = Mat.GetBox(new Point(3, 2), 3);

			var expectedBox = new Matrix<int>(new[,]
			{
				{12, 13, 14},
				{21, 22, 23},
				{30, 31, 32}
			});

			Assert.AreEqual(expectedBox, box);
		}

		[Test]
		[TestCase(33)]
		[TestCase(11)]
		[TestCase(5)]
		public void GetBox_TakesPoint_OutOfRange_ThrowsException(int size)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetBox(new Point(8, 8), size));
		}

		[Test]
		public void GetFromRegion()
		{
			var matReg = Mat.GetRect(RegionHelper.FromCenter(3, 2, 3, 5));

			var expectedBox = new Matrix<int>(new[,]
			{
				{19, 20, 21, 22, 23},
				{28, 29, 30, 31, 32},
				{37, 38, 39, 40, 41}
			});

			Assert.AreEqual(expectedBox, matReg);
		}

		[Test]
		[TestCase(33, 3)]
		[TestCase(11, 11)]
		[TestCase(11, 3)]
		public void GetRect_OutOfRange_ThrowsException(int height, int width)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetRect(1, 1, height, width));
		}

		[Test]
		public void GetRect_ReturnsRect()
		{
			var box = Mat.GetRect(2, 2, 3, 5);

			var expectedBox = new Matrix<int>(new[,]
			{
				{10, 11, 12, 13, 14},
				{19, 20, 21, 22, 23},
				{28, 29, 30, 31, 32}
			});

			Assert.AreEqual(expectedBox, box);
		}

		[Test]
		public void GetRect_ReturnsRect_TakesRectInput()
		{
			var box = Mat.GetRect(new Rectangle(0, 1, 5, 3));

			var expectedBox = new Matrix<int>(new[,]
			{
				{10, 11, 12, 13, 14},
				{19, 20, 21, 22, 23},
				{28, 29, 30, 31, 32}
			});

			Assert.AreEqual(expectedBox, box);
		}

		[Test]
		public void GetRect_ReturnsRect_TakesRectInput2()
		{
			var box = Mat.GetRect(new Rectangle(1, 1, 5, 3));

			var expectedBox = new Matrix<int>(new[,]
			{
				{11, 12, 13, 14, 15},
				{20, 21, 22, 23, 24},
				{29, 30, 31, 32, 33}
			});

			Assert.AreEqual(expectedBox, box);
		}

		[Test]
		public void GetRect_ReturnsRect1()
		{
			var box = Mat.GetRect(2, 2, 3, 3);

			var expectedBox = new Matrix<int>(new[,]
			{
				{11, 12, 13},
				{20, 21, 22},
				{29, 30, 31}
			});

			Assert.AreEqual(expectedBox, box);
		}

		[Test]
		[TestCase(33, 3)]
		[TestCase(11, 11)]
		[TestCase(11, 3)]
		public void GetRect_TakesRectInput_OutOfRange_ThrowsException(int height, int width)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetRect(new Rectangle(1, 1, height, width)));
		}
	}
}