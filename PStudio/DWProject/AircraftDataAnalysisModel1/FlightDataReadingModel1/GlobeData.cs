using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{

    /// <summary>
    /// 经纬度数据
    /// </summary>
    public class GlobeData
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public float Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// 经度
        /// </summary>
        public float Longitude
        {
            get;
            set;
        }

        public static readonly float MIN_LATITUDE = 0;
        public static readonly float MAX_LATITUDE = 90;
        public static readonly float MIN_LONGITUDE = 0;
        public static readonly float MAX_LONGITUDE = 180;
    }
}
