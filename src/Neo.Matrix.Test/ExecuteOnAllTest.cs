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
			_matrix = Matrix<DummyObject>.NewMatrix(4, 4, new DummyObject());
		}

		private class DummyObject
		{
			public bool IsExecuted { get; set; }
		}

		private Matrix<DummyObject> _matrix = null!;

		[Test]
		public void ExecuteOnAll_ExecutesFuncOnAll()
		{
			_matrix.ExecuteOnAll(c => c!.IsExecuted = true);

			Assert.IsTrue(_matrix.GetFlat().All(c => c!.IsExecuted));
		}

		[Test]
		public void ExecuteOnAll_RowColumnOverload_ExecutesFuncOnAll()
		{
			_matrix.ExecuteOnAll((c, _, _) => c!.IsExecuted = true);

			Assert.IsTrue(_matrix.GetFlat().All(c => c!.IsExecuted));
		}
	}
}