using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	public class SemanticDomainQuestionsWriter
	{
		static public void WriteToFile(string path, SemanticDomainQuestionsCollection questionsCollection)
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.Encoding = Encoding.UTF8;
			settings.ConformanceLevel = ConformanceLevel.Document;
			XmlWriter fileWriter = XmlWriter.Create(path, settings);
			fileWriter.WriteStartDocument();
			fileWriter.WriteStartElement("semantic-domain-questions");

			string type = "DDP1";
			if(questionsCollection.Version == SemanticDomainQuestionsCollection.SemanticDomainsVersion.DDP4)
			{
				type = "DDP4";
			}
			fileWriter.WriteAttributeString("semantic-domain-type", type);
			fileWriter.WriteAttributeString("lang", questionsCollection.WritingSystemId);

			foreach (SemanticDomainQuestions questions in questionsCollection.DomainKeyToQuestionsMap.Values)
			{
				WriteSemanticDomainQuestions(fileWriter, questions);
			}
			fileWriter.WriteEndElement();
			fileWriter.Close();
		}

		private static void WriteSemanticDomainQuestions(XmlWriter fileWriter, SemanticDomainQuestions domainKeyToQuestionsMap)
		{
			fileWriter.WriteStartElement("semantic-domain");
			fileWriter.WriteAttributeString("guid", domainKeyToQuestionsMap.Guid.ToString());
			fileWriter.WriteAttributeString("id", domainKeyToQuestionsMap.SemanticDomainKey);

			foreach (string question in domainKeyToQuestionsMap.Questions)
			{
				WriteQuestion(fileWriter, question);
			}
			fileWriter.WriteEndElement();
		}

		private static void WriteQuestion(XmlWriter fileWriter, string question)
		{
			fileWriter.WriteStartElement("question");
			fileWriter.WriteString(question);
			fileWriter.WriteEndElement();
		}
	}
}
