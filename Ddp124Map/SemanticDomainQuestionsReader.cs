using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ddp124Map
{
	public class SemanticDomainQuestionsReader
	{
		static public SemanticDomainQuestionsCollection ReadFromFile(string filePath)
		{
			SemanticDomainQuestionsCollection allQuestions = new SemanticDomainQuestionsCollection();
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			XmlReader fileReader = XmlReader.Create(filePath, settings);

			while (fileReader.Read())
			{
				if (fileReader.Name == "semantic-domain-questions")
				{
					allQuestions = PopulateSemanticDomainsQuestionsFromXml(fileReader);
				}
			}
			fileReader.Close();
			return allQuestions;
		}

		static private SemanticDomainQuestionsCollection PopulateSemanticDomainsQuestionsFromXml(XmlReader fileReader)
		{
			if (!(fileReader.Name == "semantic-domain-questions"))
			{
				throw new InvalidOperationException(String.Format("Node is {0} but we expected 'semantic-domain-questions' node.", fileReader.Name));
			}

			SemanticDomainQuestionsCollection allQuestions = new SemanticDomainQuestionsCollection();

			string type = fileReader.GetAttribute("semantic-domain-type");
			if(type == "DDP4"){allQuestions.Version = SemanticDomainQuestionsCollection.SemanticDomainsVersion.DDP4;}
			else if (type == "DDP1") {allQuestions.Version = SemanticDomainQuestionsCollection.SemanticDomainsVersion.DDP1; }
			else {throw new ArgumentOutOfRangeException(String.Format("There is no type for {0}", type)); }

			allQuestions.WritingSystemId = fileReader.GetAttribute("lang");

			if (fileReader.IsEmptyElement) { return allQuestions; }

			while (fileReader.Read())
			{
				bool readerHasReachedEndOfSemanticDomainsQuestions =
						  fileReader.NodeType == XmlNodeType.EndElement && fileReader.Name == "semantic-domain-questions";

				if (readerHasReachedEndOfSemanticDomainsQuestions) { break; }

				if (fileReader.Name == "semantic-domain")
				{
					SemanticDomainQuestions questions = PopulateSemanticDomainQuestionsFromXml(fileReader);
					allQuestions.DomainKeyToQuestionsMap.Add(questions.SemanticDomainKey, questions);
				}
				else
				{
					throw new InvalidOperationException(
						String.Format("Malformed 'semantic-domain-questions' node found. Found an unexpected '{0}' element.", fileReader.Name));
				}
			}
			fileReader.Close();
			return allQuestions;
		}

		static private SemanticDomainQuestions PopulateSemanticDomainQuestionsFromXml(XmlReader fileReader)
		{
			if (fileReader.Name != "semantic-domain")
			{
				throw new InvalidOperationException(String.Format("Node is {0} but we expected 'semantic-domain' node.", fileReader.Name));
			}

			Guid guid = Guid.NewGuid();
			string id = fileReader.GetAttribute("id");

			SemanticDomainQuestions questions = new SemanticDomainQuestions(guid, id);

			if(fileReader.IsEmptyElement){return questions;}

			while (fileReader.Read())
			{
				bool readerHasReachedEndOfSemanticDomainsQuestions =
					   fileReader.NodeType == XmlNodeType.EndElement && fileReader.Name == "semantic-domain";


				if (readerHasReachedEndOfSemanticDomainsQuestions) { break; }

				if (fileReader.Name == "question")
				{
					string question = PopulateSemanticDomainQuestionFromXml(fileReader);
					questions.Questions.Add(question);
				}
				else
				{
					throw new InvalidOperationException(
						String.Format("Malformed 'semantic-domain' node found. found an unexpected '{0}' element.", fileReader.Name));
				}
			}
			Console.WriteLine("Populated questions");
			return questions;
		}

		static private string PopulateSemanticDomainQuestionFromXml(XmlReader fileReader)
		{
			if (fileReader.Name != "question")
			{
				throw new InvalidOperationException(String.Format("Node is {0} but we expected 'question' node.", fileReader.Name));
			}

			string question = fileReader.ReadString().Trim();

			Console.WriteLine("Populated question {0}", question);

			bool readerHasReachedEndOfQuestionElement =
				(fileReader.Name == "question" && fileReader.NodeType == XmlNodeType.EndElement);
			if (!readerHasReachedEndOfQuestionElement)
			{
				throw new InvalidOperationException(String.Format("Malformed 'question' node found. We expected to find a clsoing 'question' element, but instead found {0}.", fileReader.Name));
			}
			return question;
		}
	}
}
