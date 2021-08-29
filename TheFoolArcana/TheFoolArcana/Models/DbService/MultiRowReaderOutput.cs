using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoolArcana.Models.DbService
{
    public class MultiRowReaderOutput
    {
        public JArray Output { get; set; }
        public IDictionary<string, object> OutputParameters { get; set; }

        public MultiRowReaderOutput(JArray rows)
        {
            Output = rows;
            OutputParameters = new Dictionary<string, object>();
        }
    }
}
