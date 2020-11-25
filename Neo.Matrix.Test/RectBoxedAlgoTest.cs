using System;
using NeoMatrix.Exceptions;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class RectBoxedAlgoTest
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
			public int Value { get; set; }
		}

		private Matrix<DummyObject> _matrix = null!;

		[Test]
		public void RectBoxedAlgo_CorrectSize()
		{
			var sum = _matrix.RectBoxedAlgo(3, 3, (_, _, m) => m.Sum(c => c.Value));

			Assert.AreEqual(2, sum.Rows);
			Assert.AreEqual(2, sum.Columns);
		}

		[Test]
		[TestCase(-1, -1)]
		[TestCase(-3, 3)]
		[TestCase(5, 1)]
		[TestCase(1, 5)]
		[TestCase(5, 5)]
		public void RectBoxedAlgo_NotAllowedDimension(int rows, int columns)
		{
			Assert.Throws<OutOfRangeException>(() => _matrix.RectBoxedAlgo(rows, columns, (_, _, m) => m.Sum(c => c.Value)));
		}

		[Test]
		public void RectBoxedAlgo_ReturnSum()
		{
			var expected = new Matrix<double>(new double[,]
			{
				{54, 63},
				{90, 99}
			});

			var sum = _matrix.RectBoxedAlgo(3, 3, (_, _, m) => m.Sum(c => c.Value));

			Assert.AreEqual(expected, sum);
		}

		[Test]
		public void RectBoxedAlgo_CalculateBigArray()
		{
			var matrix = Matrix<DummyObject>.NewMatrix(300, 300, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			var filtered = matrix.RectBoxedAlgo(11, 11, (_, _, m) => m.Average(c => c.Value));

			// matrix.ToBitmap(c => c.Value).Save("C:\\Users\\Klaus\\Downloads\\test.bmp");
			// filtered.ToBitmap(c => c).Save("C:\\Users\\Klaus\\Downloads\\test1.bmp");

			Assert.AreEqual(290, filtered.Columns);
			Assert.AreEqual(290, filtered.Rows);
		}

		[Test]
		public void RectBoxedAlgo_CalculateBigArray_Stride2()
		{
			var matrix = Matrix<DummyObject>.NewMatrix(300, 300, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			var filtered = matrix.RectBoxedAlgo(11, 11, (_, _, m) => m.Average(c => c.Value), 2, 2);

			// matrix.ToBitmap(c => c.Value).Save("C:\\Users\\Klaus\\Downloads\\test.bmp");
			// filtered.ToBitmap(c => c).Save("C:\\Users\\Klaus\\Downloads\\test1.bmp");

			Assert.AreEqual(145, filtered.Columns);
			Assert.AreEqual(145, filtered.Rows);
		}

		[Test]
		public void RectBoxedAlgo_CalculateBigArray_YStride5_XStride10()
		{
			var matrix = Matrix<DummyObject>.NewMatrix(300, 600, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			var filtered = matrix.RectBoxedAlgo(11, 11, (_, _, m) => m.Average(c => c.Value), 5, 10);

			// matrix.ToBitmap(c => c.Value).Save("C:\\Users\\Klaus\\Downloads\\test.bmp");
			// filtered.ToBitmap(c => c).Save("C:\\Users\\Klaus\\Downloads\\test1.bmp");

			Assert.AreEqual(59, filtered.Columns);
			Assert.AreEqual(58, filtered.Rows);
		}

		[Test]
		[TestCase(3, 5)]
		[TestCase(5, 3)]
		public void RectBoxedAlgo_StrideOutOfRange_Exception(int yStride, int xStride)
		{
			var matrix = Matrix<DummyObject>.NewMatrix(300, 600, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			Assert.Throws<MatrixException>(() => matrix.RectBoxedAlgo(11, 11, (_, _, m) => m.Average(c => c.Value), yStride, xStride));
		}

		[Test]
		[TestCase(0, 0)]
		[TestCase(0, 1)]
		[TestCase(1, 0)]
		public void RectBoxedAlgo_InvalidStride_Exception(int yStride, int xStride)
		{
			var matrix = Matrix<DummyObject>.NewMatrix(300, 600, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			Assert.Throws<OutOfRangeException>(() => matrix.RectBoxedAlgo(11, 11, (_, _, m) => m.Average(c => c.Value), yStride, xStride));
		}

		[Test]
		[TestCase(0, 1)]
		[TestCase(4, 3)]
		public void RectBoxedAlgo_EvenRows_ThrowsErrors(int rows, int columns)
		{
			Assert.Throws<EvenRowsException>(() => _matrix.RectBoxedAlgo(rows, columns, (_, _, m) => m.Sum(c => c.Value)));
		}

		[Test]
		[TestCase(1, 0)]
		[TestCase(5, 4)]
		public void RectBoxedAlgo_EvenColumns_ThrowsErrors(int rows, int columns)
		{
			Assert.Throws<EvenColumnsException>(() => _matrix.RectBoxedAlgo(rows, columns, (_, _, m) => m.Sum(c => c.Value)));
		}
	}
}