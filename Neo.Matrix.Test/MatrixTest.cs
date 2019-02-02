using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixTest
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
		public void IsSquare_SquareMatrix()
		{
			var matrix = new Matrix<int>(5, 5);

			Assert.IsTrue(matrix.IsSquare());
		}

		[Test]
		public void IsSquare_RectangularMatrix()
		{
			var matrix = new Matrix<int>(6, 5);

			Assert.IsFalse(matrix.IsSquare());
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
		public void GetUniDimensionalView_ReturnsUniDimensionalView()
		{
			var col = Mat.GetUniDimensionalView().ToList();

			var expectedCol = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};

			Assert.That(col, Is.EqualTo(expectedCol));
		}

		[Test]
		public void Duplicate_ReturnsDuplicate()
		{
			var duplicate = Mat.Duplicate().GetUniDimensionalView(); // this is not the best way to use uni dimensional view, but it works

			Assert.That(duplicate, Is.EqualTo(Mat.GetUniDimensionalView()));
		}

		[Test]
		public void ZeroMatrix_CreateNewMatrixAndInitializeIt()
		{
			var x = Matrix<int>.ZeroMatrix(2, 2, () => 4).GetUniDimensionalView();

			var expectedCol = new List<int> {4, 4, 4, 4};
			Assert.That(x, Is.EqualTo(expectedCol));
		}
	}
}