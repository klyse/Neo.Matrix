using System;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class CalcOnMatrixTest
	{
		public class DummyObject : IMatrixValue<double>
		{
			public bool IsExecuted { get; set; }
			public int Value { get; set; }

			public double GetValueObject()
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
		public void GetTotalSum_CalculatesTotalSumOnMatrix()
		{
			Assert.AreEqual(Matrix.GetTotalSum(), (16 * 16 + 16) / 2);
		}

		[Test]
		public void GetAverage_CalculatesAverageOnMatrix()
		{
			Assert.AreEqual(Matrix.GetAverage(), 8.5);
		}

		[Test]
		public void GetMin_ReturnsMinValueOfMatrix()
		{
			Assert.AreEqual(Matrix.GetMin(), 1);
		}

		[Test]
		public void GetMax_ReturnsMaxValueOfMatrix()
		{
			Assert.AreEqual(Matrix.GetMax(), 16);
		}

		[Test]
		[TestCase(5,1)]
		[TestCase(1,5)]
		[TestCase(5,5)]
		public void GetRectSum_BoxToBig(int cols, int width)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Matrix.GetRectSum(cols,width));
		}

		[Test]
		[TestCase(3,2)]
		[TestCase(4,3)]
		[TestCase(2,2)]
		public void GetRectSum_BoxNotUneven(int cols, int width)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Matrix.GetRectSum(cols,width));
		}

		[Test]
		[TestCase(0,0)]
		[TestCase(-1,-1)]
		[TestCase(-2,2)]
		public void GetRectSum_NotAllowedDimension(int cols, int width)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Matrix.GetRectSum(cols,width));
		}

		[Test]
		public void GetBoxSum_BoxToBig()
		{
			Assert.Throws<IndexOutOfRangeException>(() => Matrix.GetBoxSum(5));
		}

		[Test]
		public void GetBoxSum_BoxNotUneven()
		{
			Assert.Throws<IndexOutOfRangeException>(() => Matrix.GetBoxSum(2));
		}

		[Test]
		[TestCase(0)]
		[TestCase(-2)]
		public void GetBoxSum_NotAllowedDimension(int box)
		{
			Assert.Throws<IndexOutOfRangeException>(() => Matrix.GetBoxSum(box));
		}
	}
}