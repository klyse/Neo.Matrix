using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixWithStartupTest
	{
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mat = new Matrix<int>(new[,]
			{
				{1, 2, 3},
				{4, 5, 6},
				{7, 8, 9}
			});
		}

		private Matrix<int> _mat = null!;

		[Test]
		public void ArrayAccess_ReturnsCorrectValue()
		{
			Assert.AreEqual(1, _mat[0, 0]);
			Assert.AreEqual(2, _mat[0, 1]);
			Assert.AreEqual(3, _mat[0, 2]);
			Assert.AreEqual(5, _mat[1, 1]);
			Assert.AreEqual(9, _mat[2, 2]);
		}

		[Test]
		public void Duplicate_ReturnsDuplicate()
		{
			var duplicate = _mat.Duplicate();

			Assert.That(_mat.GetFlat(), Is.EqualTo(duplicate.GetFlat()));
			Assert.That(_mat, Is.Not.SameAs(duplicate));
		}

		[Test]
		public void GetAbove_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => _mat.GetAbove(0, 0));
		}

		[Test]
		public void GetAbove_ReturnsValue()
		{
			Assert.AreEqual(2, _mat.GetAbove(1, 1));
		}

		[Test]
		public void GetBelow_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => _mat.GetBelow(2, 2));
		}

		[Test]
		public void GetBelow_ReturnsValue()
		{
			Assert.AreEqual(8, _mat.GetBelow(1, 1));
		}

		[Test]
		public void GetCol_ReturnsColumn()
		{
			var col = _mat.GetCol(1).ToList();

			var expectedCol = new List<int> {2, 5, 8};

			Assert.That(expectedCol, Is.EqualTo(col));
		}

		[Test]
		public void GetFlat_ReturnsUniDimensionalView()
		{
			var col = _mat.GetFlat().ToList();

			var expectedCol = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};

			Assert.That(expectedCol, Is.EqualTo(col));
		}

		[Test]
		public void GetLeft_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => _mat.GetLeft(0, 0));
		}

		[Test]
		public void GetLeft_ReturnsValue()
		{
			Assert.AreEqual(4, _mat.GetLeft(1, 1));
		}

		[Test]
		public void GetRight_OutOfRange_ThrowsException()
		{
			Assert.Throws<IndexOutOfRangeException>(() => _mat.GetRight(2, 2));
		}

		[Test]
		public void GetRight_ReturnsValue()
		{
			Assert.AreEqual(6, _mat.GetRight(1, 1));
		}

		[Test]
		public void GetRow_ReturnsRow()
		{
			var row = _mat.GetRow(1).ToList();

			var expectedRow = new List<int> {4, 5, 6};

			Assert.That(expectedRow, Is.EqualTo(row));
		}

		[Test]
		public void Transpose_TransposesMatrix()
		{
			var x = _mat.Transpose().GetFlat();

			var expectedCol = new List<int> {1, 4, 7, 2, 5, 8, 3, 6, 9};

			Assert.That(expectedCol, Is.EqualTo(x));
		}
	}
}