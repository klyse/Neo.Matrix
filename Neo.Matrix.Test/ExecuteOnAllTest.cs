using System.Linq;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class ExecuteOnAllTest
	{
		[SetUp]
		public void SetUp()
		{
			Matrix = Matrix<DummyObject>.NewMatrix(4, 4, () => new DummyObject());
		}

		public class DummyObject
		{
			public bool IsExecuted { get; set; }
		}

		public Matrix<DummyObject> Matrix { get; set; }

		[Test]
		public void ExecuteOnAll_ExecutesFuncOnAll()
		{
			Matrix.ExecuteOnAll(c => c.IsExecuted = true);

			Assert.IsTrue(Matrix.GetFlat().All(c => c.IsExecuted));
		}

		[Test]
		public void ExecuteOnAll_RowColumnOverload_ExecutesFuncOnAll()
		{
			Matrix.ExecuteOnAll((c, row, column) => c.IsExecuted = true);

			Assert.IsTrue(Matrix.GetFlat().All(c => c.IsExecuted));
		}
	}
}