using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class FlightParameters
    {
        public FlightParameter[] Parameters
        {
            get;
            set;
        }

        public int BytesCount { get; set; }

        public string[] ToParameterIDs()
        {
            if (Parameters != null && Parameters.Length > 0)
            {
                var result = from one in this.Parameters
                             where one.ParameterID != "(NULL)" 
                             select one.ParameterID;
                return result.ToArray();
            }

            return new string[] { };
        }
    }

    public class ByteIndex
    {
        public int Index
        {
            get;
            set;
        }

        public BitIndex[] SubIndexes
        {
            get;
            set;
        }
    }

    public class BitIndex
    {
        public int SubIndex
        {
            get;
            set;
        }
    }
}
