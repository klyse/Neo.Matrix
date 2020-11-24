using System;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixFilterTest
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
		public void GetRectSum_CorrectSize()
		{
			var sum = Matrix.RectBoxedSum(3, 3, m => m.Sum(c => c.Value));

			Assert.AreEqual(2, sum.Rows);
			Assert.AreEqual(2, sum.Columns);
		}

		[Test]
		[TestCase(-1, -1)]
		[TestCase(-3, 3)]
		[TestCase(5, 1)]
		[TestCase(1, 5)]
		[TestCase(5, 5)]
		public void GetRectSum_NotAllowedDimension(int cols, int width)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Matrix.RectBoxedSum(cols, width, matrix => matrix.Sum(c => c.Value)));
		}

		[Test]
		public void GetRectSum_ReturnSum()
		{
			var matrix = new Matrix<double>(new double[,]
											{
												{ 54, 63 },
												{ 90, 99 }
											});

			var sum = Matrix.RectBoxedSum(3, 3, m => m.Sum(c => c.Value));

			Assert.AreEqual(matrix, sum);
		}

		[Test]
		public void RectFilter_Test()
		{
			var matrix = Matrix<DummyObject>.NewMatrix(300, 300, () => new DummyObject
																	   {
																		   Value = new Random().Next(0, 3000)
																	   });

			var filtered = matrix.RectBoxedSum(11, 11, m => m.Average(c => c.Value));

			// matrix.ToBitmap(c => c.Value).Save("C:\\Users\\Klaus\\Downloads\\test.bmp");
			// filtered.ToBitmap(c => c).Save("C:\\Users\\Klaus\\Downloads\\test1.bmp");
			Assert.NotNull(filtered);
		}

		[Test]
		[TestCase(0, 0)]
		[TestCase(3, 2)]
		[TestCase(4, 3)]
		public void RectSumFilter_DimensionAreEven(int cols, int width)
		{
			Assert.Throws<Exception>(() => Matrix.RectBoxedSum(cols, width, m => m.Sum(c => c.Value)));
		}
	}
}