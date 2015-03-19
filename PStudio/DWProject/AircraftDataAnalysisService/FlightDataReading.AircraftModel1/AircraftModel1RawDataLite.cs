using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.AircraftModel1
{
    public class AircraftModel1RawDataLite
    {
        public int Second
        {
            get;
            set;
        }
    }

    public class AircraftModel1RawDataLite_KG : AircraftModel1RawDataLite
    {
        public int KG1
        {
            get;
            set;
        }

        public int KG2
        {
            get;
            set;
        }

        public int KG3
        {
            get;
            set;
        }

        public int KG4
        {
            get;
            set;
        }

        public int KG5
        {
            get;
            set;
        }

        public int KG6
        {
            get;
            set;
        }

        public int KG7
        {
            get;
            set;
        }

        public int KG8
        {
            get;
            set;
        }

        public int KG9
        {
            get;
            set;
        }

        public int KG10
        {
            get;
            set;
        }

        public int KG11
        {
            get;
            set;
        }

        public int KG12
        {
            get;
            set;
        }

        public int KG13
        {
            get;
            set;
        }

        public int KG14
        {
            get;
            set;
        }

        public int KG15
        {
            get;
            set;
        }
    }

    public class AircraftModel1RawDataLite_T6 : AircraftModel1RawDataLite
    {
        public float T6L { get; set; }

        public float T6R { get; set; }
    }

    public class AircraftModel1RawDataLite_NH : AircraftModel1RawDataLite
    {
        public float NHL { get; set; }

        public float NHR { get; set; }
    }
}
