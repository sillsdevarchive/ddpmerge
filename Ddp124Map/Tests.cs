using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Ddp124Map
{
	[TestFixture]
	public class Tests
	{
		[Test]
		public void RegEx()
		{
			string line = "' 1.1.1' nl > ' 1.1' nl c Objects in the sky";
			Regex regEx = new Regex(@"' (\d\.)*\d' nl > ' (\d\.)*\d' nl c .*");
			Assert.IsTrue(regEx.Match(line).Success);
		}
	}
}
