using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	public class FormReader
	{
		public KeyValuePair<string, string> PopulateFormFromXml(XmlReader fileReader)
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
	}
}
