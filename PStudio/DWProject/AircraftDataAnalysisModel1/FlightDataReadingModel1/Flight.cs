using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class Flight
    {
        /// <summary>
        /// 以架次数据源名称：例如110624_01_0004.phy作为ID，
        /// 默认人为保证架次按照不重复的编码
        /// </summary>
        public string FlightID
        {
            get;
            set;
        }

        /// <summary>
        /// 架次名称，默认使用：110624_01_0004.phy，也可自定义
        /// </summary>
        public string FlightName
        {
            get;
            set;
        }

        /// <summary>
        /// 飞机实体（机号，代表同机型下的哪一架飞机）
        /// </summary>
        public AircraftInstance Aircraft
        {
            get;
            set;
        }

        public int StartSecond { get; set; }

        public int EndSecond { get; set; }

        public GlobeData[] GlobeDatas
        {
            get;
            set;
        }

        public DateTime FlightDate
        {
            get;
            set;
        }

        public DateTime LastUsed
        {
            get;
            set;
        }
    }
}
