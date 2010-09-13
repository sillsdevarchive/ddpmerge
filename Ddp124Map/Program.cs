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
			string filePathToMap = @"D:\SoftwareDevelopment\Ddp124Map\DDP4\Semantic Domains v1 to v4 map.txt";

			string newDdpFile = @"D:\SoftwareDevelopment\Ddp124Map\DDP4\newDdp.xml";

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

			Console.ReadLine();
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
				if(newLine != originalLine)
				{
					//if(originalLine.Contains("guid")) break;
					Console.WriteLine(originalLine);
					Console.WriteLine(newLine);
					throw new ApplicationException(String.Format("The two files differ at line {0}. Please see the console for differences.", i));
				}
			}

		}
	}
}
