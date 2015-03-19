using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    /// <summary>
    /// 飞行参数
    /// </summary>
    public class FlightParameter
    {
        /// <summary>
        /// 飞行参数ID
        /// </summary>
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 标题（中文展示名）
        /// </summary>
        public string Caption
        {
            get;
            set;
        }

        #region properties

        /// <summary>
        /// 机型编号
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 顺序（？）
        /// </summary>
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// 子顺序（？）
        /// </summary>
        public int SubIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 采样频率（？）
        /// </summary>
        public int Frequence
        {
            get;
            set;
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 数据类型
        /// </summary>
        public string ParameterDataType { get; set; }

        public ByteIndex[] ByteIndexes
        {
            get;
            set;
        }
    }
}
