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
		private Dictionary<string, List<string>> _names = new Dictionary<string, List<string>>();
		private Dictionary<string, List<string>> _abbreviation = new Dictionary<string, List<string>>();
		private Dictionary<string, List<string>> _searchKeys = new Dictionary<string, List<string>>();
		private Dictionary<string, List<string>> _descriptions = new Dictionary<string, List<string>>();

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

		public Dictionary<string, List<string>> Names
		{
			get { return _names; }
			set { _names = value; }
		}

		public Dictionary<string, List<string>> Abbreviation
		{
			get { return _abbreviation; }
			set { _abbreviation = value; }
		}

		public Dictionary<string, List<string>> SearchKeys
		{
			get { return _searchKeys; }
			set { _searchKeys = value; }
		}

		public Dictionary<string, List<string>> Descriptions
		{
			get { return _descriptions; }
			set { _descriptions = value; }
		}

		internal void MergeInTranslations(SemanticDomainInfo ddp1SemanticDomain)
		{
				this.Names = MergeTranslationsInFormCollections(this.Names, ddp1SemanticDomain.Names);
				this.Descriptions = MergeTranslationsInFormCollections(this.Descriptions, ddp1SemanticDomain.Descriptions);
				this.Abbreviation = MergeTranslationsInFormCollections(this.Abbreviation, ddp1SemanticDomain.Abbreviation);
				this.SearchKeys = MergeTranslationsInFormCollections(this.SearchKeys, ddp1SemanticDomain.SearchKeys);
		}

		private Dictionary<string, List<string>> MergeTranslationsInFormCollections(Dictionary<string, List<string>> thisFormCollection, Dictionary<string, List<string>> otherFormCollection)
		{
			foreach (KeyValuePair<string, List<string>> wsIdAndForms in otherFormCollection)
			{
				if (!thisFormCollection.ContainsKey(wsIdAndForms.Key))
				{
					thisFormCollection.Add(wsIdAndForms.Key, wsIdAndForms.Value);
				}
			}
			return thisFormCollection;
		}

		private bool FormCollectionsContainEvenOneIdenticalLine(Dictionary<string, List<string>> thisFormCollection, Dictionary<string, List<string>> otherFormCollection)
		{
			foreach (KeyValuePair<string, List<string>> wsIdAndForms in thisFormCollection)
			{
				if(otherFormCollection.ContainsKey(wsIdAndForms.Key))
				{
					foreach (string formInOther in otherFormCollection[wsIdAndForms.Key])
					{
						foreach (string formInThis in wsIdAndForms.Value)
						{
							if(!String.IsNullOrEmpty(formInOther) && formInThis.Contains(formInOther))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}
	}
}
