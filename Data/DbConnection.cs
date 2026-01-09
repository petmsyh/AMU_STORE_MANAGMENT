using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AMU.store.Mngt.Data       
{
    public static class DbConnection
    {
        public static SqlConnection GetConnection()
        {
            var configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            if (!File.Exists(configPath))
                throw new InvalidOperationException("App config not found: " + configPath);

            var doc = XDocument.Load(configPath);
            var csElement = doc.Descendants("connectionStrings")
                .Descendants("add")
                .FirstOrDefault(x => (string)x.Attribute("name") == "AMU_DB");

            if (csElement == null)
                throw new InvalidOperationException("Connection string 'AMU_DB' not found in App.config.");

            var cs = (string)csElement.Attribute("connectionString");
            if (string.IsNullOrEmpty(cs))
                throw new InvalidOperationException("Connection string 'AMU_DB' is empty.");

            return new SqlConnection(cs);
        }
    }
}
