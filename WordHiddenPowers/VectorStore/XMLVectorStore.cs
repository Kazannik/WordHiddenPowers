namespace WordHiddenPowers.VectorStore
{
	public partial class XMLVectorStore
	{
		public static string DatabaseFileName { get; private set; } = "VectorStore.xml";

		public static bool DefaultDatabaseFileExists()
		{
			string fileName = DatabaseFileName;
			return System.IO.File.Exists(fileName);
		}

		public void ReadDatabase()
		{
			ReadDatabase(DatabaseFileName);
		}

		public void ReadDatabase(string xmlFileName)
		{
			DatabaseFileName = xmlFileName;
			ReadXml(xmlFileName);
			AcceptChanges();
		}
				
		public void WriteDatabase()
		{
			WriteDatabase(DatabaseFileName);
		}

		public void WriteDatabase(string xmlFileName)
		{
			DatabaseFileName = xmlFileName;
			WriteXml(xmlFileName);
			AcceptChanges();
		}

		public void WriteSchema(string xsdFileName)
		{
			WriteXmlSchema(xsdFileName);
		}
	}
}
