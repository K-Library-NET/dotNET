using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    /// <summary>
    /// 这种原始点先不要做任何精简，一秒钟有几个就生成几个值
    /// </summary>
    public class ParameterRawData
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 一秒之内的值
        /// </summary>
        public float[] Values
        {
            get;
            set;
        }

        /// <summary>
        /// 时间
        /// </summary>
        public int Second
        {
            get;
            set;
        }
    }
}
