using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    /// <summary>
    /// 机号。某个机型的某一架飞机
    /// </summary>
    [DataContract]
    public class AircraftInstance
    {
        public ObjectId Id
        {
            get;
            set;
        }

        /// <summary>
        /// 机型
        /// </summary>
        [DataMember]
        public AircraftModel AircraftModel
        {
            get;
            set;
        }

        /// <summary>
        /// 机号
        /// </summary>
        [DataMember]
        public string AircraftNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 上次使用时间
        /// </summary>
        [DataMember]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LastUsed
        {
            get;
            set;
        }
    }
}
