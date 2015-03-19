using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    /// <summary>
    /// 机号。某个机型的某一架飞机
    /// </summary>
    [DataContract]
    public class AircraftInstance
    {
        /// <summary>
        /// 机型
        /// </summary>
        public AircraftModel AircraftModel
        {
            get;
            set;
        }

        /// <summary>
        /// 机号
        /// </summary>
        public string AircraftNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 上次使用时间
        /// </summary>
        public DateTime LastUsed
        {
            get;
            set;
        }
    }
}
