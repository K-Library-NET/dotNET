using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public abstract class ModelRawDataBuilder : FlightDataEntitiesRT.IModelRawDataBuilder
    {
        public abstract ModelRawData CreateModelRawDataObj();

        public abstract void AssignValue(ModelRawData data, ParameterRawData two);
    }
}
