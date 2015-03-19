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
    /// 极值报表单个记录定义
    /// </summary>
    [DataContract]
    public class ExtremumReportItemDefinition
    {
        /// <summary>
        /// 序号
        /// </summary>
        [DataMember]
        public int Number
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
    }
}
