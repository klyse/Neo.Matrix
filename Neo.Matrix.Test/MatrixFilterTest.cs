using System;
using System.Diagnostics;
using NeoMatrix.Exceptions;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class MatrixFilterTest
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
		[Combinatorial]
		public void RectBoxedAlgo_CalculateBigArray([Values(31, 11, 5)] int rows, [Values(31, 11, 5)] int columns)
		{
			var matrix = Matrix<DummyObject>.NewMatrix(300, 300, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			var filtered = matrix.RectBoxedAlgo(rows, columns, (_, _, m) => m.Average(c => c.Value));

			Assert.AreEqual(300 - columns + 1, filtered.Columns);
			Assert.AreEqual(300 - rows + 1, filtered.Rows);
		}

		[Test]
		public void RectBoxedAlgo_CalculateBigArray_Stride2()
		{
			var matrix = Matrix<DummyObject>.NewMatrix(300, 300, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			var filtered = matrix.RectBoxedAlgo(11, 11, (_, _, m) => m.Average(c => c.Value), 2, 2);

			Assert.AreEqual(145, filtered.Columns);
			Assert.AreEqual(145, filtered.Rows);
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
		public void RectBoxedAvg_InvalidStride_Exception(int yStride, int xStride)
		{
			var matrix = Matrix<DummyObject>.NewMatrix(300, 600, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			Assert.Throws<OutOfRangeException>(() => matrix.RectBoxedAvg(11, 11, c => c.Value, yStride, xStride));
		}

		[Test]
		[TestCase(0, 1)]
		[TestCase(4, 3)]
		public void RectBoxedAvg_EvenRows_ThrowsErrors(int rows, int columns)
		{
			Assert.Throws<EvenRowsException>(() => _matrix.RectBoxedAvg(rows, columns, c => c.Value));
		}

		[Test]
		[TestCase(1, 0)]
		[TestCase(5, 4)]
		public void RectBoxedAvg_EvenColumns_ThrowsErrors(int rows, int columns)
		{
			Assert.Throws<EvenColumnsException>(() => _matrix.RectBoxedAvg(rows, columns, c => c.Value));
		}

		[Test]
		[TestCase(3, 5)]
		[TestCase(5, 3)]
		public void RectBoxedAvg_StrideOutOfRange_Exception(int yStride, int xStride)
		{
			var matrix = Matrix<DummyObject>.NewMatrix(300, 600, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			Assert.Throws<MatrixException>(() => matrix.RectBoxedAvg(11, 11, c => c.Value, yStride, xStride));
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

		[Test]
		[Combinatorial]
		public void RectBoxedAvg_CheckAvgResult([Values(300, 100, 30)] int matRows, [Values(300, 100, 30)] int matColumns, [Values(11, 5)] int rows, [Values(11, 5)] int columns, [Values(1, 2, 5, 10)] int yStride, [Values(1, 2, 5, 10)] int xStride)
		{
			// some combinations are illegal, skip them now!
			if ((300 - columns + 1) % xStride != 0 ||
			    (300 - rows + 1) % yStride != 0 ||
			    columns < xStride ||
			    rows < yStride)
				Assert.Pass("skipped: invalid combination");

			var matrix = Matrix<DummyObject>.NewMatrix(matRows, matColumns, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			var expected = matrix.RectBoxedAlgo(rows, columns, (_, _, mat) => mat.Average(c => c.Value), yStride, xStride);
			var result = matrix.RectBoxedAvg(rows, columns, c => c.Value, yStride, xStride);

			Assert.AreEqual(expected, result);
		}

		[Test]
		[Combinatorial]
		public void RectBoxedAvg_CheckTime_TakesLessThan_NotOptimizedAlgo([Values(11, 5)] int rows, [Values(11, 5)] int columns, [Values(1, 2, 5, 10)] int yStride, [Values(1, 2, 5, 10)] int xStride)
		{
			// some combinations are illegal, skip them now!
			if ((300 - columns + 1) % xStride != 0 ||
			    (300 - rows + 1) % yStride != 0 ||
			    columns < xStride ||
			    rows < yStride)
				Assert.Pass("skipped: invalid combination");

			var matrix = Matrix<DummyObject>.NewMatrix(300, 300, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			var optimizedAlgo = Stopwatch.StartNew();
			matrix.RectBoxedAvg(rows, columns, c => c.Value, yStride, xStride);
			optimizedAlgo.Stop();

			var reference = Stopwatch.StartNew();
			matrix.RectBoxedAlgo(rows, columns, (_, _, mat) => mat.Average(c => c.Value), yStride, xStride);
			reference.Stop();

			Console.WriteLine($"Not optimized: {reference.Elapsed} optimized: {optimizedAlgo.Elapsed}");
			Assert.GreaterOrEqual(reference.ElapsedMilliseconds, optimizedAlgo.ElapsedMilliseconds);
		}

		[Test]
		[Combinatorial]
		public void RectBoxedSum_CheckAvgResult([Values(300, 100, 30)] int matRows, [Values(300, 100, 30)] int matColumns, [Values(11, 5)] int rows, [Values(11, 5)] int columns, [Values(1, 2, 5, 10)] int yStride, [Values(1, 2, 5, 10)] int xStride)
		{
			// some combinations are illegal, skip them now!
			if ((300 - columns + 1) % xStride != 0 ||
			    (300 - rows + 1) % yStride != 0 ||
			    columns < xStride ||
			    rows < yStride)
				Assert.Pass("skipped: invalid combination");

			var matrix = Matrix<DummyObject>.NewMatrix(matRows, matColumns, () => new DummyObject
			{
				Value = new Random().Next(0, 3000)
			});

			var expected = matrix.RectBoxedAlgo(rows, columns, (_, _, mat) => mat.Sum(c => c.Value), yStride, xStride);
			var result = matrix.RectBoxedSum(rows, columns, c => c.Value, yStride, xStride);

			Assert.AreEqual(expected, result);
		}
	}
}