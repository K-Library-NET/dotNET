using FlightDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AircraftDataAnalysisWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class DataInputService : IAircraftDataInput
    {
        public FlightDataEntities.Flight AddOrReplaceFlight(FlightDataEntities.Flight flight)
        {
            try
            {
                LogHelper.Info("DataInputService.AddOrReplaceFlight Requested.", null);
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddOrReplaceFlight(flight);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddOrReplaceFlight", ex);
                return null;
            }
        }

        public string DeleteExistsData(FlightDataEntities.Flight flight)
        {
            try
            {
                LogHelper.Info("DataInputService.DeleteExistsData Requested.", null);
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.DeleteExistsData(flight);
            }
            catch (Exception ex)
            {
                LogHelper.Error("DeleteExistsData", ex);
                return ex.Message;
            }
        }

        public string DeleteFlight(FlightDataEntities.Flight flight)
        {
            try
            {
                LogHelper.Info("DataInputService.DeleteFlight Requested.", null);
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.DeleteFlight(flight);
            }
            catch (Exception ex)
            {
                LogHelper.Error("DeleteFlight", ex);
                return ex.Message;
            } 
        }

        public string AddDecisionRecordsBatch(FlightDataEntities.Flight flight, FlightDataEntities.Decisions.DecisionRecord[] records)
        {
            try
            {
                LogHelper.Info("DataInputService.AddDecisionRecordsBatch Requested.", null);
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddDecisionRecordsBatch(flight, records);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddDecisionRecordsBatch", ex);
                return ex.Message;
            }
        }

        public string AddOneParameterValue(FlightDataEntities.Flight flight, string parameterID,
            FlightDataEntities.Level1FlightRecord[] reducedRecords)
        {
            try
            {
                LogHelper.Info("DataInputService.AddOneParameterValue Requested.", null);
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddOneParameterValue(flight, parameterID, reducedRecords);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddOneParameterValue", ex);
                return ex.Message;
            }
        }

        public string AddLevelTopFlightRecords(FlightDataEntities.Flight flight,
            FlightDataEntities.LevelTopFlightRecord[] topRecords)
        {
            try
            {
                LogHelper.Info("DataInputService.AddLevelTopFlightRecords Requested.", null);
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddLevelTopFlightRecords(flight, topRecords);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddLevelTopFlightRecords", ex);
                return ex.Message;
            }
        }


        public string AddFlightRawDataRelationPoints(Flight flight, List<FlightRawDataRelationPoint> flightRawDataRelationPoints)
        {
            try
            {
                LogHelper.Info("DataInputService.AddOrReplaceFlightRawDataRelationPoints Requested.", null);
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddFlightRawDataRelationPoints(flight, flightRawDataRelationPoints);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddOrReplaceFlightRawDataRelationPoints", ex);
                return ex.Message;
            }
        }

        public string AddOrReplaceFlightExtreme(Flight flight, ExtremumPointInfo[] extremumPointInfo)
        {
            try
            {
                LogHelper.Info("DataInputService.AddOrReplaceFlightExtreme Requested.", null);
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddOrReplaceFlightExtreme(flight, extremumPointInfo);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddOrReplaceFlightExtreme", ex);
                return ex.Message;
            }
        }


        public string AddFlightConditionDecisionRecordsBatch(Flight flight, FlightDataEntities.Decisions.DecisionRecord[] records)
        {
            try
            {
                LogHelper.Info("DataInputService.AddFlightConditionDecisionRecordsBatch Requested.", null);
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddFlightConditionDecisionRecordsBatch(flight, records);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddFlightConditionDecisionRecordsBatch", ex);
                return ex.Message;
            }
        }

        public string AddOrReplaceFlightGlobeDataBatch(string flightId, AircraftModel model,
            int startIndex, int endIndex, FlightDataEntities.GlobeData[] globedatas)
        {
            try
            {
                LogHelper.Info("DataInputService.AddOrReplaceFlightGlobeDataBatch Requested.", null);
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddOrReplaceFlightGlobeDataBatch(flightId, model, startIndex, endIndex, globedatas);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddOrReplaceFlightGlobeDataBatch", ex);
                return ex.Message;
            }
        }

        public string AddOrReplaceAircraftInstance(AircraftInstance instance)
        {
            try
            {
                LogHelper.Info("DataInputService.AddOrReplaceAircraftInstance Requested.", null);
                if (instance == null || instance.AircraftModel == null
                    || string.IsNullOrEmpty(instance.AircraftModel.ModelName)
                    || string.IsNullOrEmpty(instance.AircraftNumber))
                {
                    return "AddOrReplaceAircraftInstance: 参数为空。";
                }

                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddOrReplaceAircraftInstance(instance);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddOrReplaceAircraftInstance", ex);
                return ex.Message;
            }
        }

        public string DeleteAircraftInstance(AircraftInstance instance)
        {
            try
            {
                LogHelper.Info("DataInputService.DeleteAircraftInstance Requested.", null);
                if (instance == null || instance.AircraftModel == null
                    || string.IsNullOrEmpty(instance.AircraftModel.ModelName)
                    || string.IsNullOrEmpty(instance.AircraftNumber))
                {
                    return "DeleteAircraftInstance: 参数为空。";
                }

                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.DeleteAircraftInstance(instance);
            }
            catch (Exception ex)
            {
                LogHelper.Error("DeleteAircraftInstance", ex);
                return ex.Message;
            }
        }
    }
}
