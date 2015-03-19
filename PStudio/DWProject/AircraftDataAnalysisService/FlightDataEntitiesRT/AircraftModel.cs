using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    /// <summary>
    /// 机型。作为数据的最顶级元素，NoSQL数据库的DB名称
    /// </summary>
    [DataContract]
    public class AircraftModel
    {
        /// <summary>
        /// 机型编号
        /// </summary>
        public string ModelName
        {
            get;
            set;
        }

        /// <summary>
        /// 机型中文显示
        /// </summary>
        public string Caption
        {
            get;
            set;
        }

        #region properties
        //机型各个属性

        /// <summary>
        /// 上次使用时间
        /// </summary> 
        public DateTime LastUsed
        {
            get;
            set;
        }

        #endregion
    }
}
