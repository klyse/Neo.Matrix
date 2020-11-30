namespace NeoMatrix.Exceptions
{
	/// <summary>
	///     Uneven columns are not permitted
	/// </summary>
	public class UnevenException : MatrixException
	{
		/// <summary>
		///     Default constructor
		/// </summary>
		private UnevenException(string message) : base(message)
		{
		}

		/// <summary>
		///     check and throw exception
		/// </summary>
		public static void Check(string param, int n)
		{
			if (n % 2 != 0)
				throw new UnevenException($"'{param}' cannot be uneven");
		}
	}
}