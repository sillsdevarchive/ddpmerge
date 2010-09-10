using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	public class SemanticDomainReader
	{
		static public SemanticDomainCollection ReadFromFile(string filePath)
		{
			SemanticDomainCollection semanticDomains = new SemanticDomainCollection();
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			XmlReader fileReader = XmlReader.Create(filePath, settings);

			while (fileReader.Read())
			{
				if(fileReader.Name == "option")
				{
					semanticDomains.AddSemanticDomain(PopulateSemanticDomainFromXml(fileReader));
				}
			}
			fileReader.Close();
			return semanticDomains;
		}

		static private SemanticDomainInfo PopulateSemanticDomainFromXml(XmlReader fileReader)
		{
			if (fileReader.Name != "option")
			{
				throw new InvalidOperationException(String.Format("Node is {0} but we expected an 'option' node.",
																  fileReader.Name));
			}

			SemanticDomainInfo semanticDomain = null;

			if (fileReader.IsEmptyElement) {throw new InvalidOperationException("An empty 'option' element was found.");}

			while (fileReader.Read())
			{
				bool readerHasReachedEndOfOption =
					fileReader.NodeType == XmlNodeType.EndElement && fileReader.Name == "option";

				if (readerHasReachedEndOfOption) { break; }

				if (fileReader.Name == "key")
				{
					string key = PopulateKeyFromXml(fileReader);
					semanticDomain = new SemanticDomainInfo(key);
				}
				else if (fileReader.Name == "name")
				{
					semanticDomain.Names = PopulateFormsFromXml(fileReader);
				}
				else if (fileReader.Name == "abbreviation")
				{
					semanticDomain.Abbreviation = PopulateFormsFromXml(fileReader);
				}
				else if (fileReader.Name == "description")
				{
					semanticDomain.Descriptions = PopulateFormsFromXml(fileReader);
				}
				else if (fileReader.Name.ToLower() == "searchkeys")
				{
					semanticDomain.SearchKeys = PopulateFormsFromXml(fileReader);
				}
				else
				{
					throw new ApplicationException(String.Format("An unhandled Element {0} was found!",
																 fileReader.Name));
				}
			}
			if (semanticDomain == null)
			{
				throw new ApplicationException("An empty option was found!");
			}
			return semanticDomain;
		}

		static private KeyValuePair<string, string> PopulateFormFromXml(XmlReader fileReader)
		{
			if (fileReader.Name != "form")
			{
				throw new InvalidOperationException(String.Format("Node is {0} but we expected 'key' node.", fileReader.Name));
			}

			string writingsystemId = fileReader.GetAttribute("ws");
			string form = fileReader.ReadString();

			KeyValuePair<string, string> wsIdToFormMap = new KeyValuePair<string, string>(writingsystemId, form);
			bool readerHasReachedEndOfFormElement =
				(fileReader.Name == "form" && fileReader.NodeType == XmlNodeType.EndElement);
			if (!readerHasReachedEndOfFormElement)
			{
				throw new InvalidOperationException(String.Format("Malformed 'form' node found. We expected to find a clsoing 'form' element, but instead found {0}.", fileReader.Name));
			}
			Console.WriteLine("Returning Form: {0} -> {1}.", wsIdToFormMap.Key, wsIdToFormMap.Value);
			return wsIdToFormMap;
		}

		static private Dictionary<string, List<string>> PopulateFormsFromXml(XmlReader fileReader)
		{
			string formsCollectionsName = fileReader.Name;
			Dictionary<string, List<string>> wsIdToFormMap = new Dictionary<string, List<string>>();

			if(fileReader.IsEmptyElement){return new Dictionary<string, List<string>>();}

			while (fileReader.Read())
			{
				bool readerHasReachedEndOfCollection =
					(fileReader.Name == formsCollectionsName && fileReader.NodeType == XmlNodeType.EndElement);

				if (readerHasReachedEndOfCollection) { break; }

				if (fileReader.Name == "form")
				{
					KeyValuePair<string, string> wsIdAndForm = PopulateFormFromXml(fileReader);
					if(!wsIdToFormMap.ContainsKey(wsIdAndForm.Key))
					{
						List<string> listOfForms = new List<string>();
						wsIdToFormMap.Add(wsIdAndForm.Key, listOfForms);
					}
					wsIdToFormMap[wsIdAndForm.Key].Add(wsIdAndForm.Value);
				}
				else
				{
					throw new InvalidOperationException(String.Format("Found unexpected node '{0}' in FormsCollection xml.", fileReader.Name));
				}
			}
			Console.WriteLine("Returning {0}", formsCollectionsName);
			return wsIdToFormMap;
		}

		static private string PopulateKeyFromXml(XmlReader fileReader)
		{
			if (fileReader.Name != "key")
			{
				throw new InvalidOperationException(String.Format("Node is {0} but we expected 'key' node.", fileReader.Name));
			}
			string key = fileReader.ReadString();
			bool readerHasReachedKeyEndElement =
				(fileReader.Name == "key" && fileReader.NodeType == XmlNodeType.EndElement);
			if (!readerHasReachedKeyEndElement)
			{
				throw new InvalidOperationException(String.Format("Malformed 'key' node found. We expected to find a closing 'key' element, but instead found {0}.", fileReader.Name));
			}
			Console.WriteLine("key={0}", key);
			return key;
		}
	}
}
