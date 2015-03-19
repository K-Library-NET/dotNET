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
    /// 最底层的数据，直接到数据点
    /// </summary>
    [DataContract]
    public class Level1FlightRecord
    {
        public ObjectId Id
        {
            get;
            set;
        }

        /// <summary>
        /// 架次ID，必须，用于批量删除
        /// </summary>
        [DataMember]
        public string FlightID
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行参数ID
        /// </summary>
        [DataMember]
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 当前秒内的采集参数
        /// </summary>
        [DataMember]
        public float[] Values
        {
            get;
            set;
        }

        [DataMember]
        public int StartSecond
        {
            get;
            set;
        }

        [DataMember]
        public int EndSecond
        {
            get;
            set;
        }

        public IEnumerable<FlightRawData> ToRawDatas()
        {
            if (this.StartSecond < this.EndSecond && this.Values != null && this.Values.Length > 0)
            {
                List<FlightRawData> dts = new List<FlightRawData>();
                int factor = this.Values.Length / (this.EndSecond - this.StartSecond);
                
                for (int i = this.StartSecond; i < this.EndSecond; i++)
                {
                    FlightRawData rd = new FlightRawData() { ParameterID = this.ParameterID, Second = i };
                    var values = this.Values.Skip((i - this.StartSecond) * factor).Take(factor);
                    rd.Values = values.ToArray();
                    dts.Add(rd);
                }

                return dts;
            }
            else
            {
                FlightRawData dt = new FlightRawData()
                {
                    ParameterID = this.ParameterID,
                    Second = this.StartSecond,
                    Values = this.Values
                };
                return new FlightRawData[] { dt };
            }
        }
    }
}
