using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class EnglishTownGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("34549300-4024-407c-a7cf-f561d7136f38"); }
		}

		public string Name
		{
			get { return "English Town Name Generator"; }
		}

		public Type Type
		{
			get { return typeof(IEnglishTownGenerator); }
		}

		public List<OleDbType> CompatibleDataTypes
		{
			get
			{
				return new List<OleDbType>
								{
									OleDbType.LongVarChar,
									OleDbType.LongVarWChar,
									OleDbType.VarChar,
									OleDbType.VarWChar
								};
			}
		}
	}

	public class EnglishTownGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
            get { return new Guid("34549300-4024-407c-a7cf-f561d7136f38"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((IEnglishTownGenerator)generator).GenerateTownNames((double)data[0]);

			return generatedData;
		}
	}

	public class EnglishTownGenerator : IEnglishTownGenerator
	{
		private static List<string> townLines;

		private readonly INumberGenerator numberGenerator;
		private readonly IFileDataProvider fileDataProvider;

		public EnglishTownGenerator(INumberGenerator numberGenerator, IFileDataProvider fileDataProvider)
		{
			this.numberGenerator = numberGenerator;
			this.fileDataProvider = fileDataProvider;
		}

		private List<string> GetTownNames()
		{
			if (townLines == null || townLines.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				townLines = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.EnglishTowns.txt");
			}

			return townLines;
		}

		public string GenerateTownName()
		{
			List<string> lines = GetTownNames();

			int randomNumber = numberGenerator.GenerateRandomNumber(0, lines.Count - 1);

			return lines[randomNumber];
		}

		public List<string> GenerateTownNames(double count)
		{
			List<string> cities = new List<string>();

			for (double i = 0; i < count; i++)
			{
				cities.Add(GenerateTownName());
			}

			return cities;
		}
	}
}
