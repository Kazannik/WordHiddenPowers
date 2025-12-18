// Ignore Spelling: Utils

using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace WordHiddenPowers.Utils
{
	public static class Resource
	{
		public static string GetStringResource(string resourceName)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			StreamReader stream = new StreamReader(assembly.GetManifestResourceStream(resourceName));

			if (stream != null)
			{
				return stream.ReadToEnd();				
			}
			else
			{
				throw new ArgumentException ($"Ресурс '{resourceName}' не найден.");
			}
		}

		public static byte[] GetBytesResource(string resourceName)
		{
			return Encoding.UTF8.GetBytes(GetStringResource(resourceName: resourceName));
		}
	}
}
