using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	public class SemanticDomainReader
	{
		static public IEnumerable<SemanticDomainInfo> ReadFromFile(string filePath)
		{
			List<SemanticDomainInfo> semanticDomains = new List<SemanticDomainInfo>();
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			XmlReader fileReader = XmlReader.Create(filePath, settings);

			while (fileReader.Read())
			{
				if(fileReader.Name == "option")
				{
					semanticDomains.Add(new OptionReader().PopulateSemanticDomainFromXml(fileReader));
				}
			}
			return semanticDomains;
		}
	}
}
