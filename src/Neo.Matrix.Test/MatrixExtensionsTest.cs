using System.Drawing;
using NeoMatrix.Test.Helpers;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixExtensionsTest
	{

		[Test]
		public void Average_CalculatesAverageOnMatrix()
		{
			var matrix = MatrixPopulator.CreateIncrementedDummy(4, 4);
			
			Assert.AreEqual(8.5, matrix.Average(c => c.Value));
		}

		[Test]
		public void Max_ReturnsMaxValueOfMatrix()
		{
			var matrix = MatrixPopulator.CreateIncrementedDummy(4, 4);
			Assert.AreEqual(16, matrix.Max(c => c.Value));
		}

		[Test]
		public void Min_ReturnsMinValueOfMatrix()
		{
			var matrix = MatrixPopulator.CreateIncrementedDummy(4, 4);
			Assert.AreEqual(1, matrix.Min(c => c.Value));
		}

		[Test]
		public void Sum_CalculatesTotalSumOnMatrix()
		{
			var matrix = MatrixPopulator.CreateIncrementedDummy(4, 4);
			Assert.AreEqual((16 * 16 + 16) / 2, matrix.Sum(c => c.Value));
		}

		[Test]
		public void ToBitmap_ReturnsBitmapOfMatrix()
		{
			var matrix = MatrixPopulator.CreateIncrementedDummy(4, 4);
			var bitmap = matrix.ToBitmap(c => c.Value);

			Assert.NotNull(bitmap);
		}

		[Test]
		public void ToBitmap_UnevenMatrix_ReturnsBitmapOfMatrix()
		{
			var bitmap = Matrix<int>.NewMatrix(10, 15, 1)
				.ToBitmap(c => c);

			Assert.NotNull(bitmap);
		}

		[Test]
		public void ToBitmap_UnevenMatrix_CheckSize()
		{
			var bitmap = Matrix<int>.NewMatrix(10, 15, 1)
				.ToBitmap(c => c);

			Assert.AreEqual(10, bitmap.Height);
			Assert.AreEqual(15, bitmap.Width);
		}

		[Test]
		public void ToBitmap_UnevenMatrix_ReturnsColoredBitmapOfMatrix()
		{
			var bitmap = Matrix<int>.NewMatrix(10, 15, (row, _) => row)
				.ToBitmap(c => c > 5 ? Color.Red : Color.Black);

			Assert.AreEqual(Color.Black.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
			Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(0, 6).ToArgb());
		}

		[Test]
		public void ToBitmap_UnevenMatrixWithRowColumnParameter_ReturnsColoredBitmapOfMatrix()
		{
			var bitmap = Matrix<int>.NewMatrix(10, 15, 1)
				.ToBitmap((_, c, _) => c > 5 ? Color.Red : Color.Black);

			Assert.AreEqual(Color.Black.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
			Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(6, 0).ToArgb());
		}

		[Test]
		public void ToMatrix_ConvertBitmapToMatrix()
		{
			var initMatrix = MatrixPopulator.CreateIncrementedDummy(4, 4);
			
			var bitmap = initMatrix.ToBitmap((_, _, v) => Color.FromArgb(v.Value, v.Value, v.Value));

			var matrix = bitmap.ToMatrix((_, _, color) => new MatrixPopulator.Dummy(color.G));

			Assert.AreEqual(initMatrix, matrix);
		}

		[Test]
		public void ToMatrix_UnevenMatrix_ConvertBitmapToMatrix()
		{
			var initMatrix = MatrixPopulator.CreateIncrementedDummy(4, 4);
			
			var expected = initMatrix.AddPadding(2, 0, 2, 0, new MatrixPopulator.Dummy(10));
			var bitmap = expected.ToBitmap((_, _, v) => Color.FromArgb(v.Value, v.Value, v.Value));

			var matrix = bitmap.ToMatrix((_, _, color) => new MatrixPopulator.Dummy(color.G));

			Assert.AreEqual(expected, matrix);
		}
	}
}