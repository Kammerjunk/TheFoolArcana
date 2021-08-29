using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoolArcana.DbService
{
    /// <summary>
    /// Contains methods for manipulating inputs from the API to inputs to the SQL server.
    /// </summary>
    public class InputTranslator
    {
        /// <summary>
        /// Gets the <see cref="ParameterDirection"/> enum by matching string input.
        /// Throws an <see cref="ArgumentException"/> if the input is not recognised.
        /// </summary>
        /// <param name="input">The string value to convert to an enum.</param>
        /// <returns>The <see cref="ParameterDirection"/> value matching the input.</returns>
        public ParameterDirection GetDirection(string input)
        {
            bool parsed = Enum.TryParse(input, true, out ParameterDirection result);
            if (parsed)
                return result;
            else
                throw new ArgumentException("Parameter direction not recognised: " + input);
        }

        /// <summary>
        /// Gets the <see cref="SqlDbType"/> enum by parsing string input.
        /// Throws an <see cref="ArgumentException"/> if the input does not match an enum value.
        /// </summary>
        /// <param name="input">The string value to convert to an enum.</param>
        /// <returns>The <see cref="SqlDbType"/> value matching the input.</returns>
        public SqlDbType GetType(string input)
        {
            bool parsed = Enum.TryParse(input, true, out SqlDbType result);
            if (parsed)
                return result;
            else
                throw new ArgumentException("Parameter type not recognised: " + input);
        }

        /// <summary>
        /// Converts a JSON array of objects into an SQL table for an input
        /// parameter. This lets stored procedures be called with table types.
        /// It is important to note that this method is not aware of the actual structure
        /// of any table types prior to the Stored Procedure being called. If the Stored
        /// Procedure is called with an incorrect table, it will fail in pre-execution.
        /// </summary>
        /// <param name="array">
        /// A JSON array of objects representing rows in a table. The property names of the first object are used for the table columns.
        /// </param>
        /// <returns>A <see cref="DataTable"/> representation of a table value.</returns>
        public DataTable CreateDataTable(JArray array)
        {
            var dataTable = new DataTable();

            if (array.Count < 1)
                return dataTable; // SqlClient will handle errors by throwing an exception before executing query

            JObject firstObj = array.First as JObject;
            foreach (JProperty prop in firstObj.Properties())
            {
                object value = ((JValue)prop.Value).Value;
                Type propType = value == null ? typeof(string) : value.GetType();
                dataTable.Columns.Add(prop.Name, propType);
            }

            foreach (JObject row in array)
            {
                var dataRow = new List<object>();
                foreach (JProperty col in row.Properties())
                {
                    object val = ((JValue)col.Value).Value;
                    dataRow.Add(val);
                }
                dataTable.Rows.Add(dataRow.ToArray());
            }

            return dataTable;
        }
    }
}
