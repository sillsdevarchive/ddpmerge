using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	public class OptionReader
	{
		public SemanticDomainInfo PopulateSemanticDomainFromXml(XmlReader fileReader)
		{
			if (fileReader.Name != "option")
			{
				throw new InvalidOperationException(String.Format("Node is {0} but we expected an 'option' node.",
																  fileReader.Name));
			}

			SemanticDomainInfo semanticDomain = null;
			while (fileReader.Read())
			{
				bool readerHasReachedEndOfOption =
					fileReader.NodeType == XmlNodeType.EndElement && fileReader.Name == "option";

				if (readerHasReachedEndOfOption){break;}

				if (fileReader.Name == "key")
				{
					string key = new KeyReader().PopulateKeyFromXml(fileReader);
					semanticDomain = new SemanticDomainInfo(key);
				}
				else if (fileReader.Name == "name")
				{
					semanticDomain.Names = new FormsCollectionReader().PopulateFormsFromXml(fileReader);
				}
				else if (fileReader.Name == "abbreviation")
				{
					semanticDomain.Abbreviation = new FormsCollectionReader().PopulateFormsFromXml(fileReader);
				}
				else if (fileReader.Name == "description")
				{
					semanticDomain.Descriptions = new FormsCollectionReader().PopulateFormsFromXml(fileReader);
				}
				else if (fileReader.Name == "searchkeys")
				{
					semanticDomain.Descriptions = new FormsCollectionReader().PopulateFormsFromXml(fileReader);
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
	}
}
