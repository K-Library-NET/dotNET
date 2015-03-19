using FlightDataEntities;
using FlightDataReading.DataPointTransforms;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading
{
    public class FlightDataEntityTransform
    {
        public static FlightRawData FromLevel1FlightRecordToFlightRawData(Level1FlightRecord record)
        {
            IFlightDataEntityTransformStrategy strategy = GetStrategy();
            return strategy.FromLevel1FlightRecordToFlightRawData(record);
        }

        private static IFlightDataEntityTransformStrategy GetStrategy()
        {
            return null;
            //return new DefaultFlightDataEntityTransformStrategy();
        }

        public static Level1FlightRecord FromFlightRawDataToLevel1FlightRecord(FlightRawData entity)
        {
            IFlightDataEntityTransformStrategy strategy = GetStrategy();
            return strategy.FromFlightRecordEntityToLevel1FlightRecord(entity);
        }

        /// <summary>
        /// 精简数据，返回最顶一层的数据
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static LevelTopFlightRecord[] FromLevel1RecordCollectionToLevelTopRecords(
            Flight flight, MongoCollection<Level1FlightRecord> collection)
        {
            return null;//DEBUG
        }
    }
}
