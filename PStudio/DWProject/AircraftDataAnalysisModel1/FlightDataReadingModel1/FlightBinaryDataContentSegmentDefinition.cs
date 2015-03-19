using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.AircraftModel1
{
    public class FlightBinaryDataContentSegmentDefinition
    {
        public int BytesCount
        {
            get;
            set;
        }

        public string SegmentName
        {
            get;
            set;
        }

        public string DataTypeStr
        {
            get;
            set;
        }

        public FlightBitDataContentSegmentDefinition[] BitsDefinition
        {
            get;
            set;
        }
    }

    public class FlightBitDataContentSegmentDefinition : FlightBinaryDataContentSegmentDefinition
    {
        public int BitsCount
        {
            get;
            set;
        }
    }
}
