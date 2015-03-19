using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class ModelRawData
    {
        public string[] Fields
        {
            get;
            set;
        }


        public virtual float Second { get; set; }
    }
}
