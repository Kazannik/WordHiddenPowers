using System.Data;
using System.Linq;

namespace WordHiddenPowers.Repository
{
	partial class VectorDataSet
	{
		public string EmbedLLModelName
		{
			get => Setting.GetValue(Const.Globals.EMBED_LLMODEL_VARIABLE_NAME);
			set => Setting.SetValue(Const.Globals.EMBED_LLMODEL_VARIABLE_NAME, value);
		}

		partial class SettingDataTable
		{
			public string GetValue(string key)
			{
				return GetOrDefault(key: key)?["value"] as string;
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
					Rows.Add(new object[] { key, value });
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

			private SettingRow Get(string key) => (from SettingRow row in this
												   where row.RowState != DataRowState.Deleted
												   && row.key.Equals(key)
												   select row).First();
			private bool Exists(string key) => (from SettingRow row in this
												where row.RowState != DataRowState.Deleted
												&& row.key.Equals(key)
												select row).Any();
		}

		partial class WordFilesDataTable
		{
			public WordFilesRow Get(string fileName) => (from WordFilesRow row in this
														 where row.RowState != DataRowState.Deleted
														 && row.FileName.Equals(fileName)
														 select row).First();

			public bool Exists(string fileName) => (from WordFilesRow row in this
													where row.RowState != DataRowState.Deleted
													&& row.FileName.Equals(fileName)
													select row).Any();
		}
	}
}
