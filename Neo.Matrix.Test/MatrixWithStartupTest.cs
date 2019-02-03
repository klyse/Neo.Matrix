using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixWithStartupTest
	{
		public Matrix<int> Mat { get; set; }

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			Mat = new Matrix<int>(new[,]
			{
				{1, 2, 3},
				{4, 5, 6},
				{7, 8, 9}
			});
		}

		[Test]
		public void ArrayAccess_ReturnsCorrectValue()
		{
			Assert.AreEqual(Mat[0, 0], 1);
			Assert.AreEqual(Mat[0, 1], 2);
			Assert.AreEqual(Mat[0, 2], 3);
			Assert.AreEqual(Mat[1, 1], 5);
			Assert.AreEqual(Mat[2, 2], 9);
		}

		[Test]
		public void GetCol_ReturnsColumn()
		{
			var col = Mat.GetCol(1).ToList();

			var expectedCol = new List<int> {2, 5, 8};

			Assert.That(col, Is.EqualTo(expectedCol));
		}

		[Test]
		public void GetRow_ReturnsRow()
		{
			var row = Mat.GetRow(1).ToList();

			var expectedCol = new List<int> {4, 5, 6};

			Assert.That(row, Is.EqualTo(expectedCol));
		}

		[Test]
		public void GetFlat_ReturnsUniDimensionalView()
		{
			var col = Mat.GetFlat().ToList();

			var expectedCol = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};

			Assert.That(col, Is.EqualTo(expectedCol));
		}

		[Test]
		public void Duplicate_ReturnsDuplicate()
		{
			var duplicate = Mat.Duplicate().GetFlat(); // this is not the best way to use uni dimensional view, but it works

			Assert.That(duplicate, Is.EqualTo(Mat.GetFlat()));
		}

		[Test]
		public void Transpose_TransposesMatrix()
		{
			var x = Mat.Transpose().GetFlat();

			var expectedCol = new List<int> {1, 4, 7, 2, 5, 8, 3, 6, 9};

			Assert.That(x, Is.EqualTo(expectedCol));
		}

		[Test]
		public void GetAbove_ReturnsValue()
		{
			Assert.AreEqual(Mat.GetAbove(1, 1), 2);
		}

		[Test]
		public void GetAbove_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetAbove(0, 0));
		}

		[Test]
		public void GetBelow_ReturnsValue()
		{
			Assert.AreEqual(Mat.GetBelow(1, 1), 8);
		}

		[Test]
		public void GetBelow_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetBelow(2, 2));
		}

		[Test]
		public void GetLeft_ReturnsValue()
		{
			Assert.AreEqual(Mat.GetLeft(1, 1), 4);
		}

		[Test]
		public void GetLeft_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetLeft(0, 0));
		}

		[Test]
		public void GetRight_ReturnsValue()
		{
			Assert.AreEqual(Mat.GetRight(1, 1), 6);
		}

		[Test]
		public void GetRight_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetRight(2, 2));
		}

		[Test]
		[TestCase(1, 2)]
		[TestCase(2, 1)]
		[TestCase(1, 1)]
		[TestCase(5, 10)]
		[TestCase(12, 13)]
		public void GetBox_WithEvenNumber_ThrowsException(int height, int width)
		{
			Assert.Throws<Exception>(() => Mat.GetRect(1, 1, height, width));
		}

		[Test]
		[TestCase(1, 2)]
		[TestCase(2, 1)]
		[TestCase(1, 1)]
		[TestCase(5, 10)]
		[TestCase(12, 13)]
		public void GetBox_TakesRectangle_WithEvenNumber_ThrowsException(int height, int width)
		{
			Assert.Throws<Exception>(() => Mat.GetRect(new Rectangle(1, 1, height, width)));
		}
	}
}