using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class Level2FlightRecord
    {
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

        public string FlightID
        {
            get;
            set;
        }

        //不要持有Level1的记录了，实在对象太大了存不进去MongoDB
        //public Level1FlightRecord[] Values
        //{
        //    get;
        //    set;
        //}

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
