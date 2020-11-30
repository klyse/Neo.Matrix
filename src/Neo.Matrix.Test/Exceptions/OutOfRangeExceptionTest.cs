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
		public void EvenColumns_Exception_Ok()
		{
			EvenColumnsException.Check(1);
			Assert.Pass();
		}

		[Test]
		public void EvenColumns_Exception()
		{
			Assert.Catch<EvenColumnsException>(() => EvenColumnsException.Check(2));
		}

		[Test]
		public void EvenRows_Exception_Ok()
		{
			EvenColumnsException.Check(1);
			Assert.Pass();
		}

		[Test]
		public void EvenRows_Exception()
		{
			Assert.Catch<EvenColumnsException>(() => EvenColumnsException.Check(2));
		}

		[Test]
		public void UnevenColumns_Exception_Ok()
		{
			EvenColumnsException.Check(1);
			Assert.Pass();
		}

		[Test]
		public void UnevenColumns_Exception()
		{
			Assert.Catch<UnevenColumnsException>(() => UnevenColumnsException.Check(1));
		}

		[Test]
		public void UnevenRows_Exception_Ok()
		{
			UnevenColumnsException.Check(2);
			Assert.Pass();
		}

		[Test]
		public void UnevenRows_Exception()
		{
			Assert.Catch<UnevenColumnsException>(() => UnevenColumnsException.Check(1));
		}
	}
}