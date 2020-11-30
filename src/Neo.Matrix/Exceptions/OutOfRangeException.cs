namespace NeoMatrix.Exceptions
{
	/// <summary>
	///     Uneven columns are not permitted
	/// </summary>
	public class OutOfRangeException : MatrixException
	{
		/// <summary>
		///     Default constructor
		/// </summary>
		public OutOfRangeException(string param) : base($"'{param}' value is out of range")
		{
		}

		/// <summary>
		///     check and throw exception
		/// </summary>
		public static void Check(string param,int n, int? min = null, int? max = null)
		{
			if (max is { } && n >= max)
				throw new OutOfRangeException(param);

			if (min is { } && n <= min)
				throw new OutOfRangeException(param);
		}
	}
}