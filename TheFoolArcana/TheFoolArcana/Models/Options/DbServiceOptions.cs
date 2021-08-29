using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoolArcana.Models.Options
{
    public class DbServiceOptions
    {
        public const string DbService = "DbService";

        public string ConnString { get; set; }
    }
}
