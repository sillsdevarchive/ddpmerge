using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ddp124Map
{
	public class SemanticDomainQuestions
	{
		private Guid _guid;
		private string _semanticDomainKey;
		private List<string> _questions = new List<string>();

		public SemanticDomainQuestions(Guid guid, string semanticDomainKey)
		{
			_guid = guid;
			_semanticDomainKey = semanticDomainKey;
		}

		public Guid Guid
		{
			get { return _guid; }
		}

		public string SemanticDomainKey
		{
			get { return _semanticDomainKey; }
		}

		public string Number
		{
			get
			{
				return _semanticDomainKey.Split('\u0020')[0].Trim();
			}
		}

		public List<string> Questions
		{
			get { return _questions; }
			set { _questions = value; }
		}
	}
}
