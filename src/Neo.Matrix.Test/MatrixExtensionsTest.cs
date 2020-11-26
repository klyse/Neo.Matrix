using System.Drawing;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixExtensionsTest
	{
		[SetUp]
		public void SetUp()
		{
			var val = 0;
			_matrix = Matrix<DummyObject>.NewMatrix(4, 4, () =>
			{
				val++;
				return new DummyObject
				{
					Value = val
				};
			});
		}

		private class DummyObject
		{
			public int Value { get; init; }
		}

		private Matrix<DummyObject> _matrix = null!;

		[Test]
		public void Average_CalculatesAverageOnMatrix()
		{
			Assert.AreEqual(8.5, _matrix.Average(c => c.Value));
		}

		[Test]
		public void Max_ReturnsMaxValueOfMatrix()
		{
			Assert.AreEqual(16, _matrix.Max(c => c.Value));
		}

		[Test]
		public void Min_ReturnsMinValueOfMatrix()
		{
			Assert.AreEqual(1, _matrix.Min(c => c.Value));
		}

		[Test]
		public void Sum_CalculatesTotalSumOnMatrix()
		{
			Assert.AreEqual((16 * 16 + 16) / 2, _matrix.Sum(c => c.Value));
		}

		[Test]
		public void ToBitmap_ReturnsBitmapOfMatrix()
		{
			var bitmap = _matrix.ToBitmap(c => c.Value);

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
			
			Assert.AreEqual(10,bitmap.Height);
			Assert.AreEqual(15,bitmap.Width);
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
	}
}