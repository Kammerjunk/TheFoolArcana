using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoolArcana.Models.DbService
{
    public class DbResponseNonQuery<T>
    {
        public T OutputModel { get; set; }
    }
}
