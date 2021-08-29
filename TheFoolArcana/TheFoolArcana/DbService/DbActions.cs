using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TheFoolArcana.Models.DbService;
using TheFoolArcana.Models.Options;

namespace TheFoolArcana.DbService
{
    public class DbActions
    {
        private DbServiceOptions Options { get; }

        internal virtual DbAccess DbAccess { get; }
        internal virtual ParameterManipulator ParameterManipulator { get; }

        public DbActions(DbServiceOptions options)
        {
            Options = options;

            DbAccess = new DbAccess();
            ParameterManipulator = new ParameterManipulator();
        }

        /// <summary>
        /// Executes a Stored Procedure as a row reader.
        /// This method reads all rows returned and parses any parameters passed in Output direction.
        /// </summary>
        /// <param name="schema">The schema of the stored procedure.</param>
        /// <param name="spName">The name of the Stored Procedure to execute.</param>
        /// <param name="parameters">
        /// A collection of <see cref="InputParameter"/> inputs to be turned into SqlParameters.
        /// </param>
        /// <returns>
        /// The data output of the Stored Procedure structured as a JSON object.
        /// The rows are returned as an array named <c>Output</c>.
        /// The output parameters are returned as an object named
        /// <c>OutputParameters</c> in the format <c>"Key": "Value"</c>
        /// </returns>
        public JToken ExecuteReaderAllRows(string schema, string spName, IEnumerable<InputParameter> parameters)
        {
            IEnumerable<SqlParameter> sqlParams = ParameterManipulator.CreateParameters(parameters);

            using SqlConnection conn = new SqlConnection(Options.ConnString);
            MultiRowReaderOutput output = DbAccess.ExecuteReaderAllRows(conn, schema, spName, sqlParams);

            return JToken.FromObject(output);
        }
    }
}
