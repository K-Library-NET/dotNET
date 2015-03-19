using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    /// <summary>
    /// 读取头信息
    /// </summary>
    public class FlightDataHeader
    {
        public int FlightSeconds
        {
            get;
            set;
        }

        public DateTime FlightDate
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 纬度值（用于经纬度地图）
        /// </summary>
        public float[] Latitudes
        {
            get;
            set;
        }

        /// <summary>
        /// 经度值（用于经纬度地图）
        /// </summary>
        public float[] Longitudes
        {
            get;
            set;
        }
    }
}
