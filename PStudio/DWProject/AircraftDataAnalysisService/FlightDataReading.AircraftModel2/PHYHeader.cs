using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    /// <summary>
    /// PHY文件基本信息
    /// </summary>
    public class PHYHeader
    {
        public string OPName { get; set; }
        public DateTime BTime { get; set; }
        public int FlyPlanAddr { get; set; }
        public int ParaListAddr { get; set; }
        //飞参文件开始段有效数据起始地址
        public int PhyValueAddr { get; set; }
        //飞参文件尾部无效数据起始地址
        public int PhyValueEndAddr { get; set; }
        public string AircrfName { get; set; }
        public string AircrfNum { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PNum { get; set; }
        public int SWNum { get; set; }

        #region 额外信息
        /// <summary>
        /// GPS开始时间，毫秒
        /// </summary>
        public int GPSStartTime { get; set; }
        /// <summary>
        /// GPS结束时间，毫秒
        /// </summary>
        public int GPSEndTime { get; set; }
        public int FlySeconds { get; set; }
        #endregion
    }
}
