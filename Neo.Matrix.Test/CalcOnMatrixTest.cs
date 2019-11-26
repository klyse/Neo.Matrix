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
		public void Sum_CalculatesTotalSumOnMatrix()
		{
			Assert.AreEqual(Matrix.Sum(c => c.Value), (16 * 16 + 16) / 2);
		}

		[Test]
		public void Average_CalculatesAverageOnMatrix()
		{
			Assert.AreEqual(Matrix.Average(), 8.5);
		}

		[Test]
		public void Min_ReturnsMinValueOfMatrix()
		{
			Assert.AreEqual(Matrix.Min(), 1);
		}

		[Test]
		public void Max_ReturnsMaxValueOfMatrix()
		{
			Assert.AreEqual(Matrix.Max(), 16);
		}
	}
}