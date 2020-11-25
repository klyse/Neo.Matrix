using NUnit.Framework;

namespace NeoMatrix.Test
{
	public class LongRunningAttribute : CategoryAttribute
	{
		public LongRunningAttribute() : base("LongRunning")
		{
		}
	}
}