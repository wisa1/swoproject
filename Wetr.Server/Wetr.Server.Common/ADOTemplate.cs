using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Server.Common
{
    public class ADOTemplate
    {
        private IConnectionFactory connectionFactory;
        public delegate T RowMapper<T>(IDataRecord row);

        public ADOTemplate(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IEnumerable<T> Query<T>(string sql, RowMapper<T> mapper, SqlParameter[] parameters = null)
        {
            using (DbConnection connection = connectionFactory.CreateConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    AddParameters(parameters, command);

                    var items = new List<T>();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(mapper(reader));
                        }
                    }
                    return items;
                }
            }
        }

        public int Execute(string sql, SqlParameter[] parameters = null)
        {
            using (DbConnection connection = connectionFactory.CreateConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    AddParameters(parameters, command);

                    return command.ExecuteNonQuery();
                }
            }
        }

        private void AddParameters(SqlParameter[] parameters, DbCommand command)
        {
            if (parameters == null)
            {
                return;
            }
            foreach (var parameter in parameters)
            {
                DbParameter dbParameter = command.CreateParameter();
                dbParameter.ParameterName = parameter.Name;
                dbParameter.Value = parameter.Value;

                command.Parameters.Add(dbParameter);
            }
        }

    }
}
