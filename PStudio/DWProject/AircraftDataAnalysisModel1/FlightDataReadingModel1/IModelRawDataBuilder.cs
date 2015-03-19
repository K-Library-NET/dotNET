using System;
namespace FlightDataEntitiesRT
{
    public interface IModelRawDataBuilder
    {
        void AssignValue(ModelRawData data, ParameterRawData two);
        ModelRawData CreateModelRawDataObj();
    }
}
