using System.Collections.Generic;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixExtensionsTest
	{
		public Matrix<int> Mat { get; set; }

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			Mat = new Matrix<int>(new[,]
			{
				{1, 2, 3},
				{4, 5, 6},
				{7, 8, 9}
			});
		}

		[Test]
		public void Transpose_TransposesMatrix()
		{
			var x = Mat.Transpose().GetUniDimensionalView();

			var expectedCol = new List<int> {1, 4, 7, 2, 5, 8, 3, 6, 9};

			Assert.That(x, Is.EqualTo(expectedCol));
		}
	}
}