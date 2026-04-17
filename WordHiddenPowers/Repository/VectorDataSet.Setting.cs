using System.Data;
using System.Linq;

namespace WordHiddenPowers.Repository
{
	public partial class VectorDataSet
	{
		private const string DEFAULT_EMBEDDING_MOLDEL_NAME = "mistral:latest";

		#region Setting

		public string EmbeddingModelName
		{
			get => GetValue("EmbeddingModelName", DEFAULT_EMBEDDING_MOLDEL_NAME);

			set => SetValue("EmbeddingModelName", value);
		}

		public string GetValue(string key, string defaultValue)
		{
			DataRow row = GetOrDefault(key: key);
			return row == null ? defaultValue : row["value"] as string;
		}

		public void SetValue(string key, string value)
		{
			if (Exists(key))
			{
				SettingRow row = Get(key: key) as SettingRow;
				row.BeginEdit();
				row.value = value;
				row.EndEdit();
			}
			else
			{
				Setting.Rows.Add(new object[] { key, value });
			}
		}

		public void RemoveValue(string key)
		{
			DataRow row = GetOrDefault(key: key);
			row?.Delete();
		}

		private DataRow GetOrDefault(string key)
		{
			if (Exists(key: key))
			{
				return Get(key: key);
			}
			else
			{
				return null;
			}
		}

		private DataRow Get(string key) => (from DataRow row in Setting
											where row.RowState != DataRowState.Deleted
											&& row["key"].Equals(key)
											select row).First();

		private bool Exists(string key) => (from DataRow row in Setting
											where row.RowState != DataRowState.Deleted
											&& row["key"].Equals(key)
											select row).Any();

		#endregion


	}
}
