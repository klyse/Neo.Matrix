using System;
using System.Collections.Generic;
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
			Assert.AreEqual(1, Mat[0, 0]);
			Assert.AreEqual(2,Mat[0, 1]);
			Assert.AreEqual(3,Mat[0, 2]);
			Assert.AreEqual(5,Mat[1, 1]);
			Assert.AreEqual(9,Mat[2, 2]);
		}

		[Test]
		public void Duplicate_ReturnsDuplicate()
		{
			var duplicate = Mat.Duplicate();

			Assert.That(Mat.GetFlat(), Is.EqualTo(duplicate.GetFlat()));
			Assert.That(Mat, Is.Not.SameAs(duplicate));
		}

		[Test]
		public void GetAbove_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetAbove(0, 0));
		}

		[Test]
		public void GetAbove_ReturnsValue()
		{
			Assert.AreEqual(2,Mat.GetAbove(1, 1));
		}

		[Test]
		public void GetBelow_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetBelow(2, 2));
		}

		[Test]
		public void GetBelow_ReturnsValue()
		{
			Assert.AreEqual(8, Mat.GetBelow(1, 1));
		}

		[Test]
		public void GetCol_ReturnsColumn()
		{
			var col = Mat.GetCol(1).ToList();

			var expectedCol = new List<int> {2, 5, 8};

			Assert.That(expectedCol, Is.EqualTo(col));
		}

		[Test]
		public void GetFlat_ReturnsUniDimensionalView()
		{
			var col = Mat.GetFlat().ToList();

			var expectedCol = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};

			Assert.That(expectedCol, Is.EqualTo(col));
		}

		[Test]
		public void GetLeft_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetLeft(0, 0));
		}

		[Test]
		public void GetLeft_ReturnsValue()
		{
			Assert.AreEqual(4,Mat.GetLeft(1, 1));
		}

		[Test]
		public void GetRight_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => Mat.GetRight(2, 2));
		}

		[Test]
		public void GetRight_ReturnsValue()
		{
			Assert.AreEqual(6,Mat.GetRight(1, 1));
		}

		[Test]
		public void GetRow_ReturnsRow()
		{
			var row = Mat.GetRow(1).ToList();

			var expectedRow = new List<int> {4, 5, 6};

			Assert.That(expectedRow, Is.EqualTo(row));
		}

		[Test]
		public void Transpose_TransposesMatrix()
		{
			var x = Mat.Transpose().GetFlat();

			var expectedCol = new List<int> {1, 4, 7, 2, 5, 8, 3, 6, 9};

			Assert.That(expectedCol, Is.EqualTo(x));
		}
	}
}