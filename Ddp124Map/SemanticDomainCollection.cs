using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	public class SemanticDomainCollection
	{
		private List<SemanticDomainInfo> _semanticDomains = new List<SemanticDomainInfo>();

		public void PrintAllSemanticDomainkeys()
		{
			foreach (SemanticDomainInfo semanticDomain in _semanticDomains)
			{
				Console.WriteLine(semanticDomain.Key);
			}
		}

		public void AddSemanticDomain(SemanticDomainInfo semanticDomain)
		{
			_semanticDomains.Add(semanticDomain);
		}

		public IEnumerable<SemanticDomainInfo> AllSemanticDomains
		{
			get
			{
				foreach (SemanticDomainInfo semanticDomain in _semanticDomains)
				{
					yield return semanticDomain;
				}
			}
		}

		internal SemanticDomainInfo GetSemanticDomainWithNumber(string numberOfDomainToFind)
		{
			foreach (SemanticDomainInfo semanticDomain in _semanticDomains)
			{
				if (semanticDomain.Number == numberOfDomainToFind)
				{
					return semanticDomain;
				}
			}
			throw new ApplicationException(String.Format("SemanticDomain with number {0} was not found.",numberOfDomainToFind));
		}
	}
}
