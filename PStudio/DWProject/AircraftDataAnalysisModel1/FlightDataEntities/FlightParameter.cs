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
    /// 飞行参数
    /// </summary>
    [DataContract]
    public class FlightParameter
    {
        public ObjectId Id
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
        /// 标题（中文展示名）
        /// </summary>
        [DataMember]
        public string Caption
        {
            get;
            set;
        }

        #region properties

        ///// <summary>
        ///// 是否默认关注的参数，如果为True则默认先展示
        ///// </summary>
        //[DataMember]
        //public bool IsConcerned { get; set; }

        /// <summary>
        /// 机型编号
        /// </summary>
        [DataMember]
        public string ModelName { get; set; }

        /// <summary>
        /// 顺序（？）
        /// </summary>
        [DataMember]
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// 子顺序（？）
        /// </summary>
        [DataMember]
        public int SubIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 单位
        /// </summary>
        [DataMember]
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// 采样频率（？）
        /// </summary>
        [DataMember]
        public int Frequence
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 数据类型
        /// </summary>
        [DataMember]
        public string ParameterDataType { get; set; }

        [DataMember]
        public ByteIndex[] ByteIndexes
        {
            get;
            set;
        }
    }

    [DataContract]
    public class ByteIndex
    {
        [DataMember]
        public int Index
        {
            get;
            set;
        }

        [DataMember]
        public BitIndex[] SubIndexes
        {
            get;
            set;
        }
    }

    [DataContract]
    public class BitIndex
    {
        [DataMember]
        public int SubIndex
        {
            get;
            set;
        }
    }

    [DataContract]
    public class FlightParameters
    {
        [DataMember]
        public FlightParameter[] Parameters
        {
            get;
            set;
        }

        [DataMember]
        public int BytesCount { get; set; }
    }
}
