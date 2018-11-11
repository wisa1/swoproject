using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Server.Common
{
    public class ConnectionFactory : IConnectionFactory
    {
        private DbProviderFactory dbProviderFactory;
        public string ConnectionString { get; }
        public string ProviderName { get; }

        public ConnectionFactory(string providerName, string connectionString)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;
            this.dbProviderFactory = DbProviderFactories.GetFactory(providerName);
        }

        public static ConnectionFactory CreateFromConfiguration(string key)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[key].ProviderName;
            return new ConnectionFactory(providerName, connectionString);
        }

        public DbConnection CreateConnection()
        {
            DbConnection connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = this.ConnectionString;
            connection.Open();
            return connection;
        }
    }
}
