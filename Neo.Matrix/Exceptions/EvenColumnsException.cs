namespace NeoMatrix.Exceptions
{
	/// <summary>
	///     Even rows are not permitted
	/// </summary>
	public class EvenColumnsException : MatrixException
	{
		/// <summary>
		///     Default constructor
		/// </summary>
		public EvenColumnsException() : base("even column number is not permitted")
		{
		}

		/// <summary>
		///     check and throw exception
		/// </summary>
		public static void Check(int n)
		{
			if (n % 2 == 0)
				throw new EvenColumnsException();
		}
	}
}