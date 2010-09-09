using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ddp124Map
{
	class Program
	{
		static void Main(string[] args)
		{
			SemanticDomainCollection semanticDomains = new SemanticDomainCollection(@"D:\SoftwareDevelopment\Ddp124Map\DDP4\Ddp1.xml");
			semanticDomains.PrintAllSemanticDomainkeys();
			Console.ReadLine();
		}
	}
}
