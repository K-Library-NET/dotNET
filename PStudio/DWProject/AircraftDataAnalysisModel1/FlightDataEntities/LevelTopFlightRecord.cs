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
    /// 最顶层的数据，可以用于极值报表
    /// </summary>
    [DataContract]
    public class LevelTopFlightRecord
    {
        /// <summary>
        /// 记录ID，用作关联、定位
        /// </summary>
        public ObjectId Id
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

        /// <summary>
        /// 飞行参数ID，用作关联
        /// </summary>
        [DataMember]
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 起始秒数，从开始飞行的时间开始算
        /// </summary>
        [DataMember]
        public int StartSecond
        {
            get;
            set;
        }

        /// <summary>
        /// 结束秒数，从开始飞行的时间算
        /// </summary>
        [DataMember]
        public int EndSecond
        {
            get;
            set;
        }

        ///// <summary>
        ///// 整个段中的平均值（精简前）
        ///// </summary>
        //[DataMember]
        //public float AvgValue
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 整个段中的最小值（精简前）
        ///// </summary>
        //[DataMember]
        //public float MinValue
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 最小值（第一次）出现的时间（飞行秒）
        ///// </summary>
        //[DataMember]
        //public int MinValueSecond
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 最大值（第一次）出现的时间（飞行秒）
        ///// </summary>
        //[DataMember]
        //public int MaxValueSecond
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 整个段中的最大值（精简前）
        ///// </summary>
        //[DataMember]
        //public float MaxValue
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 整个段中的记录点个数（精简前）
        ///// 必须保留Count值，因为用于计算出SUM
        ///// SUM = count * AvgValue
        ///// </summary>
        //[DataMember]
        //public int Count
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 第一层飞行记录数据，经过一定处理，比如每秒钟只保留一个点
        /// </summary>
        [DataMember]
        public Level2FlightRecord[] Level2FlightRecord
        {
            get;
            set;
        }

        /// <summary>
        /// 极值点，用于极值报表
        /// </summary>
        [DataMember]
        public ExtremumPointInfo ExtremumPointInfo
        {
            get;
            set;
        }
    }
}
