using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    /// <summary>
    /// 经纬度数据
    /// </summary>
    [DataContract]
    public class GlobeData
    {
        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public float Latitude
        {
            get;
            set;
        }

        [DataMember]
        /// <summary>
        /// 经度
        /// </summary>
        public float Longitude
        {
            get;
            set;
        }

        [DataMember]
        public int Index
        {
            get;
            set;
        }

        [DataMember]
        public string FlightID
        {
            get;
            set;
        }

        [DataMember]
        public string AircraftModelName
        {
            get;
            set;
        }

        public ObjectId Id
        {
            get;
            set;
        }

        public static readonly float MIN_LATITUDE = 0;
        public static readonly float MAX_LATITUDE = 90;
        public static readonly float MIN_LONGITUDE = 0;
        public static readonly float MAX_LONGITUDE = 180;
    }
}
