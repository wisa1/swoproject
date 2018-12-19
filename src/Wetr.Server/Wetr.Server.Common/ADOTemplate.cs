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

        //Select
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, RowMapper<T> mapper, SqlParameter[] parameters = null)
        {
            using (DbConnection connection = connectionFactory.CreateConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    AddParameters(parameters, command);

                    var items = new List<T>();
                    using (IDataReader reader = await command.ExecuteReaderAsync())
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

        //Insert / Delete
        public async Task<int> ExecuteAsync(string sql, SqlParameter[] parameters = null)
        {
            using (DbConnection connection = connectionFactory.CreateConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    AddParameters(parameters, command);

                    return (await command.ExecuteNonQueryAsync());                   
                }
            }
        }

        //Aggregates.
        public async Task<decimal> ExecuteScalarAsync(string sql, SqlParameter[] parameters = null)
        {
            using (DbConnection connection = connectionFactory.CreateConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    AddParameters(parameters, command);
                    return Convert.ToDecimal(await command.ExecuteScalarAsync());
                }
            }
        }

        #region private helper functions
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
        #endregion

    }
}
