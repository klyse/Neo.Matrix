using System;
using NeoMatrix.Exceptions;
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
		public void RectBoxedSum_CorrectSize()
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
		public void RectBoxedSum_NotAllowedDimension(int rows, int columns)
		{
			Assert.Throws<OutOfRangeException>(() => Matrix.RectBoxedSum(rows, columns, matrix => matrix.Sum(c => c.Value)));
		}

		[Test]
		public void RectBoxedSum_ReturnSum()
		{
			var expected = new Matrix<double>(new double[,]
			{
				{54, 63},
				{90, 99}
			});

			var sum = Matrix.RectBoxedSum(3, 3, m => m.Sum(c => c.Value));

			Assert.AreEqual(expected, sum);
		}

		[Test]
		public void RectBoxedSum_Test()
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
		[TestCase(0, 1)]
		[TestCase(4, 3)]
		public void RectBoxedSum_EvenRows_ThrowsErrors(int rows, int columns)
		{
			Assert.Throws<EvenRowsException>(() => Matrix.RectBoxedSum(rows, columns, m => m.Sum(c => c.Value)));
		}

		[Test]
		[TestCase(1, 0)]
		[TestCase(5, 4)]
		public void RectBoxedSum_EvenColumns_ThrowsErrors(int rows, int columns)
		{
			Assert.Throws<EvenColumnsException>(() => Matrix.RectBoxedSum(rows, columns, m => m.Sum(c => c.Value)));
		}
	}
}