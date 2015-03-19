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
    /// 架次。某一架飞机的一次飞行记录
    /// </summary>
    [DataContract]
    public class Flight
    {
        public ObjectId Id
        {
            get;
            set;
        }

        /// <summary>
        /// 以架次数据源名称：例如110624_01_0004.phy作为ID，
        /// 默认人为保证架次按照不重复的编码
        /// </summary>
        [DataMember]
        public string FlightID
        {
            get;
            set;
        }

        /// <summary>
        /// 架次名称，默认使用：110624_01_0004.phy，也可自定义
        /// </summary>
        [DataMember]
        public string FlightName
        {
            get;
            set;
        }

        /// <summary>
        /// 飞机实体（机号，代表同机型下的哪一架飞机）
        /// </summary>
        [DataMember]
        public AircraftInstance Aircraft
        {
            get;
            set;
        }

        [DataMember]
        public DateTime FlightDate
        {
            get;
            set;
        }

        [DataMember]
        public int StartSecond { get; set; }

        [DataMember]
        public int EndSecond { get; set; }

        [DataMember]
        public GlobeData[] GlobeDatas { get; set; }
    }
}
