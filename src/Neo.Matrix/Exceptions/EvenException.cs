namespace NeoMatrix.Exceptions
{
	/// <summary>
	///     Even rows are not permitted
	/// </summary>
	public class EvenException : MatrixException
	{
		/// <summary>
		///     Default constructor
		/// </summary>
		private EvenException(string message) : base(message)
		{
		}

		/// <summary>
		///     check and throw exception
		/// </summary>
		public static void Check(string param, int n)
		{
			if (n % 2 == 0)
				throw new EvenException($"'{param}' cannot be even");
		}
	}
}