using System;
using NeoMatrix.Exceptions;
using NeoMatrix.Test.Helpers;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class MatrixFilterTest
	{
		[Test]
		[TestCase(0, 1)]
		[TestCase(4, 3)]
		public void CalculateMatrixParameters_EvenRows_ThrowsError(int rows, int columns)
		{
			var matrix = MatrixPopulator.CreateIncrementedInt(10, 10);

			Assert.Throws<EvenRowsException>(() => MatrixFilter.CalculateMatrixParameters(matrix, rows, columns, 1, 1, out _, out _, out _, out _));
		}

		[Test]
		[TestCase(1, 0)]
		[TestCase(5, 4)]
		public void CalculateMatrixParameters_EvenColumns_ThrowsError(int rows, int columns)
		{
			var matrix = MatrixPopulator.CreateRandomInt(10, 10);

			Assert.Throws<EvenColumnsException>(() => MatrixFilter.CalculateMatrixParameters(matrix, rows, columns, 1, 1, out _, out _, out _, out _));
		}

		[Test]
		[TestCase(-1, -1)]
		[TestCase(-3, 3)]
		[TestCase(5, 1)]
		[TestCase(1, 5)]
		[TestCase(5, 5)]
		public void CalculateMatrixParameters_Invalid_RowsColumns_ThrowsError(int rows, int columns)
		{
			var matrix = MatrixPopulator.CreateIncrementedInt(4, 4);

			Assert.Throws<OutOfRangeException>(() => MatrixFilter.CalculateMatrixParameters(matrix, rows, columns, 1, 1, out _, out _, out _, out _));
		}

		[Test]
		[TestCase(0, 0)]
		[TestCase(0, 1)]
		[TestCase(1, 0)]
		[TestCase(1, -1)]
		[TestCase(-1, -1)]
		[TestCase(-1, 0)]
		[TestCase(10, 0)]
		[TestCase(0, 10)]
		public void CalculateMatrixParameters_InvalidStride_ThrowsError(int yStride, int xStride)
		{
			var matrix = MatrixPopulator.CreateIncrementedInt(4, 6, 0);

			Assert.Throws<OutOfRangeException>(() => MatrixFilter.CalculateMatrixParameters(matrix, 3, 3, yStride, xStride, out _, out _, out _, out _));
		}
		
		[Test]
		[TestCase(10, 10)]
		[TestCase(10, 2)]
		[TestCase(2, 10)]
		public void CalculateMatrixParameters_StrideBiggerThanRowsColumns_ThrowsError(int yStride, int xStride)
		{
			var matrix = MatrixPopulator.CreateIncrementedInt(4, 6, 0);

			Assert.Throws<StrideException>(() => MatrixFilter.CalculateMatrixParameters(matrix, 3, 3, yStride, xStride, out _, out _, out _, out _));
		}

		[Test]
		[TestCase(3, 5)]
		[TestCase(5, 3)]
		public void CalculateMatrixParameters_StrideNotDivisibleByRemainingColumnsRows_ThrowsError(int yStride, int xStride)
		{
			var matrix = MatrixPopulator.CreateIncrementedInt(300, 600);

			Assert.Throws<StrideException>(() => MatrixFilter.CalculateMatrixParameters(matrix, 11, 11, yStride, xStride, out _, out _, out _, out _));
		}

		[Test]
		public void CalculateMatrixParameters_CalculatesOffsetCorrect()
		{
			var matrix = MatrixPopulator.CreateIncrementedInt(10, 10);

			MatrixFilter.CalculateMatrixParameters(matrix, 3, 3, 1, 1, out var rowOffset, out var colOffset, out _, out _);
			
			Assert.AreEqual(1,rowOffset);
			Assert.AreEqual(1,colOffset);
		}
		
		[Test]
		public void CalculateMatrixParameters_CalculatesOffsetCorrect1()
		{
			var matrix = MatrixPopulator.CreateIncrementedInt(100, 200);

			MatrixFilter.CalculateMatrixParameters(matrix, 31, 21, 1, 1, out var rowOffset, out var colOffset, out _, out _);
			
			Assert.AreEqual(15,rowOffset);
			Assert.AreEqual(10,colOffset);
		}
		
		[Test]
		public void CalculateMatrixParameters_CalculatesRemainingCorrect()
		{
			var matrix = MatrixPopulator.CreateIncrementedInt(10, 10);

			MatrixFilter.CalculateMatrixParameters(matrix, 3, 3, 1, 1, out _, out _, out var remainingRows, out var remainingColumns);
			
			Assert.AreEqual(8,remainingRows);
			Assert.AreEqual(8,remainingColumns);
		}
		
		[Test]
		public void CalculateMatrixParameters_CalculatesRemainingCorrect1()
		{
			var matrix = MatrixPopulator.CreateIncrementedInt(100, 200);

			MatrixFilter.CalculateMatrixParameters(matrix, 3, 11, 1, 1, out _, out _, out var remainingRows, out var remainingColumns);
			
			Assert.AreEqual(98,remainingRows);
			Assert.AreEqual(190,remainingColumns);
		}

		[Test]
		public void RectBoxedAlgo_CorrectSize()
		{
			var matrix = MatrixPopulator.CreateIncrementedDummy(4, 4);

			var sum = matrix.RectBoxedAlgo(3, 3, (_, _, m) => m.Sum(c => c.Value));

			Assert.AreEqual(2, sum.Rows);
			Assert.AreEqual(2, sum.Columns);
		}

		[Test]
		public void RectBoxedAlgo_ReturnSum()
		{
			var matrix = MatrixPopulator.CreateIncrementedDummy(4, 4);

			var expected = new Matrix<double>(new double[,]
			{
				{54, 63},
				{90, 99}
			});

			var sum = matrix.RectBoxedAlgo(3, 3, (_, _, m) => m.Sum(c => c.Value));

			Assert.AreEqual(expected, sum);
		}

		[Test]
		[LongRunning]
		[Combinatorial]
		public void RectBoxedAlgo_CalculateBigArray([Values(31, 11, 5)] int rows, [Values(31, 11, 5)] int columns)
		{
			var matrix = MatrixPopulator.CreateRandomDummy(300, 300, 0);

			var filtered = matrix.RectBoxedAlgo(rows, columns, (_, _, m) => m.Average(c => c.Value));

			Assert.AreEqual(300 - columns + 1, filtered.Columns);
			Assert.AreEqual(300 - rows + 1, filtered.Rows);
		}

		[Test]
		[LongRunning]
		public void RectBoxedAlgo_CalculateBigArray_Stride2()
		{
			var matrix = MatrixPopulator.CreateRandomDummy(300, 300, 0);

			var filtered = matrix.RectBoxedAlgo(11, 11, (_, _, m) => m.Average(c => c.Value), 2, 2);

			Assert.AreEqual(145, filtered.Columns);
			Assert.AreEqual(145, filtered.Rows);
		}


		[Test]
		[Combinatorial]
		[LongRunning]
		public void RectBoxedAvg_CheckAvgResult([Values(300, 100, 30)] int matRows, [Values(300, 100, 30)] int matColumns, [Values(11, 5)] int rows, [Values(11, 5)] int columns, [Values(1, 2, 5, 10)] int yStride, [Values(1, 2, 5, 10)] int xStride)
		{
			// some combinations are illegal, skip them now!
			if ((300 - columns + 1) % xStride != 0 ||
			    (300 - rows + 1) % yStride != 0 ||
			    columns < xStride ||
			    rows < yStride)
				Assert.Pass("skipped: invalid combination");

			var matrix = MatrixPopulator.CreateRandomDummy(matRows, matColumns, 0);

			var expected = matrix.RectBoxedAlgo(rows, columns, (_, _, mat) => mat.Average(c => c.Value), yStride, xStride);
			var result = matrix.RectBoxedAvg(rows, columns, c => c.Value, yStride, xStride);

			Assert.AreEqual(expected, result);
		}

		[Test]
		[Combinatorial]
		[LongRunning]
		public void RectBoxedSum_CheckAvgResult([Values(300, 100, 30)] int matRows, [Values(300, 100, 30)] int matColumns, [Values(11, 5)] int rows, [Values(11, 5)] int columns, [Values(1, 2, 5, 10)] int yStride, [Values(1, 2, 5, 10)] int xStride)
		{
			// some combinations are illegal, skip them now!
			if ((300 - columns + 1) % xStride != 0 ||
			    (300 - rows + 1) % yStride != 0 ||
			    columns < xStride ||
			    rows < yStride)
				Assert.Pass("skipped: invalid combination");

			var matrix = MatrixPopulator.CreateRandomDummy(matRows, matColumns, 0);

			var expected = matrix.RectBoxedAlgo(rows, columns, (_, _, mat) => mat.Sum(c => c.Value), yStride, xStride);
			var result = matrix.RectBoxedSum(rows, columns, c => c.Value, yStride, xStride);

			Assert.AreEqual(expected, result);
		}
	}
}