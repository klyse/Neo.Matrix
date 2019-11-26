using System.Collections.Generic;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixTest
	{
		[Test]
		public void IsSquare_RectangularMatrix()
		{
			var matrix = new Matrix<int>(6, 5);

			Assert.IsFalse(matrix.IsSquare());
		}

		[Test]
		public void IsSquare_SquareMatrix()
		{
			var matrix = new Matrix<int>(5, 5);

			Assert.IsTrue(matrix.IsSquare());
		}

		[Test]
		public void NewMatrix_CreateNewMatrixAndInitializeIt()
		{
			var x = Matrix<int>.NewMatrix(2, 2, () => 4).GetFlat();

			var expectedCol = new List<int> { 4, 4, 4, 4 };
			Assert.That(x, Is.EqualTo(expectedCol));
		}
	}
}