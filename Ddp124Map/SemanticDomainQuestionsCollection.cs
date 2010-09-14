using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ddp124Map
{
	public class SemanticDomainQuestionsCollection
	{
		public enum SemanticDomainsVersion
		{
			DDP1,
			DDP4,
			Undefined
		}

		private SemanticDomainsVersion _version;
		private string _wsId;
		private SortedDictionary<string, SemanticDomainQuestions> _domainKeyToQuestionsMap = new SortedDictionary<string, SemanticDomainQuestions>();

		public SemanticDomainQuestionsCollection(SemanticDomainsVersion version, string writingSystemId)
		{
			_version = version;
			_wsId = writingSystemId;
		}

		public SemanticDomainQuestionsCollection() : this(SemanticDomainsVersion.Undefined, "") { }

		public SemanticDomainsVersion Version
		{
			get { return _version; }
			set { _version = value; }
		}

		public string WritingSystemId
		{
			get { return _wsId; }
			set { _wsId = value; }
		}

		public SortedDictionary<string, SemanticDomainQuestions> DomainKeyToQuestionsMap
		{
			get { return _domainKeyToQuestionsMap; }
			set { _domainKeyToQuestionsMap = value; }
		}
	}
}
