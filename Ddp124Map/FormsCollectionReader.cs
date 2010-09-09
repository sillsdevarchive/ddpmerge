using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	public class FormsCollectionReader
	{
		public Dictionary<string, string> PopulateFormsFromXml(XmlReader fileReader)
		{
			string formsCollectionsName = fileReader.Name;
			Dictionary<string, string> wsIdToFormMap = new Dictionary<string, string>();
			while(fileReader.Read())
			{
				bool readerHasReachedEndOfCollection =
					(fileReader.Name == formsCollectionsName && fileReader.NodeType == XmlNodeType.EndElement);

				if (readerHasReachedEndOfCollection){break;}

				if (fileReader.Name == "form")
				{
					KeyValuePair<string, string> wsIdAndForm = new FormReader().PopulateFormFromXml(fileReader);
					wsIdToFormMap.Add(wsIdAndForm.Key, wsIdAndForm.Value);
				}
				else
				{
					throw new InvalidOperationException(String.Format("Found unexpected node '{0}' in FormsCollection xml.", fileReader.Name));
				}
			}
			Console.WriteLine("Returning {0}", formsCollectionsName);
			return wsIdToFormMap;
		}
	}
}
