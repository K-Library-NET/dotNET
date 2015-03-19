using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    public class FlightRawDataRelationPoint
    {
        public ObjectId Id
        {
            get;
            set;
        }

        public string FlightID
        {
            get;
            set;
        }

        public string XAxisParameterID
        {
            get;
            set;
        }

        public string YAxisParameterID
        {
            get;
            set;
        }

        public double XAxisParameterValue
        {
            get;
            set;
        }

        public double YAxisParameterValue
        {
            get;
            set;
        }

        public DateTime FlightDate
        {
            get;
            set;
        }
    }
}
