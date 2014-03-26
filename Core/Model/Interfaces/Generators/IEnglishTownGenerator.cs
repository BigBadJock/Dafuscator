using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IEnglishTownGenerator
	{
		string GenerateTownName();
		List<string> GenerateTownNames(double count);
	}
}