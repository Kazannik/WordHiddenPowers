// Ignore Spelling: Utils

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace WordHiddenPowers.Utils
{
	static class Resource
	{
		public static string GetStringResource(string resourceName)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			StreamReader stream = new(assembly.GetManifestResourceStream(resourceName));

			if (stream != null)
			{
				return stream.ReadToEnd();
			}
			else
			{
				throw new ArgumentException($"Ресурс '{resourceName}' не найден.");
			}
		}

		public static byte[] GetBytesResource(string resourceName)
		{
			return Encoding.UTF8.GetBytes(GetStringResource(resourceName: resourceName));
		}

		/// <summary>
		/// Получить GUID-Продукта.
		/// </summary>
		/// <returns></returns>
		public static string GetAddInGuid()
		{
			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false);
			if (attributes.Length > 0)
			{
				return ((GuidAttribute)attributes[0]).Value;
			}
			return string.Empty;
		}
	}
}
