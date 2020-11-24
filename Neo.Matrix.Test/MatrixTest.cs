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
		public void NewMatrix_Create_IsEqual()
		{
			var newMatrix = Matrix<int>.NewMatrix(2, 3, () => 4);

			var expected = new Matrix<int>(new[,]
			{
				{4, 4, 4},
				{4, 4, 4}
			});

			Assert.AreEqual(expected, newMatrix);
		}

		[Test]
		public void NewMatrix_CreateNewEmptyMatrixAndInitializeIt()
		{
			var newMatrix = Matrix<int?>.NewMatrix(2, 3, () => null);

			var expected = new Matrix<int?>(new int?[,]
			{
				{null, null, null},
				{null, null, null}
			});

			Assert.AreEqual(expected, newMatrix);
		}

		[Test]
		public void NewMatrix_CreateNewEmptyMatrix_WithDifferentSize()
		{
			var newMatrix = Matrix<int?>.NewMatrix(2, 3, () => null);

			var expected = new Matrix<int?>(new int?[,]
			{
				{null, null},
				{null, null}
			});

			Assert.AreNotEqual(expected, newMatrix);
		}
	}
}