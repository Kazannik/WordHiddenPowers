using System;

#if DEVELOP
namespace PatternsWizardBoxDeveloper.Services
#else
namespace WordHiddenPowers.Services
#endif
{
	static partial class Searcher
	{
		internal static string[] GetPatterns(string keyword)
		{
			string[] patterns = keyword.Split(["';'"], StringSplitOptions.RemoveEmptyEntries);
			if (patterns.Length > 0)
			{
				if (patterns[0].Length > 0
					&& patterns[0][0] == 39
					&& patterns[^1][^1] == 39)
				{
					patterns[0] = patterns[0].Substring(1);
					patterns[^1] = patterns[^1][..(patterns[^1].Length - 1)];
				}
			}
			return patterns;
		}
	}
}
