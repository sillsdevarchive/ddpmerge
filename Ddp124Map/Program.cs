using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ddp124Map
{
	class Program
	{
		static void Main(string[] args)
		{
			string filePathToDdp1 = @"D:\SoftwareDevelopment\Ddp124Map\DDP4\Ddp1.xml";
			string filePathToDdp4 = @"D:\SoftwareDevelopment\Ddp124Map\DDP4\Ddp4.xml";
			string filePathToThaiDdp1Questions = @"D:\SoftwareDevelopment\Ddp124Map\DDP4\Ddp4Questions-th.xml";
			string filePathToEnglishDdp4Questions = @"D:\SoftwareDevelopment\Ddp124Map\DDP4\Ddp4Questions-en.xml";
			string filePathToMap = @"D:\SoftwareDevelopment\Ddp124Map\DDP4\Semantic Domains v1 to v4 map.txt";

			string newDdpFile = @"D:\SoftwareDevelopment\Ddp124Map\DDP4\newDdp.xml";
			string newQuestionsFile = @"D:\SoftwareDevelopment\Ddp124Map\DDP4\newQuestion.xml";

			SemanticDomainCollection semanticDomainsDdp1 = SemanticDomainReader.ReadFromFile(filePathToDdp1);
			SemanticDomainCollection semanticDomainsDdp4 = SemanticDomainReader.ReadFromFile(filePathToDdp4);

			Dictionary<string, string> map = Ddp4to1MapReader.ReadFromFile(filePathToMap);
			foreach (SemanticDomainInfo ddp1SemanticDomain in semanticDomainsDdp1.AllSemanticDomains)
			{
				string correspondingNumberInDdp4 = ddp1SemanticDomain.Number;
				if(map.ContainsKey(ddp1SemanticDomain.Number))
				{
					correspondingNumberInDdp4 = map[ddp1SemanticDomain.Number];
				}
				SemanticDomainInfo semanticDomainToMergeInto = semanticDomainsDdp4.GetSemanticDomainWithNumber(correspondingNumberInDdp4);
				semanticDomainToMergeInto.MergeInTranslations(ddp1SemanticDomain);
			}

			SemanticDomainWriter.WriteToFile(newDdpFile, semanticDomainsDdp4);

			SemanticDomainQuestionsCollection thaiDdp1Questions =
				SemanticDomainQuestionsReader.ReadFromFile(filePathToThaiDdp1Questions);

			SemanticDomainQuestionsCollection thaiDdp4Questions = new SemanticDomainQuestionsCollection(thaiDdp1Questions.Version, thaiDdp1Questions.WritingSystemId);

			foreach (SemanticDomainQuestions domainQuestions in thaiDdp1Questions.DomainKeyToQuestionsMap.Values)
			{
				string semanticDomainKey = GetMappedSemanticDomainKey(map, semanticDomainsDdp4, domainQuestions);

				bool questionsForDomainAlreadyExist =
					thaiDdp4Questions.DomainKeyToQuestionsMap.ContainsKey(semanticDomainKey);

				if (!questionsForDomainAlreadyExist)
				{
					SemanticDomainQuestions newQuestions = new SemanticDomainQuestions(Guid.NewGuid(), semanticDomainKey);
					thaiDdp4Questions.DomainKeyToQuestionsMap.Add(semanticDomainKey, newQuestions);
				}
				thaiDdp4Questions.DomainKeyToQuestionsMap[semanticDomainKey].Questions = domainQuestions.Questions;
			}

			SemanticDomainQuestionsWriter.WriteToFile(newQuestionsFile, thaiDdp4Questions);

			Console.ReadLine();
		}

		private static string GetMappedSemanticDomainKey(Dictionary<string, string> map, SemanticDomainCollection semanticDomainsDdp4, SemanticDomainQuestions domainQuestions)
		{
			string semanticDomainNumber = domainQuestions.Number;
			if(map.ContainsKey(domainQuestions.Number))
			{
				semanticDomainNumber = map[domainQuestions.Number];
			}
			return semanticDomainsDdp4.GetSemanticDomainWithNumber(semanticDomainNumber).Key;
		}

		private static void CompareFiles(string filePathToDdp1, string newDdpFile)
		{
			StreamReader original = new StreamReader(filePathToDdp1);
			StreamReader newFile = new StreamReader(newDdpFile);

			string originalLine;
			string newLine;
			int i = 0;
			while ((originalLine = original.ReadLine()) != null)
			{
				i++;
				newLine = newFile.ReadLine();
				if(newLine.Trim() != originalLine.Trim())
				{
					if(originalLine.Contains("guid")) continue;
					Console.WriteLine(originalLine);
					Console.WriteLine(newLine);
					throw new ApplicationException(String.Format("The two files differ at line {0}. Please see the console for differences.", i));
				}
			}

		}
	}
}
