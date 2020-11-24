namespace NeoMatrix.Exceptions
{
	/// <summary>
	///     Even rows are not permitted
	/// </summary>
	public class EvenRowsException : MatrixException
	{
		/// <summary>
		///     Default constructor
		/// </summary>
		public EvenRowsException() : base("even row number is not permitted")
		{
		}

		/// <summary>
		///     check and throw exception
		/// </summary>
		public static void Check(int n)
		{
			if (n % 2 == 0)
				throw new EvenRowsException();
		}
	}
}