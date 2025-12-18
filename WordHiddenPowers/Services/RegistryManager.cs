using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System;

namespace WordHiddenPowers.Services
{
	internal static class RegistryManager
	{
		private static readonly string AppKeyPath = @"Software\KazannikSoft\WordHiddenPowers";

		public static void SaveSetting(string keyName, string value)
		{
			// Открываем раздел реестра для записи (если не существует, создается)
			RegistryKey key = Registry.CurrentUser.CreateSubKey(AppKeyPath);
			key.SetValue(keyName, value, RegistryValueKind.String); // Сохраняем как строку
			key.Close();
		}

		public static string GetSetting(string keyName, string defaultValue = "")
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey(AppKeyPath);
			if (key != null)
			{
				object value = key.GetValue(keyName);
				key.Close();
				return value?.ToString() ?? defaultValue;
			}
			return defaultValue;
		}
	}
}

// Пример использования:
// RegistryManager.SaveSetting("UserName", "Alice");
// string userName = RegistryManager.GetSetting("UserName", "Guest");
// Console.WriteLine($"User: {userName}");