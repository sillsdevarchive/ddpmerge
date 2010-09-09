using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	interface XmlToSemanticDomainReader
	{
		void PopulateSemanticDomainFromXml(XmlReader fileReader, SemanticDomainInfo semanticDomainToPopulate);
	}
}
