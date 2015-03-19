using FlightDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AircraftDataAnalysisWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IAircraftService”。
    [ServiceContract]
    public interface IAircraftService
    {
        /// <summary>
        /// 取得当前服务端配置的机型信息（对客户端来说基本算写死的常量）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        AircraftModel GetCurrentAircraftModel();

        /// <summary>
        /// 取得此机型的所有架次
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Flight[] GetAllFlights(AircraftModel model);

        /// <summary>
        /// 取得此机型的所有架次
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Flight[] GetAllFlightsByInstance(AircraftModel model, AircraftInstance aircraftInstance);

        /// <summary>
        /// 取得明细数据
        /// </summary>
        /// <param name="flight">架次</param>
        /// <param name="parameterIds">参数ID</param>
        /// <param name="startSecond">起始秒（0秒钟开始）</param>
        /// <param name="endSecond">结束秒</param>
        /// <returns></returns>
        [OperationContract]
        KeyValuePair<string, FlightDataEntities.FlightRawData[]>[] GetFlightData(Flight flight,
            string[] parameterIds, int startSecond, int endSecond);

        //直接通过LevelTop来取得
        ///// <summary>
        ///// 取得该架次的极值数据
        ///// </summary>
        ///// <param name="flight"></param>
        ///// <returns></returns>
        //[OperationContract]
        //FlightDataEntities.ExtremumPointInfo[] GetExtremumPointInfos(Flight flight);

        /// <summary>
        /// 通过机型，取得所有曲线面板
        /// </summary>
        /// <param name="aircraftModel">机型</param>
        /// <returns></returns>
        [OperationContract]
        FlightDataEntities.Charts.ChartPanel[] GetAllChartPanels(AircraftModel aircraftModel);

        /// <summary>
        /// 取得架次（通过GetCurrentAircraftModel可以获得）对应的参数列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        FlightParameters GetAllFlightParameters(AircraftModel aircraftModel);

        /// <summary>
        /// 取得架次（通过GetCurrentAircraftModel可以获得）对应的所有判据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        FlightDataEntities.Decisions.Decision[] GetAllDecisions(AircraftModel aircraftModel);

        /// <summary>
        /// 取得最顶层数据
        /// </summary>
        /// <param name="flight">架次</param>
        /// <param name="parameterIds">参数ID列表，如果传入空或者空数组，则返回该架次的全部</param>
        /// <param name="withLevel1Data">是否包含最底层数据，如果包含，数据量可能很大</param>
        /// <returns></returns>
        [OperationContract]
        FlightDataEntities.LevelTopFlightRecord[] GetLevelTopFlightRecords(Flight flight,
            string[] parameterIds);

        /// <summary>
        /// 取得一个架次的所有判定成功记录
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        [OperationContract]
        FlightDataEntities.Decisions.DecisionRecord[] GetDecisionRecords(Flight flight);

        [OperationContract]
        ExtremumReportDefinition GetExtremumReportDefinition(string aircraftModelName);

        [OperationContract]
        FlightDataEntities.Decisions.FlightConditionDecision[]
            GetAllFlightConditionDecisions(FlightDataEntities.AircraftModel aircraft);

        /// <summary>
        /// 只能一个一个flight取，避免数据过多
        /// </summary>
        /// <param name="flightID"></param>
        /// <returns></returns>
        [OperationContract]
        FlightDataEntities.FlightRawDataRelationPoint[]
            GetFlightRawDataRelationPoints(FlightDataEntities.AircraftModel aircraft, string flightID,
            string XAxisParameterID, string YAxisParameterID);

        [OperationContract]
        FlightDataEntities.ExtremumPointInfo[] GetExtremumPointInfosByAircraftInstance(AircraftInstance aircraftInstance);

        [OperationContract]
        GlobeData[] GetGlobeDatas(string flightID, AircraftModel model, int startIndex, int endIndex);

        [OperationContract]
        AircraftInstance[] GetAllAircrafts(AircraftModel model);

        [OperationContract]
        int GetEarliestYear(AircraftModel model);

        [OperationContract]
        FlightDataEntities.Decisions.DecisionRecord[] GetFlightConditionDecisionRecords(AircraftModel model,
             DateTime startYearMonth, DateTime endYearMonth, string[] aircraftNumbers);

        [OperationContract]
        string GetAppConfigValue(string appKey);
    }
}
