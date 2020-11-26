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
			var newMatrix = Matrix<int>.NewMatrix(2, 3, 4);

			var expected = new Matrix<int>(new[,]
			{
				{4, 4, 4},
				{4, 4, 4}
			});

			Assert.AreEqual(expected, newMatrix);
		}


		[Test]
		public void NewMatrix_Create_WithFiller()
		{
			var newMatrix = Matrix<int>.NewMatrix(2, 3, 4);

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
			var newMatrix = Matrix<int?>.NewMatrix(2, 3, filler: null);

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
			var newMatrix = Matrix<int?>.NewMatrix(2, 3, filler: null);

			var expected = new Matrix<int?>(new int?[,]
			{
				{null, null},
				{null, null}
			});

			Assert.AreNotEqual(expected, newMatrix);
		}

		[Test]
		public void AddPadding_AddEvenPadding()
		{
			var newMatrix = Matrix<int?>
				.NewMatrix(2, 2, 1)
				.AddPadding(1);


			var expected = new Matrix<int?>(new int?[,]
			{
				{null, null, null, null},
				{null, 1, 1, null},
				{null, 1, 1, null},
				{null, null, null, null}
			});

			Assert.AreEqual(expected, newMatrix);
		}

		[Test]
		public void AddPadding_AddEvenPadding_WithFiller()
		{
			var newMatrix = Matrix<int>
				.NewMatrix(2, 2, 1)
				.AddPadding(1, 5);


			var expected = new Matrix<int>(new[,]
			{
				{5, 5, 5, 5},
				{5, 1, 1, 5},
				{5, 1, 1, 5},
				{5, 5, 5, 5}
			});

			Assert.AreEqual(expected, newMatrix);
		}

		[Test]
		public void AddPadding_AddEvenPadding_WithFillerDefaultValue()
		{
			var newMatrix = Matrix<int>
				.NewMatrix(2, 2, 1)
				.AddPadding(1);


			var expected = new Matrix<int>(new[,]
			{
				{0, 0, 0, 0},
				{0, 1, 1, 0},
				{0, 1, 1, 0},
				{0, 0, 0, 0}
			});

			Assert.AreEqual(expected, newMatrix);
		}

		[Test]
		public void AddPadding_AddUnevenEvenPadding()
		{
			var newMatrix = Matrix<int?>
				.NewMatrix(2, 2, 1)
				.AddPadding(0, 1, 2, 3);


			var expected = new Matrix<int?>(new int?[,]
			{
				{null, null, null, 1, 1, null},
				{null, null, null, 1, 1, null},
				{null, null, null, null, null, null},
				{null, null, null, null, null, null}
			});

			Assert.AreEqual(expected, newMatrix);
		}
	}
}