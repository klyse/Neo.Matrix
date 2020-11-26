using System;
using NUnit.Framework;

namespace NeoMatrix.Test
{
	[TestFixture]
	public class MatrixEqualityCheckTest
	{
		private class CustomElement : IEquatable<CustomElement>
		{
			public int I { get; set; } = 100;

			public bool Equals(CustomElement? other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return I == other.I;
			}

			public override bool Equals(object? obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != GetType()) return false;
				return Equals((CustomElement) obj);
			}

			public override int GetHashCode()
			{
				// ReSharper disable once NonReadonlyMemberInGetHashCode
				return I;
			}
		}

		[Test]
		public void Equals_Matrix_CustomElement_IsEqual()
		{
			var newMatrix1 = Matrix<CustomElement>.NewMatrix(3, 3, new CustomElement());
			var newMatrix2 = Matrix<CustomElement>.NewMatrix(3, 3, new CustomElement());

			Assert.AreEqual(newMatrix1, newMatrix2);
		}

		[Test]
		public void Equals_Matrix_CustomElement_IsUnequal()
		{
			var newMatrix1 = Matrix<CustomElement>.NewMatrix(3, 3, new CustomElement());
			var newMatrix2 = Matrix<CustomElement>.NewMatrix(3, 3, new CustomElement());

			newMatrix1[0, 0]!.I = 1;

			Assert.AreNotEqual(newMatrix1, newMatrix2);
		}

		[Test]
		public void Equals_Matrix_IsEqual()
		{
			var matrix1 = new Matrix<double>(new double[,]
			{
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4}
			});
			var matrix2 = new Matrix<double>(new double[,]
			{
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4}
			});

			Assert.AreEqual(matrix1, matrix2);
		}

		[Test]
		public void Equals_Matrix_IsUnequal1()
		{
			var matrix1 = new Matrix<double>(new double[,]
			{
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 4, 4},
				{1, 2, 3, 4}
			});
			var matrix2 = new Matrix<double>(new double[,]
			{
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4}
			});

			Assert.AreNotEqual(matrix1, matrix2);
		}

		[Test]
		public void Equals_Matrix_IsUnequal2()
		{
			var matrix1 = new Matrix<double>(new double[,]
			{
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 1}
			});
			var matrix2 = new Matrix<double>(new double[,]
			{
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4}
			});

			Assert.AreNotEqual(matrix1, matrix2);
		}

		[Test]
		public void Equals_Matrix_IsUnequal3()
		{
			var matrix1 = new Matrix<double>(new double[,]
			{
				{2, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4}
			});
			var matrix2 = new Matrix<double>(new double[,]
			{
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4},
				{1, 2, 3, 4}
			});

			Assert.AreNotEqual(matrix1, matrix2);
		}

		[Test]
		public void Equals_Matrix_IsUnequalNull()
		{
			var matrix1 = new Matrix<int?>(new int?[,]
			{
				{null, 2},
				{1, null}
			});
			var matrix2 = new Matrix<int?>(new int?[,]
			{
				{2, null},
				{null, 1}
			});

			Assert.AreNotEqual(matrix1, matrix2);
		}

		[Test]
		public void Equals_Matrix_IsUnequalNull2()
		{
			var matrix1 = new Matrix<int?>(new int?[,]
			{
				{null, 1, null},
				{null, 1, null}
			});
			var matrix2 = new Matrix<int?>(new int?[,]
			{
				{null, null, 1},
				{null, null, 1}
			});

			Assert.AreNotEqual(matrix1, matrix2);
		}

		[Test]
		public void Equals_Matrix_IsUnequalNull3()
		{
			var matrix1 = new Matrix<int?>(new int?[,]
			{
				{null, null},
				{null, null}
			});
			var matrix2 = new Matrix<int?>(new int?[,]
			{
				{null, null, null},
				{null, null, null},
				{null, null, null}
			});

			Assert.AreNotEqual(matrix1, matrix2);
		}

		[Test]
		public void Equals_Matrix_IsUnequalNull4()
		{
			var matrix1 = new Matrix<int?>(new int?[,]
			{
				{1, 1, null, null},
				{1, 1, null, null},
				{null, null, null, null},
				{null, null, null, null}
			});

			var matrix2 = new Matrix<int?>(new int?[,]
			{
				{null, null, null, null},
				{null, 1, 1, null},
				{null, 1, 1, null},
				{null, null, null, null}
			});

			Assert.AreNotEqual(matrix1, matrix2);
		}

		[Test]
		public void Equals_Matrix_IsEqualNull()
		{
			var matrix1 = new Matrix<int?>(new int?[,]
			{
				{null, 2},
				{1, null}
			});
			var matrix2 = new Matrix<int?>(new int?[,]
			{
				{null, 2},
				{1, null}
			});

			Assert.AreEqual(matrix1, matrix2);
		}

		[Test]
		public void Equals_Matrix_IsEqualNull2()
		{
			var matrix1 = new Matrix<int?>(new int?[,]
			{
				{null, null},
				{null, null}
			});
			var matrix2 = new Matrix<int?>(new int?[,]
			{
				{null, null},
				{null, null}
			});

			Assert.AreEqual(matrix1, matrix2);
		}
	}
}