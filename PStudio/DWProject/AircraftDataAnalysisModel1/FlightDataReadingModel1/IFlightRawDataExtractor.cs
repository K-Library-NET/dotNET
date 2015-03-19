using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public interface IFlightRawDataExtractor
    {
        FlightDataHeader GetHeader();

        void Close();

        ParameterRawData[] GetDataBySecond(int second);

        Decisions.Decision[] GetFaultDecisions();
    }
}
