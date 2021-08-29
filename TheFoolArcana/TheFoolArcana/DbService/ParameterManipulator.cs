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
    public class ParameterManipulator
    {
        internal virtual InputTranslator InputTranslator { get; }

        /// <summary>
        /// Creates a new <see cref="ParameterManipulator"/> instance.
        /// The <see cref="InputTra
        public ParameterManipulator()
        {
            InputTranslator = new InputTranslator();
        }

        /// <summary>
        /// Turns all of the input parameters from the input JSON data into an
        /// <see cref="SqlParameter"/> collection for adding to a database call.
        /// </summary>
        /// <param name="parameters">The .NET input parameters.</param>
        /// <returns></returns>
        public IEnumerable<SqlParameter> CreateParameters(IEnumerable<InputParameter> parameters)
        {
            IList<SqlParameter> sqlParams = new List<SqlParameter>();

            if (parameters != null)
            {
                foreach (InputParameter property in parameters)
                {
                    sqlParams.Add(CreateInputParameter(property));
                }
            }

            return sqlParams;
        }

        /// <summary>
        /// Turns a single input parameter into an <see cref="SqlParameter"/>.
        /// </summary>
        /// <param name="param">The .NET input parameter.</param>
        /// <returns></returns>
        public SqlParameter CreateInputParameter(InputParameter param)
        {
            var sqlParam = new SqlParameter()
            {
                //required and fixed data
                ParameterName = param.ParameterName,
                Direction = InputTranslator.GetDirection(param.Direction),
                SqlDbType = InputTranslator.GetType(param.SqlDbType)
            };

            //optional data
            //if SqlDbType is a variable type, Size is required (handled by db server)
            if (param.Size != null)
                sqlParam.Size = (int)param.Size;

            if (param.Value != null)
                if (sqlParam.SqlDbType == SqlDbType.Structured)
                    sqlParam.Value = InputTranslator.CreateDataTable(param.Value as JArray);
                else
                    sqlParam.Value = param.Value;
            else
                sqlParam.Value = DBNull.Value;

            return sqlParam;
        }

        /// <summary>
        /// Creates an <see cref="OutputParameter"/> from an <see cref="SqlParameter"/>'s name and value.
        /// </summary>
        /// <param name="param">The parameter to use.</param>
        /// <returns></returns>
        public OutputParameter CreateOutputParameter(SqlParameter param)
        {
            return new OutputParameter(param.ParameterName, param.Value);
        }
    }
}
