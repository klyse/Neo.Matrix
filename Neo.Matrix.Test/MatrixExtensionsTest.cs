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
			Matrix = Matrix<DummyObject>.NewMatrix(4, 4, () =>
														 {
															 val++;
															 return new DummyObject
																	{
																		Value = val
																	};
														 });
		}

		public class DummyObject
		{
			public int Value { get; set; }
		}

		public Matrix<DummyObject> Matrix { get; set; }

		[Test]
		public void Average_CalculatesAverageOnMatrix()
		{
			Assert.AreEqual(Matrix.Average(c => c.Value), 8.5);
		}

		[Test]
		public void Max_ReturnsMaxValueOfMatrix()
		{
			Assert.AreEqual(Matrix.Max(c => c.Value), 16);
		}

		[Test]
		public void Min_ReturnsMinValueOfMatrix()
		{
			Assert.AreEqual(Matrix.Min(c => c.Value), 1);
		}

		[Test]
		public void Sum_CalculatesTotalSumOnMatrix()
		{
			Assert.AreEqual(Matrix.Sum(c => c.Value), (16 * 16 + 16) / 2);
		}

		[Test]
		public void ToBitmap_ReturnsBitmapOfMatrix()
		{
			var bitmap = Matrix.ToBitmap(c => c.Value);

			Assert.NotNull(bitmap);
		}

		[Test]
		public void ToBitmap_UnevenMatrix_ReturnsBitmapOfMatrix()
		{
			var bitmap = Matrix<int>.NewMatrix(10, 15, () => 1)
									.ToBitmap(c => c);

			Assert.NotNull(bitmap);
		}

		[Test]
		public void ToBitmap_UnevenMatrix_ReturnsColoredBitmapOfMatrix()
		{
			var bitmap = Matrix<int>.NewMatrix(10, 15, (row, column) => row)
									.ToBitmap(c => c > 5 ? Color.Red : Color.Black);

			Assert.AreEqual(Color.Black.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
			Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(0, 6).ToArgb());
		}
	}
}