using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.AircraftModel1
{
    public class FlightDataContentSegment
    {
        public float Value
        {
            get;
            set;
        }

        public string DataTypeStr
        {
            get;
            set;
        }


        public string SegmentName { get; set; }
    }
}
