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
			StreamReader stream = new StreamReader(filePath);

			string line;
			Regex lineWithInterestingRule = new Regex(@"' (\d.)*' n1 > ' (\d.)*' n1 c .*");

			while((line = stream.ReadLine()) != null)
			{

			}
		}
	}
}
