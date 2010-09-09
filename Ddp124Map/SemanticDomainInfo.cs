using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ddp124Map
{
	public class SemanticDomainInfo
	{
		private string _number = "";
		private string _keyName = "";
		private Dictionary<string, string> _names = new Dictionary<string, string>();
		private Dictionary<string, string> _abbreviation = new Dictionary<string, string>();
		private Dictionary<string, string> _searchKeys = new Dictionary<string, string>();
		private Dictionary<string, string> _descriptions = new Dictionary<string, string>();

		public SemanticDomainInfo(string key)
		{
			// should check whether we really have a number + name combo
			_number = key.Split('\u0020')[0].Trim();
			_keyName = key.Replace(Number, "").Trim();
		}

		public string Number
		{
			get { return _number; }
			set { _number = value; }
		}

		public string KeyName
		{
			get { return _keyName; }
			set { _keyName = value; }
		}

		public string Key
		{
			get
			{
				return _number + " " + _keyName;
			}
		}

		public Dictionary<string, string> Names
		{
			get { return _names; }
			set { _names = value; }
		}

		public Dictionary<string, string> Abbreviation
		{
			get { return _abbreviation; }
			set { _abbreviation = value; }
		}

		public Dictionary<string, string> SearchKeys
		{
			get { return _searchKeys; }
			set { _searchKeys = value; }
		}

		public Dictionary<string, string> Descriptions
		{
			get { return _descriptions; }
			set { _descriptions = value; }
		}
	}
}
