using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ddp124Map
{
	public class Ddp4to1MapReader
	{
		static public Dictionary<string, string> ReadFromFile(string filePath)
		{
			Dictionary<string, string> map = new Dictionary<string, string>();

			StreamReader stream = new StreamReader(filePath);


			Regex lineMatcher = new Regex(@"' (\d\.)*\d' nl > ' (\d\.)*\d' nl c .*");
			Regex numberMatcher = new Regex(@"(\d\.)*\d");
			string line;
			while((line = stream.ReadLine()) != null)
			{
				if (lineMatcher.Match(line).Success)
				{
					MatchCollection matches = numberMatcher.Matches(line);
					map.Add(matches[0].Value, matches[1].Value);
				}
			}
			return map;
		}
	}
}
