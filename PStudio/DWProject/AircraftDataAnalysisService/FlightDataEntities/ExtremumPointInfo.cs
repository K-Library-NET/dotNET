using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    [DataContract]
    public class ExtremumPointInfo
    {
        public MongoDB.Bson.ObjectId Id
        {
            get;
            set;
        }

        /// <summary>
        /// 参数ID
        /// </summary>
        [DataMember]
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 架次ID
        /// </summary>
        [DataMember]
        public string FlightID
        {
            get;
            set;
        }

        [DataMember]
        public string AircraftNumber
        {
            get;
            set;
        }

        [DataMember]
        public DateTime FlightDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        [DataMember]
        public float MaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        [DataMember]
        public float MinValue
        {
            get;
            set;
        }

        /// <summary>
        /// 最大值出现的秒值
        /// </summary>
        [DataMember]
        public float MaxValueSecond
        {
            get;
            set;
        }

        /// <summary>
        /// 最小值出现的秒值
        /// </summary>
        [DataMember]
        public float MinValueSecond
        {
            get;
            set;
        }
    }
}
