using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	class KeyReader
	{
		public string PopulateKeyFromXml(XmlReader fileReader)
		{
			if(fileReader.Name != "key")
			{
				throw new InvalidOperationException(String.Format("Node is {0} but we expected 'key' node.", fileReader.Name));
			}
			string key= fileReader.ReadString();
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
