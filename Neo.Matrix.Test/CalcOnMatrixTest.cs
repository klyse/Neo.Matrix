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
			Assert.AreEqual(Matrix.GetTotalSum(), (16*16+16)/2);
		}
	}
}