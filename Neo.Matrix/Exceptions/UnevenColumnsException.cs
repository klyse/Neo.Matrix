namespace NeoMatrix.Exceptions
{
	/// <summary>
	///     Uneven columns are not permitted
	/// </summary>
	public class UnevenColumnsException : MatrixException
	{
		/// <summary>
		///     Default constructor
		/// </summary>
		public UnevenColumnsException() : base("uneven column number is not permitted")
		{
		}

		/// <summary>
		///     check and throw exception
		/// </summary>
		public static void Check(int n)
		{
			if (n % 2 != 0)
				throw new UnevenColumnsException();
		}
	}
}