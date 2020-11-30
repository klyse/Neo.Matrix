using System;

namespace NeoMatrix.Test.Helpers
{
	public static class MatrixPopulator
	{
		public static Matrix<double> CreateRandomDouble(int rows, int columns, int min = -3000, int max = 3000)
		{
			var random = new Random();
			return Matrix<double>.NewMatrix(rows, columns, () => random.Next(min, max));
		}

		public static Matrix<int> CreateRandomInt(int rows, int columns, int min = -3000, int max = 3000)
		{
			var random = new Random();
			return Matrix<int>.NewMatrix(rows, columns, () => random.Next(min, max));
		}

		public static Matrix<int> CreateIncrementedInt(int rows, int columns, int start = 1)
		{
			var i = start;
			return Matrix<int>.NewMatrix(rows, columns, () =>
			{
				i++;
				return i - 1;
			});
		}

		public static Matrix<Dummy> CreateRandomDummy(int rows, int columns, int min = -3000, int max = 3000)
		{
			var random = new Random();
			return Matrix<Dummy>.NewMatrix(rows, columns, () => new Dummy(random.Next(min, max)));
		}

		public static Matrix<Dummy> CreateIncrementedDummy(int rows, int columns, int start = 1)
		{
			var i = start;
			return Matrix<Dummy>.NewMatrix(rows, columns, () =>
			{
				i++;
				return new Dummy(i - 1);
			});
		}

		public record Dummy(int Value);
	}
}