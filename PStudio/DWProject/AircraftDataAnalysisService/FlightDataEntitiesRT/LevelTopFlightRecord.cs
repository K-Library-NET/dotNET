using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    /// <summary>
    /// 最顶层
    /// </summary>
    public class LevelTopFlightRecord
    {
        public string FlightID
        {
            get;
            set;
        }

        public string ParameterID
        {
            get;
            set;
        }

        public int StartSecond
        {
            get;
            set;
        }

        public int EndSecond
        {
            get;
            set;
        }

        public Level2FlightRecord[] Level2FlightRecord
        {
            get;
            set;
        }

        /// <summary>
        /// 极值点，用于极值报表
        /// </summary>
        public ExtremumPointInfo ExtremumPointInfo
        {
            get;
            set;
        }
    }
}
