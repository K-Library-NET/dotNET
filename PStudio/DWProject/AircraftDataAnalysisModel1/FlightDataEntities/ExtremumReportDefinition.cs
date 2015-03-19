using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    /// <summary>
    /// 极值报表定义
    /// </summary>
    [DataContract]
    public class ExtremumReportDefinition
    {
        [DataMember]
        public ExtremumReportItemDefinition[] Items
        {
            get;
            set;
        }

        /// <summary>
        /// 机型
        /// </summary>
        [DataMember]
        public string AircraftModelName
        {
            get;
            set;
        }
    }
}
