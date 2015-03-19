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
    /// 飞行参数源数据，未经处理
    /// </summary>
    [DataContract]
    public class FlightRawData
    {
        public ObjectId Id
        {
            get;
            set;
        }

        [DataMember]
        public float[] Values
        {
            get;
            set;
        }

        /// <summary>
        /// 当前飞行秒（第几秒）
        /// </summary>
        [DataMember]
        public int Second
        {
            get;
            set;
        }

        [DataMember]
        public string ParameterID
        {
            get;
            set;
        }
    }
}
