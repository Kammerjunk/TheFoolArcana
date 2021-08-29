using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoolArcana.Models.DbService
{
    public class OutputParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public OutputParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
