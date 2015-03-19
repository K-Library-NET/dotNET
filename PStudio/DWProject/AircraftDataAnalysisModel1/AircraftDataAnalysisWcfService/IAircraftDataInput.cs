using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AircraftDataAnalysisWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IAircraftDataInput
    {
        [OperationContract]
        FlightDataEntities.Flight AddOrReplaceFlight(
            FlightDataEntities.Flight flight);

        /// <summary>
        /// 删除已有数据，包括RawData、极值、判据
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        [OperationContract]
        string DeleteExistsData(
            FlightDataEntities.Flight flight);

        [OperationContract]
        string DeleteFlight(
            FlightDataEntities.Flight flight);

        /// <summary>
        /// 写入判据记录（批量）
        /// 没法单个UpdateOrAdd，只能之前先删掉
        /// </summary>
        /// <param name="?"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        [OperationContract]
        string AddDecisionRecordsBatch(FlightDataEntities.Flight flight,
            FlightDataEntities.Decisions.DecisionRecord[] records);

        [OperationContract]
        string AddOneParameterValue(FlightDataEntities.Flight flight,
             string parameterID, FlightDataEntities.Level1FlightRecord[] reducedRecords);

        [OperationContract]
        string AddLevelTopFlightRecords(FlightDataEntities.Flight flight,
            FlightDataEntities.LevelTopFlightRecord[] topRecords);

        [OperationContract]
        string AddFlightRawDataRelationPoints(FlightDataEntities.Flight flight,
            List<FlightDataEntities.FlightRawDataRelationPoint> flightRawDataRelationPoints);

        [OperationContract]
        string AddOrReplaceFlightExtreme(FlightDataEntities.Flight flight,
            FlightDataEntities.ExtremumPointInfo[] extremumPointInfo);


        [OperationContract]
        string AddFlightConditionDecisionRecordsBatch(FlightDataEntities.Flight flight,
            FlightDataEntities.Decisions.DecisionRecord[] records);

        [OperationContract]
        string AddOrReplaceFlightGlobeDataBatch(string flightId, FlightDataEntities.AircraftModel model,
            int startIndex, int endIndex, FlightDataEntities.GlobeData[] globedatas);

        [OperationContract]
        string AddOrReplaceAircraftInstance(FlightDataEntities.AircraftInstance instance);

        [OperationContract]
        string DeleteAircraftInstance(FlightDataEntities.AircraftInstance instance);
    }
}
