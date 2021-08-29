using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TheFoolArcana.Models.DbService;

namespace TheFoolArcana.DbService
{
    /// <summary>
    /// Responsible for communicating with the database.
    /// </summary>
    public class DbAccess
    {
        internal virtual ParameterManipulator Helpers { get; set; }

        /// <summary>
        /// Creates a new DbAccess object.
        /// The <see cref="Helpers"/> internal virtual property is automatically instantiated.
        /// </summary>
        public DbAccess()
        {
            Helpers = new ParameterManipulator();
        }

        /// <summary>
        /// Executes an ExecuteReader SQL call and returns all rows read. Also returns output parameters.
        /// </summary>
        /// <param name="conn">The connection to use.</param>
        /// <param name="spName">The name of the Stored Procedure to call.</param>
        /// <param name="parameters">
        /// A collection of parameters for the stored procedure. Output parameters are returned.
        /// </param>
        /// <returns>The rows read and the values of the output parameters passed.</returns>
        public MultiRowReaderOutput ExecuteReaderAllRows(
            SqlConnection conn, string schema,
            string spName, IEnumerable<SqlParameter> parameters)
        {
            JArray results = new JArray();

            using SqlCommand command = SetupSqlCommand(schema, spName, parameters, conn);
            command.Connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                results = ReadResultSet(reader);
            }

            MultiRowReaderOutput response = new MultiRowReaderOutput(results);
            foreach (SqlParameter param in parameters)
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                    response.OutputParameters.Add(param.ParameterName, param.Value);

            return response;
        }

        /// <summary>
        /// Sets up an <see cref="SqlCommand"/> object with schema, stored procedure name,
        /// connection, and stored procedure parameters. Does not open the SQL connection.
        /// </summary>
        /// <param name="schema">The schema of the stored procedure.</param>
        /// <param name="spName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">A collection of parameters for the stored procedure.</param>
        /// <param name="conn">The connection to use.</param>
        /// <returns></returns>
        internal SqlCommand SetupSqlCommand(
            string schema, string spName, IEnumerable<SqlParameter> parameters,
            SqlConnection conn)
        {
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
            SqlCommand command = new SqlCommand($"{schema}.usp_{spName}", conn);
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters.ToArray());

            return command;
        }

        /// <summary>
        /// Reads a single result from a data reader and returns the data as .NET JSON.
        /// </summary>
        /// <param name="reader">The connected data reader to use.</param>
        /// <returns>The data as .NET JSON (Newtonsoft).</returns>
        internal JArray ReadResultSet(SqlDataReader reader)
        {
            JArray resultSet = new JArray();
            while (reader.Read())
            {
                JObject row = new JObject();
                for (int i = 0, L = reader.FieldCount; i < L; i++)
                {
                    bool isDbNull = reader.IsDBNull(i);

                    string fieldName = reader.GetName(i);
                    object fieldValue = isDbNull ? null : reader.GetValue(i);
                    JValue value = new JValue(fieldValue);
                    row[fieldName] = value;
                }

                resultSet.Add(row);
            }

            return resultSet;
        }
    }
}
