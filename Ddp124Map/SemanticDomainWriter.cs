using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	public class SemanticDomainWriter
	{
		static public void WriteToFile(string path, SemanticDomainCollection semanticDomains)
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.Encoding = Encoding.UTF8;
			settings.ConformanceLevel = ConformanceLevel.Document;
			XmlWriter fileWriter = XmlWriter.Create(path, settings);
			fileWriter.WriteStartDocument();
			fileWriter.WriteStartElement("optionsList");
			fileWriter.WriteAttributeString("id","DDP4");
			fileWriter.WriteAttributeString("guid", Guid.NewGuid().ToString());
			fileWriter.WriteStartElement("options");
			foreach (SemanticDomainInfo semanticDomain in semanticDomains.AllSemanticDomains)
			{
				WriteSemanticDomainToFile(fileWriter, semanticDomain);
			}
			fileWriter.WriteEndElement();
			fileWriter.WriteEndElement();
			fileWriter.Close();
		}

		private static void WriteSemanticDomainToFile(XmlWriter fileWriter, SemanticDomainInfo semanticDomain)
		{
			fileWriter.WriteStartElement("option");
			WriteKeyToFile(fileWriter, semanticDomain.Key);
			WriteFormCollectionToFile(fileWriter, semanticDomain.Names, "name");
			WriteFormCollectionToFile(fileWriter, semanticDomain.Abbreviation, "abbreviation");
			WriteFormCollectionToFile(fileWriter, semanticDomain.Descriptions, "description");
			WriteFormCollectionToFile(fileWriter, semanticDomain.SearchKeys, "searchkeys");
			fileWriter.WriteEndElement();
		}

		private static void WriteFormCollectionToFile(XmlWriter fileWriter, Dictionary<string, List<string>> formCollection, string nodeName)
		{
			fileWriter.WriteStartElement(nodeName);
			foreach (KeyValuePair<string, List<string>> wsIdAndForms in formCollection)
			{
				foreach (string form in wsIdAndForms.Value)
				{
					WriteFormToFile(fileWriter, wsIdAndForms.Key, form);
				}
			}
			fileWriter.WriteEndElement();
		}

		private static void WriteFormToFile(XmlWriter fileWriter, string wsId, string form)
		{

			fileWriter.WriteStartElement("form");
			fileWriter.WriteAttributeString("ws", wsId);
			fileWriter.WriteString(form);
			fileWriter.WriteEndElement();
		}

		private static void WriteKeyToFile(XmlWriter fileWriter, string key)
		{
			fileWriter.WriteStartElement("key");
			fileWriter.WriteString(key);
			fileWriter.WriteEndElement();
		}
	}
}
