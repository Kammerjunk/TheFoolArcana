using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoolArcana.Models.DbService
{
    public class DbResponseAllRows<TRows>
    {
        public IEnumerable<TRows> Output { get; set; }
    }

    public class DbResponseAllRows<TRows, TParams> : DbResponseAllRows<TRows>
    {
        public DbResponseAllRows<TParams> OutputParameters { get; set; }
    }
}
