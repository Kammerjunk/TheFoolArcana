using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoolArcana.Models.DbService
{
    public class InputParameter
    {
        [Required]
        [DisplayName("ParameterName")]
        public string ParameterName { get; set; }

        [Required]
        [DisplayName("Direction")]
        public string Direction { get; set; }

        [Required]
        [DisplayName("SqlDbType")]
        public string SqlDbType { get; set; }

        [DisplayName("Size")]
        public int? Size { get; set; }

        [DisplayName("Value")]
        public object Value { get; set; }

        public InputParameter(
            string parameterName, string direction, string sqlDbType,
            int? size = null, object value = null)
        {
            ParameterName = parameterName;
            Direction = direction;
            SqlDbType = sqlDbType;
            Size = size;
            Value = value;
        }
    }
}
