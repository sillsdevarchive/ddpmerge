using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	public class SemanticDomainCollection
	{
		private IEnumerable<SemanticDomainInfo> _semanticDomains;

		public SemanticDomainCollection(string filePath)
		{
			_semanticDomains = new SemanticDomainReader().ReadFromFile(filePath);
		}

		public void PrintAllSemanticDomainkeys()
		{
			foreach (SemanticDomainInfo semanticDomain in _semanticDomains)
			{
				Console.WriteLine(semanticDomain.Key);
			}
		}
	}
}
