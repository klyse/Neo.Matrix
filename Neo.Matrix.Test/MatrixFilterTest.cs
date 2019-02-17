using System;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixFilterTest
	{
		public class DummyObject : IMatrixValue<double>
		{
			public bool IsExecuted { get; set; }
			public int Value { get; set; }

			public double GetValue()
			{
				return Value;
			}
		}

		public Matrix<DummyObject> Matrix { get; set; }

		[SetUp]
		public void SetUp()
		{
			var val = 0;
			Matrix = Matrix<DummyObject>.NewMatrix(4, 4, () =>
			{
				val++;
				return new DummyObject()
				{
					Value = val
				};
			});
		}

		[Test]
		[TestCase(0, 0)]
		[TestCase(3, 2)]
		[TestCase(4, 3)]
		public void RectSumFilter_DimensionAreEven(int cols, int width)
		{
			Assert.Throws<Exception>(() => Matrix.RectSumFilter(cols, width));
		}

		[Test]
		[TestCase(-1, -1)]
		[TestCase(-3, 3)]
		[TestCase(5, 1)]
		[TestCase(1, 5)]
		[TestCase(5, 5)]
		public void GetRectSum_NotAllowedDimension(int cols, int width)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Matrix.RectSumFilter(cols, width));
		}

		[Test]
		public void GetRectSum_ReturnSum()
		{
			var matrix = new Matrix<double>(new double[,]
			{
				{54, 63},
				{90, 99},
			});

			var sum = Matrix.RectSumFilter(3, 3);

			Assert.AreEqual(sum, matrix);
		}
	}
}