using NeoMatrix.Exceptions;
using NUnit.Framework;

namespace NeoMatrix.Test.Exceptions
{
	[TestFixture]
	public class ExceptionsTest
	{
		[Test]
		public void OutOfRange()
		{
			OutOfRangeException.Check("", 1, 0, 10);
			Assert.Pass();
		}

		[Test]
		public void OutOfRange_Exception_ToSmall()
		{
			Assert.Catch<OutOfRangeException>(() => OutOfRangeException.Check("", 1, 5, 10));
		}

		[Test]
		public void OutOfRange_Exception_ToSmall1()
		{
			Assert.Catch<OutOfRangeException>(() => OutOfRangeException.Check("", 1, 5));
		}

		[Test]
		public void OutOfRange_Exception_ToBig()
		{
			Assert.Catch<OutOfRangeException>(() => OutOfRangeException.Check("", 11, 1, 10));
		}

		[Test]
		public void OutOfRange_Exception_ToBig1()
		{
			Assert.Catch<OutOfRangeException>(() => OutOfRangeException.Check("", 11, max: 10));
		}

		[Test]
		public void EvenException_Ok()
		{
			EvenException.Check("", 1);
			Assert.Pass();
		}

		[Test]
		public void EvenException_ThrowsException()
		{
			Assert.Catch<EvenException>(() => EvenException.Check("", 2));
		}


		[Test]
		public void UnevenException_Ok()
		{
			UnevenException.Check("", 2);
			Assert.Pass();
		}

		[Test]
		public void UnevenException_ThrowsException()
		{
			Assert.Catch<UnevenException>(() => UnevenException.Check("", 1));
		}
	}
}