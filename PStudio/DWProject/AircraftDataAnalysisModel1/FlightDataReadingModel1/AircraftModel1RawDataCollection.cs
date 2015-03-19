using FlightDataReading.AircraftModel1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReadingModel1.AircraftModel1
{
    public class AircraftModel1RawDataCollection : ObservableCollection<AircraftModel1RawData>
    {
        public AircraftModel1RawDataCollection(IEnumerable<AircraftModel1RawData> rawdatas)
            : base(rawdatas)
        {
        }

        public AircraftModel1RawDataCollection()
            : base()
        {
        }

        public AircraftModel1RawDataCollection(IEnumerable<FlightDataEntitiesRT.ParameterRawData> dts)
            : base(AircraftModel1RawDataCollection.ToModel1RawData(dts))
        {

        }

        private static IEnumerable<AircraftModel1RawData> ToModel1RawData(
            IEnumerable<FlightDataEntitiesRT.ParameterRawData> dts)
        {
            if (dts != null && dts.Count() > 0)
            {
                var result = from one in dts
                             group one by one.Second into gp
                             select gp;

                var gpResult = from g in result
                               select ToModel1RawData(g.Key, result);

                return gpResult;
                //select ToModel1RawData(one);

                //var rs = ToResultSet(gpResult);

                // return rs;
            }

            return new AircraftModel1RawData[] { };
        }

        private static AircraftModel1RawData ToModel1RawData(int second,
            IEnumerable<IGrouping<int, FlightDataEntitiesRT.ParameterRawData>> result)
        {
            AircraftModel1RawData oneSecondData = new AircraftModel1RawData() { Second = second };
            foreach (var one in result)
            {
                AircraftModel1RawDataBuilder.AssignValueSimple(oneSecondData, one);
            }
            return oneSecondData;
        }

        private static IEnumerable<AircraftModel1RawData> ToModel1RawData(FlightDataEntitiesRT.ParameterRawData one)
        {
            List<AircraftModel1RawData> dts = new List<AircraftModel1RawData>();

            for (int i = 0; i < one.Values.Length; i++)
            {
                var model1RawData = new AircraftModel1RawData() { Second = one.Second };
                AircraftModel1RawDataBuilder.AssignValueSimple(model1RawData, one, i);
                dts.Add(model1RawData);
            }

            return dts;
        }

        //private static IEnumerable<AircraftModel1RawData> ToResultSet(IEnumerable<AircraftModel1RawData> result)
        //{
        //    List<AircraftModel1RawData> list = new List<AircraftModel1RawData>();

        //    foreach (var rs in result)
        //    {
        //        list.AddRange(rs);
        //    }

        //    return list;
        //}
    }
}
