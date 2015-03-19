using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
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

        /// <summary>
        /// 写死只要第一个值
        /// </summary>
        public float SummaryValue
        {
            get
            {
                if (Values != null && Values.Length > 0)
                    return Values[0];
                return 0;
            }
        }
    }
}
