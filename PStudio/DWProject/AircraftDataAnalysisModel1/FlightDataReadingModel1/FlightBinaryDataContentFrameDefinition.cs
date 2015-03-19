using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.AircraftModel1
{
    public class FlightBinaryDataContentFrameDefinition
    {
        public int BytesCount
        {
            get;
            set;
        }

        public FlightBinaryDataContentSegmentDefinition[] Segments
        {
            get;
            set;
        }
    }
}
