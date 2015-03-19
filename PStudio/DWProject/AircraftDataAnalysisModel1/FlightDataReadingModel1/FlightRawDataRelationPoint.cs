using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class FlightRawDataRelationPoint
    {
        public double YAxisParameterValue { get; set; }

        public double XAxisParameterValue { get; set; }

        public string FlightID { get; set; }

        public DateTime FlightDate { get; set; }

        public string XAxisParameterID { get; set; }

        public string YAxisParameterID { get; set; }
    }
}
