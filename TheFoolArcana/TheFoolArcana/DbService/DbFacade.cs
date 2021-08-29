using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFoolArcana.Models.DbService;
using TheFoolArcana.Models.Options;

namespace TheFoolArcana.DbService
{
    public class DbFacade
    {
        public DbServiceOptions Options { get; }
        private DbActions Actions { get; }

        public DbFacade(IConfiguration config)
        {
            Options = new DbServiceOptions();
            config.GetSection(DbServiceOptions.DbService).Bind(Options);

            Actions = new DbActions(Options);
        }

        public DbResponseAllRows<TRows> AllRows<TRows>(
            string spName, string schema = null,
            IEnumerable<InputParameter> queryParameters = null)
        {
            schema ??= "dbo";
            JToken response = Actions.ExecuteReaderAllRows(schema, spName, queryParameters);

            return response.ToObject<DbResponseAllRows<TRows>>();
        }
    }
}
