using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordHiddenPowers.Utils
{
	public static class Json
	{
		private static string GetIndent(int count)
		{
			return new string('\x0020', count);
		}

		public static string Serialize(IEnumerable<(string name, string type, string description, string parameters, bool strict)> attributes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\n");
			stringBuilder.Append(GetIndent(2) + "\"type\":\"object\",\n");
			stringBuilder.Append(GetIndent(2) + "\"properties\":\"{\n");

			foreach (var item in attributes) {
				stringBuilder.Append(GetIndent(4) + "\"" + item.name + "\": {\n");
				stringBuilder.Append(GetIndent(6) + "\"type\": \"" + item.type + "\",\n");
				stringBuilder.Append(GetIndent(6) + "\"description\": \"" + item.description + "\"\n");
				stringBuilder.Append(GetIndent(4) + "}");
				if (!item.Equals(attributes.Last())) stringBuilder.Append(",");
				stringBuilder.Append("\n");
			}

			stringBuilder.Append(GetIndent(2) + "},\n");
			stringBuilder.Append(GetIndent(2) + "\"required\": [ " + " ]");			
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		public static byte[] SerializeToBytes(IEnumerable<(string name, string type, string description, string parameters, bool strict)> attributes)
		{
			return Encoding.UTF8.GetBytes(Serialize(attributes: attributes));
		}		
	}
}
