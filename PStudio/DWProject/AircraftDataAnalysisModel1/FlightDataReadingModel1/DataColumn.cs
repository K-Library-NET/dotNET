using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class DataColumn
    {
        public DataColumn()
        {
            this.DataType = typeof(object);
        }

        public Type DataType { get; set; }
        public string Caption { get; set; }
        public string ColumnName { get; set; }
    }
}
