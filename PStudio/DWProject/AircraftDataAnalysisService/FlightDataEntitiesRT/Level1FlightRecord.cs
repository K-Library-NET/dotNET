using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class Level1FlightRecord
    {
        /// <summary>
        /// 架次ID，必须，用于批量删除
        /// </summary>
        public string FlightID
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行参数ID
        /// </summary>
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

        public float[] Values
        {
            get;
            set;
        }
    }
}
