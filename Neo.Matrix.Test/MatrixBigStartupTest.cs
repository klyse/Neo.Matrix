using System;
using System.Drawing;
using System.Linq.Expressions;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixBigStartupTest
	{
		public Matrix<int> Mat { get; set; }

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
			var box = Mat.GetRect(2, 2, 5, 3);

			var expectedBox = new Matrix<int>(new[,]
			{
				{10, 11, 12, 13, 14},
				{19, 20, 21, 22, 23},
				{28, 29, 30, 31, 32}
			});

			Assert.AreEqual(box.GetFlat(), expectedBox.GetFlat());
		}

		[Test]
		[TestCase(33, 3)]
		[TestCase(11, 11)]
		[TestCase(11, 3)]
		public void GetRect_TakesRectInput_OutOfRange_ThrowsException(int height, int width)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetRect(new Rectangle(1, 1, height, width)));
		}

		[Test]
		public void GetRect_ReturnsRect_TakesRectInput()
		{
			var box = Mat.GetRect(new Rectangle(2, 2, 5,3));

			var expectedBox = new Matrix<int>(new[,]
			{
				{10, 11, 12, 13, 14},
				{19, 20, 21, 22, 23},
				{28, 29, 30, 31, 32}
			});

			Assert.AreEqual(box.GetFlat(), expectedBox.GetFlat());
		}

		[Test]
		public void GetRect_ReturnsRect_TakesRectInput2()
		{
			var box = Mat.GetRect(new Rectangle(3, 2, 5, 3));

			var expectedBox = new Matrix<int>(new[,]
			{
				{11, 12, 13, 14, 15},
				{20, 21, 22, 23, 24},
				{29, 30, 31, 32, 33}
			});

			Assert.AreEqual(box.GetFlat(), expectedBox.GetFlat());
		}
	}
}