using FlightDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Configuration;
using System.Xml.Linq;
using System.IO;
using AircraftDataAnalysisWcfService.DALs;
using FlightDataEntities.Charts;
using FlightDataEntities.Decisions;

namespace AircraftDataAnalysisWcfService
{
    public class AircraftServiceBll
    {
        private static AircraftModel m_currentAircraftModel = null;

        private void InitCurrentAircraftModel()
        {
            string modelName = ConfigurationManager.AppSettings["AircraftModelName"];
            string modelCaption = ConfigurationManager.AppSettings["AircraftModelCaption"];

            AircraftModel model = new AircraftModel() { ModelName = modelName, Caption = modelCaption, LastUsed = DateTime.Now };

            m_currentAircraftModel = model;
        }

        #region old

        //private string m_mongoConnectionString = "mongodb://localhost/?w=1";

        //[Obsolete]
        //public string[] GetAllAircraftModelNames()
        //{

        //    string[] dbnames = null;
        //    MongoServer mongoServer = this.GetMongoServer();

        //    Exception ex = null;

        //    try
        //    {
        //        mongoServer.Connect();
        //        dbnames = mongoServer.GetDatabaseNames().ToArray();
        //    }
        //    catch (Exception e)
        //    {
        //        ex = e;
        //    }
        //    finally
        //    {
        //        if (mongoServer != null)
        //            mongoServer.Disconnect();
        //    }

        //    if (ex != null)
        //        throw ex;
        //    return dbnames;
        //}

        //private MongoServer GetMongoServer()
        //{
        //    var mongoUrl = new MongoUrl(this.GetMongoConnectionString());
        //    var clientSettings = MongoClientSettings.FromUrl(mongoUrl);
        //    if (!clientSettings.WriteConcern.Enabled)
        //    {
        //        clientSettings.WriteConcern.W = 1; // ensure WriteConcern is enabled regardless of what the URL says
        //    }
        //    var mongoClient = new MongoClient(clientSettings);
        //    return mongoClient.GetServer();
        //}

        //private string GetMongoConnectionString()
        //{
        //    return this.m_mongoConnectionString;
        //}

        //[Obsolete]
        //public FlightDataEntities.AircraftModel[] GetAllAircraftModels()
        //{
        //    MongoServer mongoServer = this.GetMongoServer();
        //    if (mongoServer != null)
        //    {
        //        MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
        //        if (database != null)
        //        {
        //            MongoCollection<AircraftModel> modelCollection
        //                = database.GetCollection<AircraftModel>(AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);

        //            IQueryable<AircraftModel> models = modelCollection.AsQueryable<AircraftModel>();
        //            var result = from one in models
        //                         orderby one.LastUsed descending
        //                         select one;

        //            return result.ToArray();
        //        }
        //    }

        //    throw new Exception(string.Format(
        //        "No MongoServer {0} finded, or no MongoCollection {1} finded.",
        //        AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL));
        //}

        //[Obsolete]
        //public string AddOrUpdateAircraftModel(FlightDataEntities.AircraftModel aircraftModel)
        //{
        //    if (aircraftModel == null)
        //        return "没有机型。";

        //    MongoServer mongoServer = this.GetMongoServer();
        //    if (mongoServer != null)
        //    {
        //        MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
        //        if (database != null)
        //        {
        //            MongoCollection<AircraftModel> modelCollection
        //                = database.GetCollection<AircraftModel>(AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);

        //            IQueryable<AircraftModel> models = modelCollection.AsQueryable<AircraftModel>();
        //            var result = from one in models
        //                         where one.ModelName == aircraftModel.ModelName
        //                         select one;

        //            if (result != null && result.Count() > 0)
        //            {
        //                foreach (var oneModel in result)
        //                {//所有Property复制
        //                    oneModel.LastUsed = aircraftModel.LastUsed;
        //                    oneModel.Caption = aircraftModel.Caption;
        //                    modelCollection.Save(oneModel);
        //                }
        //            }
        //            else
        //            {
        //                modelCollection.Insert(aircraftModel);
        //            }

        //            return string.Empty;
        //        }
        //    }

        //    return string.Format(
        //        "No MongoServer {0} finded, or no MongoCollection {1} finded.",
        //        AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);
        //}

        //[Obsolete]
        //public string DeleteAircraft(string aircraftModel)
        //{
        //    if (aircraftModel == null)
        //        return "没有机型。";

        //    MongoServer mongoServer = this.GetMongoServer();
        //    if (mongoServer != null)
        //    {
        //        MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
        //        if (database != null)
        //        {
        //            MongoCollection<AircraftModel> modelCollection
        //                = database.GetCollection<AircraftModel>(AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);

        //            IQueryable<AircraftModel> models = modelCollection.AsQueryable<AircraftModel>();
        //            //var result = from one in models
        //            //             where one.ModelName == aircraftModel//aircraftModel.ModelName
        //            //             select one;
        //            MongoDB.Driver.MongoCursor<AircraftModel> cursor = modelCollection.Find(
        //                  Query.EQ("ModelName", aircraftModel));
        //            AircraftModel model = cursor.First();

        //            modelCollection.Remove(
        //                Query.EQ("ModelName", aircraftModel));

        //            return string.Empty;
        //        }
        //    }

        //    return string.Format(
        //        "No MongoServer {0} finded, or no MongoCollection {1} finded.",
        //        AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);
        //}

        //[Obsolete]
        //public string AddOrUpdateAircraftInstance(AircraftInstance aircraftInstance)
        //{
        //    if (aircraftInstance == null)
        //        return "没有机号。";

        //    MongoServer mongoServer = this.GetMongoServer();
        //    if (mongoServer != null)
        //    {
        //        MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
        //        if (database != null)
        //        {
        //            MongoCollection<AircraftInstance> modelCollection
        //                = database.GetCollection<AircraftInstance>(AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);

        //            IQueryable<AircraftInstance> models = modelCollection.AsQueryable<AircraftInstance>();
        //            var result = from one in models
        //                         where one.AircraftNumber == aircraftInstance.AircraftNumber
        //                            && one.AircraftModel.ModelName == aircraftInstance.AircraftModel.ModelName
        //                         select one;

        //            if (result != null && result.Count() > 0)
        //            {
        //                foreach (var oneModel in result)
        //                {//所有Property复制
        //                    oneModel.LastUsed = aircraftInstance.LastUsed;
        //                    oneModel.AircraftModel = aircraftInstance.AircraftModel;
        //                    modelCollection.Save(oneModel);
        //                }
        //            }
        //            else
        //            {
        //                modelCollection.Insert(aircraftInstance);
        //            }

        //            return string.Empty;
        //        }
        //    }

        //    return string.Format(
        //        "No MongoServer {0} finded, or no MongoCollection {1} finded.",
        //        AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);
        //}

        //[Obsolete]
        //public AircraftInstance[] GetAllAircraftInstances()
        //{
        //    return GetAllAircraftInstances(string.Empty);
        //}

        //[Obsolete]
        //public AircraftInstance[] GetAllAircraftInstances(string modelName)
        //{
        //    MongoServer mongoServer = this.GetMongoServer();
        //    if (mongoServer != null)
        //    {
        //        MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
        //        if (database != null)
        //        {
        //            MongoCollection<AircraftInstance> modelCollection
        //                = database.GetCollection<AircraftInstance>(
        //                AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);

        //            IQueryable<AircraftInstance> models = modelCollection.AsQueryable<AircraftInstance>();

        //            if (string.IsNullOrEmpty(modelName))
        //                return models.ToArray();

        //            var results = from one in models
        //                          where one.AircraftModel != null && one.AircraftModel.ModelName == modelName
        //                          select one;

        //            if (results != null && results.Count() > 0)
        //                return results.ToArray();

        //            return new AircraftInstance[] { };
        //        }
        //    }

        //    throw new Exception(string.Format(
        //        "No MongoServer {0} finded, or no MongoCollection {1} finded.",
        //        AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE));
        //}

        //[Obsolete]
        //public AircraftInstance GetAircraftInstance(string aircraftNumber, string modelName)
        //{
        //    MongoServer mongoServer = this.GetMongoServer();
        //    if (mongoServer != null)
        //    {
        //        MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
        //        if (database != null)
        //        {
        //            MongoCollection<AircraftInstance> modelCollection
        //                = database.GetCollection<AircraftInstance>(
        //                AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);

        //            IQueryable<AircraftInstance> models
        //                = modelCollection.AsQueryable<AircraftInstance>();

        //            if (string.IsNullOrEmpty(aircraftNumber))
        //                return models.First();

        //            if (string.IsNullOrEmpty(modelName))
        //            {
        //                var results = from one in models
        //                              where one.AircraftNumber == aircraftNumber
        //                              select one;

        //                if (results != null && results.Count() > 0)
        //                    return results.First();
        //            }
        //            else
        //            {
        //                var results = from one in models
        //                              where one.AircraftNumber == aircraftNumber
        //                              && one.AircraftModel != null && one.AircraftModel.ModelName == modelName
        //                              select one;

        //                if (results != null && results.Count() > 0)
        //                    return results.First();
        //            }

        //            return null;
        //        }
        //    }

        //    throw new Exception(string.Format(
        //        "No MongoServer {0} finded, or no MongoCollection {1} finded.",
        //        AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE));
        //}

        //[Obsolete]
        //public string AddOrUpdateFlyParameter(FlightParameter flightParameter)
        //{
        //    return this.AddOrUpdateFlyParameter(new FlightParameter[] { flightParameter });
        //}

        //[Obsolete]
        //public string AddOrUpdateFlyParameter(FlightParameter[] flightParameter)
        //{
        //    MongoServer mongoServer = this.GetMongoServer();
        //    if (mongoServer != null)
        //    {
        //        MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
        //        if (database != null)
        //        {
        //            MongoCollection<FlightParameter> modelCollection
        //                = database.GetCollection<FlightParameter>(
        //                AircraftMongoDb.COLLECTION_AIRCRAFT_PARAMETER);

        //            foreach (var fp in flightParameter)
        //            {
        //                MongoCursor<FlightParameter> pms =
        //                 modelCollection.Find(Query.And(Query.EQ("ParameterID", fp.ParameterID),
        //                    Query.EQ("ModelName", fp.ModelName)));
        //                if (pms != null && pms.Count() > 0)
        //                {
        //                    foreach (var pm in pms)
        //                    {
        //                        pm.IsConcerned = fp.IsConcerned;
        //                        pm.Caption = fp.Caption;
        //                        pm.Index = fp.Index;
        //                        pm.SubIndex = fp.SubIndex;
        //                        pm.Unit = fp.Unit;
        //                    }
        //                }
        //                else
        //                {
        //                    modelCollection.Insert(fp);
        //                }
        //            }

        //            return string.Empty;
        //        }
        //    }

        //    return string.Format(
        //        "No MongoServer {0} finded, or no MongoCollection {1} finded.",
        //        AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_PARAMETER);
        //}


        //private bool IsValidAircraftInfo(Flight flight)
        //{
        //    if (flight == null)
        //        return false;

        //    if (flight == null || string.IsNullOrEmpty(flight.FlightID)
        //        || flight.Aircraft == null || flight.Aircraft.AircraftModel == null ||
        //        string.IsNullOrEmpty(flight.Aircraft.AircraftModel.ModelName))
        //        return false;

        //    return true;
        //}

        //[Obsolete]
        //public string OptimizeForLevel2Data(Flight flight)
        //{
        //    if (!this.IsValidAircraftInfo(flight))
        //        return "缺少机型或架次信息。";


        //    //产生汇总数据
        //    MongoServer mongoServer = this.GetMongoServer();
        //    if (mongoServer != null)
        //    {
        //        MongoDatabase database = mongoServer.GetDatabase(flight.Aircraft.AircraftModel.ModelName);
        //        if (database != null)
        //        {
        //            MongoCollection<Level1FlightRecord> modelCollection
        //                = database.GetCollection<Level1FlightRecord>(
        //                AircraftMongoDb.COLLECTION_FLIGHT_RECORD_LEVEL1 +
        //                flight.FlightID);

        //            Level2FlightRecord[] records =
        //                null;//DEBUG
        //            //  FlightDataEntityTransform.FromLevel1RecordCollectionToLevel2Record(flight, modelCollection);
        //        }
        //    }

        //    return string.Empty;
        //}
        #endregion old

        private string CombineFromBasePath(string path)
        {
            string path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            if (File.Exists(path2) || Directory.Exists(path2))
                return path2;

            string path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            return Path.Combine(path1, path);
        }

        public bool IsValidAircraftInfo(Flight flight)
        {
            if (flight == null)
                return false;

            if (flight == null || string.IsNullOrEmpty(flight.FlightID)
                || flight.Aircraft == null || flight.Aircraft.AircraftModel == null ||
                string.IsNullOrEmpty(flight.Aircraft.AircraftModel.ModelName))
                return false;

            return true;
        }

        public FlightParameters GetAllFlightParameters(AircraftModel model)
        {
            string basePath = this.CombineFromBasePath("FlightParameters.xml");

            if (!string.IsNullOrEmpty(basePath) && File.Exists(basePath))
                using (StreamReader reader = new StreamReader(basePath))
                {
                    XElement element = XElement.Load(reader);
                    if (element != null)
                    {
                        FlightParameters root = new FlightParameters();
                        var bc = element.Attribute("BytesCount");
                        if (bc != null)
                        {
                            root.BytesCount = Convert.ToInt32(bc.Value);
                        }

                        var parameters = element.Descendants("Parameter");
                        if (parameters != null && parameters.Count() > 0)
                        {
                            try
                            {
                                List<FlightParameter> fps = new List<FlightParameter>();

                                foreach (var one in parameters)
                                {
                                    FlightParameter ps = new FlightParameter()
                                    {
                                        ParameterID = one.Attribute("ParameterID").Value,
                                        Caption = one.Attribute("Caption").Value,
                                        Index = Convert.ToInt32(one.Attribute("Index").Value),
                                        SubIndex = Convert.ToInt32(one.Attribute("SubIndex").Value),
                                        ParameterDataType = one.Attribute("ParameterDataType").Value
                                    };
                                    var eles = one.Elements("Byte");
                                    if (eles != null && eles.Count() > 0)
                                    {
                                        List<ByteIndex> bis = new List<ByteIndex>();
                                        foreach (var ele in eles)
                                        {
                                            ByteIndex bi = new ByteIndex() { Index = Convert.ToInt32(ele.Attribute("Index").Value) };

                                            var eles2 = ele.Elements("Bit");
                                            if (eles2 != null && eles2.Count() > 0)
                                            {
                                                List<BitIndex> bitIndex = new List<BitIndex>();
                                                foreach (var ele2 in eles2)
                                                {
                                                    bitIndex.Add(
                                                        new BitIndex() { SubIndex = Convert.ToInt32(ele2.Attribute("Index").Value) });
                                                }
                                                bi.SubIndexes = bitIndex.ToArray();
                                            }

                                            bis.Add(bi);
                                        }

                                        ps.ByteIndexes = bis.ToArray();
                                    }

                                    fps.Add(ps);
                                }

                                root.Parameters = fps.ToArray();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }

                        return root;
                    }
                }

            return null;

            //MongoServer mongoServer = this.GetMongoServer();
            //if (mongoServer != null)
            //{
            //    MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
            //    if (database != null)
            //    {
            //        MongoCollection<FlightParameter> modelCollection
            //            = database.GetCollection<FlightParameter>(
            //            AircraftMongoDb.COLLECTION_AIRCRAFT_PARAMETER);

            //        IQueryable<FlightParameter> models = modelCollection.AsQueryable<FlightParameter>();

            //        var results = from one in models
            //                      where one.ModelName == modelName
            //                      orderby one.Index, one.SubIndex
            //                      select one;

            //        if (results != null && results.Count() > 0)
            //            return results.ToArray();

            //        return new FlightParameter[] { };
            //    }
            //}

            //throw new Exception(string.Format(
            //    "No MongoServer {0} finded, or no MongoCollection {1} finded.",
            //    AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_PARAMETER));
        }


        /// <summary>
        /// 不急着做，TopLevel还
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="parameterIds"></param>
        /// <param name="withLevel1Data"></param>
        /// <returns></returns>
        internal LevelTopFlightRecord[] GetLevelTopFlightRecords(Flight flight, string[] parameterIds)
        {
            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，但肯定LevelTopRecord需要包含趋势分析等信息，
                    //建议不要分表，存放在Common里面
                    MongoDatabase database = dal.GetMongoDatabaseByAircraftModel(mongoServer, flight.Aircraft.AircraftModel);
                    if (database != null)
                    {
                        MongoCollection<FlightDataEntities.LevelTopFlightRecord> modelCollection1
                            = dal.GetLevelTopFlightRecordMongoCollectionByFlight(database, flight);

                        IMongoQuery q1 = null;

                        if (parameterIds == null || parameterIds.Length == 0)
                            q1 = Query.EQ("FlightID", new BsonString(flight.FlightID));
                        else
                            q1 = Query.And(Query.EQ("FlightID", new BsonString(flight.FlightID)),
                                Query.In("ParameterID", (from pm in parameterIds select new BsonString(pm))));

                        var cursor = modelCollection1.Find(q1);

                        return cursor.ToArray();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("GetLevelTopFlightRecords", e);
                    return new LevelTopFlightRecord[] { };
                }
            }
            return new LevelTopFlightRecord[] { };
        }

        internal KeyValuePair<string, FlightRawData[]>[] GetFlightData(
            Flight flight, string[] parameterIds, int startSecond, int endSecond)
        {
            if (flight == null || string.IsNullOrEmpty(flight.FlightID)
                || flight.Aircraft == null || flight.Aircraft.AircraftModel == null
                || string.IsNullOrEmpty(flight.Aircraft.AircraftModel.ModelName))
            {
                LogHelper.Warn("Flight或Aircraft信息不正确（GetFlightData）。", null);
                return null;
            }

            if (startSecond == flight.StartSecond && endSecond == flight.EndSecond)
            {//应该取最顶层数据，暂时未实现此Return

            }

            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，需要拆分存储的记录，最好在DAL里面去处理表名构建逻辑
                    MongoDatabase database = dal.GetMongoDatabaseByAircraftModel(mongoServer, flight.Aircraft.AircraftModel);
                    if (database != null)
                    {
                        var cursor = FindMongoDBCursor(flight, parameterIds, startSecond, endSecond, dal, database);
                        //var queryable = cursor.AsQueryable();
                        //转换成RawData记录
                        var result = TransformToFlightRawData(parameterIds, cursor);
                        return result.ToArray();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("GetFlightData", e);
                }
            }
            return null;
        }

        private MongoCursor<Level1FlightRecord> FindMongoDBCursor(Flight flight, string[] parameterIds,
            int startSecond, int endSecond, AircraftMongoDbDal dal, MongoDatabase database)
        {
            MongoCollection<FlightDataEntities.Level1FlightRecord> modelCollection1
                = dal.GetLevel1FlightRecordMongoCollectionByFlight(database, flight);
            List<IMongoQuery> qs = new List<IMongoQuery>();
            qs.Add(Query.EQ("FlightID", flight.FlightID));
            qs.Add(Query.Or(Query.And(
                            Query.GTE("StartSecond", startSecond),
                            Query.LT("StartSecond", endSecond)),
                    Query.And(
                        Query.GT("EndSecond", startSecond),
                        Query.LTE("EndSecond", endSecond))));
            //最后这个条件是如果一个Level1Record的起始秒、结束秒
            //介于参数起始值和结束值之间，都可以Select，
            //因为区间段基本上是定长的，所以可以这样认定
            if (parameterIds != null && parameterIds.Length > 0)
            {
                var bsonParamID = from id in parameterIds
                                  select new BsonString(id);
                qs.Add(Query.In("ParameterID", bsonParamID));//前两个条件必须：架次、时间
            }

            IMongoQuery q1 = Query.And(qs);

            //var bsonParamID = from id in parameterIds
            //                  select new BsonString(id);

            ////先取出Level1记录
            //IMongoQuery q1 = Query.And(
            //    Query.EQ("FlightID", flight.FlightID),
            //    Query.Or(Query.And(
            //                Query.GTE("StartSecond", startSecond),
            //                Query.LT("StartSecond", endSecond)),
            //        Query.And(
            //            Query.GT("EndSecond", startSecond),
            //            Query.LTE("EndSecond", endSecond))),
            //    //最后这个条件是如果一个Level1Record的起始秒、结束秒
            //    //介于参数起始值和结束值之间，都可以Select，
            //    //因为区间段基本上是定长的，所以可以这样认定
            //    Query.In("ParameterID", bsonParamID) //前两个条件必须：架次、时间
            //            );

            var cursor = modelCollection1.Find(q1);
            return cursor;
        }

        private IEnumerable<KeyValuePair<string, FlightRawData[]>> TransformToFlightRawData(string[] parameterIds,
            MongoCursor<Level1FlightRecord> cursor)
        {
            Dictionary<string, List<Level1FlightRecord>> datas = new Dictionary<string, List<Level1FlightRecord>>();
            Dictionary<string, List<FlightRawData>> resultMap = new Dictionary<string, List<FlightRawData>>();
            if (parameterIds == null || parameterIds.Length == 0)
            {
                Dictionary<string, string> paramIdtmp = new Dictionary<string, string>();
                foreach (var rec in cursor)
                {
                    if (!paramIdtmp.ContainsKey(rec.ParameterID))
                    {
                        paramIdtmp.Add(rec.ParameterID, rec.ParameterID);
                        datas.Add(rec.ParameterID, new List<Level1FlightRecord>());
                        resultMap.Add(rec.ParameterID, new List<FlightRawData>());
                    }
                    else
                    {
                        datas[rec.ParameterID].Add(rec);
                    }
                }
                parameterIds = paramIdtmp.Keys.ToArray();
            }
            else
            {
                foreach (string key in parameterIds)
                {
                    if (!datas.ContainsKey(key))
                    {
                        datas.Add(key, new List<Level1FlightRecord>());
                        resultMap.Add(key, new List<FlightRawData>());
                    }
                }
                foreach (var c in cursor)
                {
                    if (datas.ContainsKey(c.ParameterID))
                        datas[c.ParameterID].Add(c);
                }
            }

            foreach (string k in datas.Keys)
            {
                var values = datas[k];
                foreach (var v in values)
                {
                    IEnumerable<FlightRawData> rawDatas = v.ToRawDatas();
                    if (rawDatas != null && rawDatas.Count() > 0)
                        resultMap[k].AddRange(rawDatas);
                }
            }

            var result = from two in resultMap
                         select new KeyValuePair<string, FlightRawData[]>(
                             two.Key, two.Value.ToArray());
            return result;
        }

        internal FlightDataEntities.Charts.ChartPanel[] GetAllChartPanels(
            AircraftModel aircraftModel)
        {
            //DEBUG: 目前先写死

            FlightDataEntities.Charts.ChartPanel[] panels = new FlightDataEntities.Charts.ChartPanel[]{
                new ChartPanel(){ PanelID = "A", PanelName = "左发动机", 
                    ParameterIDs = new string[]{"Hp","Vi", "T6L","NHL","KG7","KG8"}},
                    //气压高度 指示空速 左发排气温度 左发高压转速 襟翼放下25°襟翼放下35°

                                    new ChartPanel(){ PanelID = "B", PanelName = "右发动机", 
                    ParameterIDs = new string[]{"Hp","Vi", "T6R","NHR","KG7","KG8"}},
                                                //气压高度 指示空速 右发排气温度 右发高压转速 襟翼放下25°襟翼放下35°

                                    new ChartPanel(){ PanelID = "C", PanelName = "发动机", 
                    ParameterIDs = new string[]{"M","T6L", "T6R","NHL","NHR","Tt"}},
                    //马赫数 左发排气温度 右发排气温度 左发高压转速 右发高压转速  大气总温

                                    new ChartPanel(){ PanelID = "D", PanelName = "大气机数据", 
                    ParameterIDs = new string[]{"Hp","Vi", "M","aT","Vy","GS"}},
                    //气压高度 指示空速 马赫数 真攻角 升降速度 地速

                                    new ChartPanel(){ PanelID = "E", PanelName = "惯导飞控", 
                    ParameterIDs = new string[]{"ZH","HG", "FY","DR","KZB","KCB"}},
                    //真航向 倾斜角 俯仰角 偏流角 纵向状态标志 横向状态标志

                                    new ChartPanel(){ PanelID = "F", PanelName = "位移", 
                    ParameterIDs = new string[]{"Dx","Dy", "Dz","ZS","CS"}},
                    //副翼角位移 方向舵角位移 平尾角位移 平尾伺服器输入 副翼伺服器输入
                    
                                    new ChartPanel(){ PanelID = "G", PanelName = "着陆姿态A", 
                    ParameterIDs = new string[]{"Hp","Vi", "HG","FY","NHL","KG5","KG7"}},
                    //气压高度 指示空速 倾斜角 俯仰角 左发高压转速 左起落架放下 襟翼放下25°

                                    new ChartPanel(){ PanelID = "H", PanelName = "着陆姿态B", 
                    ParameterIDs = new string[]{"aT","Vy", "Ny","Nx","NHL","KG6","KG8"}},
                    //真攻角 升降速度 法向过载 纵向过载 右发高压转速 右起落架放下 襟翼放下35°

                                    new ChartPanel(){ PanelID = "I", PanelName = "角速度", 
                    ParameterIDs = new string[]{"Wy","Wx", "Wz"}},
                    //俯仰角速度 横滚角速度 盘旋角速度

                                    new ChartPanel(){ PanelID = "J", PanelName = "过载", 
                    ParameterIDs = new string[]{"Ny","Nx", "Nz"}},
                    //法向过载 纵向过载 侧向过载

                                    new ChartPanel(){ PanelID = "K", PanelName = "告警信号A", 
                    ParameterIDs = new string[]{"KG2","KG3", "KG4","KG9","KG10"}},
                    //剩油1000kg 主液压系统压降 助液压系统压降 前舱盖锁紧 后舱盖锁紧
                    
                                    new ChartPanel(){ PanelID = "L", PanelName = "告警信号B", 
                    ParameterIDs = new string[]{"KG11","KG12", "KG13","KG14","KG15"}},
                    //左防冰接通 右防冰接通 左主电源脱网 右主电源脱网 失速警告信号

            };

            return panels;
        }

        internal FlightDataEntities.Decisions.DecisionRecord[]
            GetDecisionRecords(Flight flight)
        {
            if (flight == null || string.IsNullOrEmpty(flight.FlightID))
            {
                LogHelper.Warn("flight为空或FlightID为空（GetDecisionRecords）。", null);
                return null;
            }

            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，需要拆分存储的记录，最好在DAL里面去处理表名构建逻辑
                    MongoDatabase database =
                        dal.GetMongoDatabaseByAircraftModel(mongoServer,
                        flight.Aircraft.AircraftModel);
                    if (database != null)
                    {
                        MongoCollection<FlightDataEntities.Decisions.DecisionRecord> modelCollection
                            = dal.GetDecisionRecordMongoCollectionByFlight(database, flight);

                        IMongoQuery q1 = Query.EQ("FlightID", flight.FlightID);
                        var cursor = modelCollection.Find(q1);
                        var result = cursor.AsQueryable();
                        return result.ToArray();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("AddDecisionRecordsBatch", e);
                }
            }
            return null;
        }

        internal FlightDataEntities.Decisions.Decision[] GetAllDecisions(AircraftModel aircraftModel)
        {
            FlightDataEntities.Decisions.Decision[] decisions = new FlightDataEntities.Decisions.Decision[]{
                new Decision(){ DecisionID = "001", DecisionName="起飞时仰角大", EventLevel = 2, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@FY#=##FY@°>11°，dT=##dT@s≥1s",
                      //俯仰角=00°>11°，dT=0s≥1s
                      SolutionInstruction = "（1）通知训练部门；（2）查看飞机腹鳍是否擦伤",
                      RelatedParameters =new string[]{"FY","NHL","NHR","KG5","KG6","KG7"  },
                     //俯仰角(10/FY)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)，襟翼放下25°(32->7/Kg7)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "FY", Operator = CompareOperator.GreaterThan , ParameterValue =11},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue = 95},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 95},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 1},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "002", DecisionName="起飞时未放襟翼", EventLevel = 1, LastTime = 2, 
                      DecisionDescriptionStringTemplate = "dT=##dT@s≥2s",
                      //dT=0s≥2s
                      SolutionInstruction = "（1）检查飞机襟翼收放系统，同时查看前一架次飞参记录襟翼指示系统情况；（2）检查飞参襟翼通道参数记录情况。",
                      RelatedParameters =new string[]{"NHL","NHR","KG5","KG6","KG7","KG8" },
                     //左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)，襟翼放下25°(32->7/Kg7)，襟翼放下35°(32->8/Kg8)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =95},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 95},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 1},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue=1},
                         new SubCondition(){ ParameterID = "KG7", Operator = CompareOperator.Equal , ParameterValue=0},
                         new SubCondition(){ ParameterID = "KG8", Operator = CompareOperator.Equal , ParameterValue=0},
                     }},
                     new Decision(){ DecisionID = "003", DecisionName="起飞时襟翼放到35°", EventLevel = 1, LastTime = 2, 
                      DecisionDescriptionStringTemplate = "dT=##dT@s≥2s",
                      //dT=0s≥12s
                      SolutionInstruction = "（1）向飞行员了解当时的情况，同时查看前一架次飞参记录襟翼指示系统情况；（2）检查襟翼收放系统",
                      RelatedParameters =new string[]{"NHL","NHR","KG5","KG6","KG8" },
                     //左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)，襟翼放下35°(32->8/Kg8)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =95},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 95},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue=1},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue=1},
                         new SubCondition(){ ParameterID = "KG8", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "004", DecisionName="失速时未告警", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@Vi#=##Vi@km/h>150km/h，@@aT#=##aT@°>12.5°，dT=##dT@s≥1s",
                      //指示空速=000km/h>150km/h，真攻角=00°>12.5°，dT=0s≥1s
                      SolutionInstruction = "（1）通电检查振杆器工作情况；（2）检查大气数据系统",
                      RelatedParameters =new string[]{"Vi","aT","KG15" },
                     //指示空速(3/Vi)，真攻角(5/aT)，失速告警信号(32->15/Kg15)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition(){ ParameterID = "aT", Operator = CompareOperator.GreaterThan , ParameterValue = 12.5F},
                         new SubCondition(){ ParameterID = "KG15", Operator = CompareOperator.Equal , ParameterValue = 0},
                     }},
                     new Decision(){ DecisionID = "005", DecisionName="起飞后未收起落架", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "dT=##dT@s≥1s",
                      //dT=0s≥1s
                      SolutionInstruction = "（1）向飞行员了解当时的情况；（2）检查起落架收放系统",
                      RelatedParameters =new string[]{"Vi","NHL","NHR","KG5","KG6" },
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =90},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 90},
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue=500},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 1},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "006", DecisionName="剩余油量1000千克", EventLevel = 1, LastTime = 2, 
                      DecisionDescriptionStringTemplate = "dT=##dT@s≥2s",
                      //dT=0s≥2s
                      SolutionInstruction = "（1）向飞行员了解情况；（2）进行加输油检查",
                      RelatedParameters =new string[]{ "NHL","NHR","KG2"},
                     //左发高压转速(30/NHL)，右发高压转速(31/NHR)，剩油1000kg(32->2/Kg2)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "KG2", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "007", DecisionName="主液压系统压降信号", EventLevel = 1, LastTime = 4, 
                      DecisionDescriptionStringTemplate = "dT=##dT@s≥4s",
                      //dT=0s≥4s
                      RelatedParameters =new string[]{ "NHL","NHR","KG3"},
                      SolutionInstruction = "（1）向飞行员了解情况；（2）进行加输油检查",
                     //左发高压转速(30/NHL)，右发高压转速(31/NHR)，主液压系统压降(32->3/Kg3)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "KG3", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "008", DecisionName="助力液压系统压降信号", EventLevel = 1, LastTime = 3, 
                      DecisionDescriptionStringTemplate = "dT=##dT@s≥3s",
                      //dT=0s≥3s
                      SolutionInstruction = "（1）向飞行员了解情况；（2）供压检查助力液压情况",
                      RelatedParameters =new string[]{ "NHL","NHR","KG4"},
                     //左发高压转速(30/NHL)，右发高压转速(31/NHR)，助液压系统压降(32->4/Kg4)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "KG4", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "009", DecisionName="前舱盖未锁紧", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "dT=##dT@s≥1s",
                      //dT=0s≥1s
                      SolutionInstruction = "（1）检查前舱盖开启、关闭及信号情况；（2）查看前一架次飞参记录前舱盖未锁紧指示情况",
                      RelatedParameters =new string[]{ "Vi","NHL","NHR","KG9"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，前舱盖锁紧(32->9/Kg9)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "KG9", Operator = CompareOperator.Equal , ParameterValue=0},
                     }},
                     new Decision(){ DecisionID = "010", DecisionName="后舱盖未锁紧", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "dT=##dT@s≥1s",
                      SolutionInstruction = "（1）检查后舱盖开启、关闭及信号情况；（2）查看前一架次飞参记录后舱盖未锁紧指示情况",
                      //dT=0s≥1s
                      RelatedParameters =new string[]{ "NHL","NHR","KG10"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，后舱盖锁紧(32->10/Kg10)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "KG10", Operator = CompareOperator.Equal , ParameterValue=0},
                     }},
                     new Decision(){ DecisionID = "011", DecisionName="左发防冰接通", EventLevel = 2, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "dT=##dT@s≥1s",
                      //dT=0s≥1s
                      SolutionInstruction = "更换左防冰压差开关或左防冰电磁阀",
                      RelatedParameters =new string[]{ "NHL","NHR","T6L","T6R","KG11"},
                     //左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)，左防冰接通(32->11/Kg11)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "KG11", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "012", DecisionName="右发防冰接通", EventLevel = 2, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "dT=##dT@s≥1s",
                      //dT=0s≥1s
                      SolutionInstruction = "更换右防冰压差开关或右防冰电磁阀",
                       RelatedParameters =new string[]{ "NHL","NHR","T6L","T6R","KG12"},
                     //左发高压转速(30/NHL)，右发高压转速(31/NHR)，，左发排气温度(28/T6L)，右发排气温度(29/T6R)右防冰接通(32->11/Kg11)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "KG12", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "013", DecisionName="左发电机故障", EventLevel = 1, LastTime = 1, 
                       DecisionDescriptionStringTemplate = "dT=##dT@s≥1s",
                      //dT=0s≥1s
                      SolutionInstruction = "更换左调压控制保护盒或左发电机",
                      RelatedParameters =new string[]{"NHL","NHR","T6L","T6R","KG13" },
                     //左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)，左主电源脱网(32->13/Kg13)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG13", Operator = CompareOperator.Equal , ParameterValue = 1},
                     }},
                     new Decision(){ DecisionID = "014", DecisionName="右发电机故障", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "dT=##dT@s≥1s",
                      //dT=0s≥1s
                      SolutionInstruction = "更换右调压控制保护盒或右发电机",
                     RelatedParameters =new string[]{ "NHL","NHR","T6L","T6R","KG14" },
                     //左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)，右主电源脱网(32->14/Kg14)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue =50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG14", Operator = CompareOperator.Equal , ParameterValue = 1},
                     }},
                     new Decision(){ DecisionID = "015", DecisionName="左发转速达到99%", EventLevel = 1, LastTime = 10, 
                     DecisionDescriptionStringTemplate = "@@NHL#=##NHL@%>99%，dT=##dT@s≥10s",
                      //左发转速=00%>99%，dT=00s≥10s
                      SolutionInstruction = "调整左发动机转速",
                     RelatedParameters =new string[]{ "Vi","NHL","NHR","T6L","T6R" },
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue = 99},
                     }},
                     new Decision(){ DecisionID = "016", DecisionName="右发转速达到99%", EventLevel = 1, LastTime = 10, 
                     DecisionDescriptionStringTemplate = "@@NHR#=##NHR@%>99%，dT=##dT@s≥10s",
                      //右发转速=00%>99%，dT=00s≥10s
                      SolutionInstruction = "调整右发动机转速",
                     RelatedParameters =new string[]{ "Vi","NHL","NHR","T6L","T6R" },
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 99},
                     }},
                     new Decision(){ DecisionID = "017", DecisionName="左发转速超转", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@NHL#=##NHL@%>101%，dT=##dT@s≥1s",
                      //左发转速=00%>101%，dT=0s≥1s
                      SolutionInstruction = "调整左发动机转速",
                     RelatedParameters =new string[]{ "Vi","NHL","NHR","T6L","T6R"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue = 101},
                     }},
                     new Decision(){ DecisionID = "018", DecisionName="右发转速超转", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@NHR#=##NHR@%>101%，dT=##dT@s≥1s",
                      //右发转速=00%>101%，dT=0s≥1s
                      SolutionInstruction = "调整右发动机转速",
                     RelatedParameters =new string[]{ "Vi","NHL","NHR","T6L","T6R"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 101},
                     }},
                     new Decision(){ DecisionID = "019", DecisionName="左发中间转速超时", EventLevel = 1, LastTime = 1800, 
                      DecisionDescriptionStringTemplate = "@@NHL#=##NHL@%>94.7%，dT=##dT@s≥1800s",
                      //左发转速=00%>94.7%，dT=000s≥1800
                      SolutionInstruction = "提醒飞行人员使用发动机",
                     RelatedParameters =new string[]{ "Vi","NHL","NHR","T6L","T6R"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue = 94.7F},
                     }},
                     new Decision(){ DecisionID = "020", DecisionName="右发中间转速超时", EventLevel = 1, LastTime = 1800, 
                      DecisionDescriptionStringTemplate = "@@NHR#=##NHR@%>94.7%，dT=##dT@s≥1800s",
                      //右发转速=00%>94.7%，dT=000s≥1800s
                      SolutionInstruction = "提醒飞行人员使用发动机",
                     RelatedParameters =new string[]{ "Vi","NHL","NHR","T6L","T6R"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 94.7F},
                     }},
                     new Decision(){ DecisionID = "021", DecisionName="左发最大军用转速超时", EventLevel = 1, LastTime = 720, 
                      DecisionDescriptionStringTemplate = "@@NHL#=##NHL@%>96%，dT=##dT@s≥720s",
                      //左发转速=00%>96%，dT=000s≥720s
                      SolutionInstruction = "提醒飞行人员使用发动机",
                      RelatedParameters =new string[]{ "Vi","NHL","NHR","T6L","T6R"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue = 96},
                     }},
                     new Decision(){ DecisionID = "022", DecisionName="右发最大军用转速超时", EventLevel = 1, LastTime = 720, 
                      DecisionDescriptionStringTemplate = "@@NHR#=##NHR@%>96%，dT=##dT@s≥720s",
                      //右发转速=00%>96%，dT=000s≥720s
                      SolutionInstruction = "提醒飞行人员使用发动机",
                     RelatedParameters =new string[]{ "Vi","NHL","NHR","T6L","T6R"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左发排气温度(28/T6L)，右发排气温度(29/T6R)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 96},
                     }},
                     new Decision(){ DecisionID = "023", DecisionName="空中左发停车", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@NHL#=##NHL@%<50%，@@NHR#=##NHR@%>53%，dT=##dT@s≥1s",
                      //左发转速=00%<50%，右发转速=00%>53%，dT=0s≥1s
                      SolutionInstruction = "（1）向飞行员了解情况；（2）机务人员加强左发动机检查 ",
                      RelatedParameters =new string[]{ "Vi","NHL","NHR","KG5","KG6","KG13"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)，左主电源脱网(32->13/Kg13)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.SmallerThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 53},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG13", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "024", DecisionName="空中右发停车", EventLevel = 1, LastTime = 1, 
                       DecisionDescriptionStringTemplate = "@@NHR#=##NHR@%<50%，@@NHL#=##NHL@%>53%，dT=##dT@s≥1s",
                      //右发转速=00%<50%，左发转速=00%>53%，dT=0s≥1s
                      SolutionInstruction = "（1）向飞行员了解情况；（2）机务人员加强右发动机检查 ",
                         RelatedParameters =new string[]{  "Vi","NHL","NHR","KG5","KG6","KG14"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)，右主电源脱网(32->14/Kg14)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.SmallerThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue = 53},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG14", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "025", DecisionName="空速超限", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@Vi#=##Vi@km/h>1250km/h，dT=##dT@s≥1s",
                      //指示空速=0000km/h>1250km/h，dT=0s≥1s
                      SolutionInstruction = "（1）向飞行员了解当时的情况；（2）根据飞机的高度、总温、真空速和气象等条件，判断实际表速；（3）检查空速表和大气数据系统",
                     RelatedParameters =new string[]{ "Vi","NHL","NHR","KG5","KG6"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue = 1250},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue=0},
                     }},
                     new Decision(){ DecisionID = "026", DecisionName="高度超限", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@Hp#=##Hp@km/h>15200km/h，dT=##dT@s≥1s",
                      //气压高度=00000km/h>15200km/h，dT=0s≥1s
                      SolutionInstruction = "（1）飞行员了解当时的情况；（2）检查大气数据系统",
                     RelatedParameters =new string[]{ "Hp", "Vi","NHL","NHR","KG5","KG6"},
                     //气压高度(2/Hp),指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Hp", Operator = CompareOperator.GreaterThan , ParameterValue =15200},
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue = 100},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue=0},
                     }},
                     new Decision(){ DecisionID = "027", DecisionName="升降速度超限", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@Vy#绝对值=##Vy@m/s>150 m/s，dT=##dT@s≥1s",
                      //升降速度绝对值=000m/s>150 m/s，dT=0s≥1s
                      SolutionInstruction = "（1）飞行员了解当时的情况；（2）检查大气数据系统",
                     RelatedParameters =new string[]{  "Vi","Vy","NHL","NHR"},
                     //指示空速(3/Vi)，升降速度(6/Vy)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "Vy",  Operator = CompareOperator.SmallerThan , ParameterValue = -150},
                                 new SubCondition(){ ParameterID = "Vy",  Operator = CompareOperator.GreaterThan , ParameterValue = 150},
                             }
                         },
                     }},
                     new Decision(){ DecisionID = "028", DecisionName="马赫数超限", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@M#=##M@>1.7，dT=##dT@s≥1s",
                      //马赫数=0>1.7，dT=0s≥1s
                      SolutionInstruction = "（1）飞行员了解当时的情况；（2）检查大气数据系统",
                     RelatedParameters =new string[]{ "Vi","M","NHL","NHR","KG5","KG6"},
                     //指示空速(3/Vi)，马赫数(4/M)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "M", Operator = CompareOperator.GreaterThan , ParameterValue = 1.7F},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue=0},
                     }},
                     new Decision(){ DecisionID = "029", DecisionName="着陆仰角过大", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@FY#=##FY@°>11°，dT=##dT@s≥1s",
                      //俯仰角=00°>11°，dT=0s≥1s
                      SolutionInstruction = "（1）通知飞行人员；（2）机务人员查看飞机腹鳍是否擦伤",
                     RelatedParameters =new string[]{ "FY","NHL","NHR","KG5","KG6","KG8"},
                     //俯仰角(10/FY)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)，襟翼放下35°(32->8/Kg8)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "FY", Operator = CompareOperator.GreaterThan , ParameterValue =11},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.SmallerThan , ParameterValue = 70},
                          new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.SmallerThan , ParameterValue = 70},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 1},
                        new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue = 1},
                         new SubCondition(){ ParameterID = "KG8", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "030", DecisionName="着陆时襟翼未放", EventLevel = 1, LastTime = 20, 
                     DecisionDescriptionStringTemplate = "dT=##dT@s≥20s",
                      //dT=0s≥1s
                      SolutionInstruction = "（1）向飞行人员了解情况；（2）机务人员检查襟翼工作情况",
                     RelatedParameters =new string[]{  "Vi","NHL","NHR","KG5","KG6","KG8"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)，襟翼放下35°(32->8/Kg8)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =310},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.SmallerThan , ParameterValue = 80},
                          new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.SmallerThan , ParameterValue = 80},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 1},
                        new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue = 1},
                         new SubCondition(){ ParameterID = "KG8", Operator = CompareOperator.Equal , ParameterValue=0},
                     }},
                     new Decision(){ DecisionID = "031", DecisionName="着陆时垂直过载过大", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@Hp#=##Hp@m<100m，@@Ny#=##Ny@g绝对值>2g，dT=##dT@s≥10s",
                      //气压高度=00m<100m，法向过载=0g绝对值>2g，dT=0s≥1s
                      SolutionInstruction = "（1）查看侧向过载和纵向过载是否正确；（2）通知训练部门；（3）机务人员加强起落架检查",
                     RelatedParameters =new string[]{ "Hp","Ny","NHL","NHR","KG5","KG6","KG8"},
                     //气压高度(2/Hp),法向过载(22/Ny)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)，襟翼放下35°(32->8/Kg8)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Hp", Operator = CompareOperator.SmallerThan , ParameterValue = 100},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "Ny",  Operator = CompareOperator.SmallerThan , ParameterValue = -2},
                                 new SubCondition(){ ParameterID = "Ny",  Operator = CompareOperator.GreaterThan , ParameterValue = 2},
                             }
                         },
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.SmallerThan , ParameterValue = 80},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.SmallerThan , ParameterValue = 80},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 1},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue= 1},
                         new SubCondition(){ ParameterID = "KG8", Operator = CompareOperator.Equal , ParameterValue= 1},
                     }},
                     new Decision(){ DecisionID = "032", DecisionName="着陆时未放起落架", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@Nx#=##Nx@g<0，dT=##dT@s≥10s",
                      //纵向过载=0g<0，dT=0s≥1s
                      SolutionInstruction = "（1）向飞行人员了解情况；（2）机务人员检查起落架收放系统工作情况",
                     RelatedParameters =new string[]{  "Nx","NHL","NHR","KG5","KG6","KG8"},
                     //纵向过载(23/Nx)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)，襟翼放下35°(32->8/Kg8)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.SmallerThan , ParameterValue = 80},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.SmallerThan , ParameterValue = 80},
                         new SubCondition(){ ParameterID = "Nx", Operator = CompareOperator.SmallerThan , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue= 0},
                         new SubCondition(){ ParameterID = "KG8", Operator = CompareOperator.Equal , ParameterValue= 1},
                     }},
                     new Decision(){ DecisionID = "033", DecisionName="M=1附近时间过长", EventLevel = 2, LastTime = 10, 
                     DecisionDescriptionStringTemplate = "0.95<（@@M#=##M@）<1.02，dT=##dT@s≥10s",
                      //0.95<（马赫数=0）<1.02，dT=0s≥10s
                      SolutionInstruction = "检查发动机和飞机结构",
                      RelatedParameters =new string[]{  "M","NHL","NHR","KG5","KG6"},
                     //马赫数(4/M)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "M", Operator = CompareOperator.GreaterThan , ParameterValue =0.95F},
                         new SubCondition(){ ParameterID = "M", Operator = CompareOperator.SmallerThan , ParameterValue = 1.02F},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue=0},
                     }},
                     new Decision(){ DecisionID = "034", DecisionName="零载荷超过2秒", EventLevel = 2, LastTime = 2, 
                     DecisionDescriptionStringTemplate = "@@Ny#=##Ny@g<0.2g，dT=##dT@s≥2s",
                      //法向过载=0g<0.2g，dT=0s≥2s
                      SolutionInstruction = "（1）检查发动机和飞机结构；（2）更换三轴加速度计",
                      RelatedParameters =new string[]{ "Vi","Ny","NHL","NHR"},
                     //指示空速(3/Vi)，法向过载(22/Ny)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "Ny", Operator = CompareOperator.SmallerThan , ParameterValue = 0.2F},
                     }},
                     new Decision(){ DecisionID = "035", DecisionName="法向过载超限", EventLevel = 2, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@Ny#=##Ny@g<-1g或>7g，dT=##dT@s≥1s",
                      //法向过载=0g<-1g或>7g，dT=0s≥1s
                      SolutionInstruction = "（1）查看空中其他状态时数据； （2）提醒飞行人员；（3）更换三轴加速度计",
                      RelatedParameters =new string[]{ "Vi","Ny","NHL","NHR"},
                     //指示空速(3/Vi)，法向过载(22/Ny)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                          new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "Ny",  Operator = CompareOperator.SmallerThan , ParameterValue = -1},
                                 new SubCondition(){ ParameterID = "Ny",  Operator = CompareOperator.GreaterThan , ParameterValue = 7},
                             }
                         },
                     }},
                     new Decision(){ DecisionID = "036", DecisionName="纵向过载超限", EventLevel = 2, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@Nx#=##Nx@g>0.7g，dT=##dT@s≥1s",
                      //纵向过载=0g>0.7g，dT=0s≥1s
                      SolutionInstruction = "（1）查看空中其他状态时数据； （2）提醒飞行人员；（3）更换三轴加速度计",
                      RelatedParameters =new string[]{ "Vi","Nx","NHL","NHR"},
                     //指示空速(3/Vi)，纵向过载(23/Nx)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition(){ ParameterID = "Nx", Operator = CompareOperator.GreaterThan , ParameterValue = 0.7F},
                     }},
                     new Decision(){ DecisionID = "037", DecisionName="侧向过载超限", EventLevel = 2, LastTime = 1, 
                       DecisionDescriptionStringTemplate = "@@Nz#=##Nz@g>0.5g，dT=##dT@s≥1s",
                      SolutionInstruction = "（1）查看空中其他状态时数据； （2）提醒飞行人员；（3）更换三轴加速度计",
                      //侧向过载=0g>0.5g，dT=0s≥1s
                      RelatedParameters =new string[]{  "Vi","Nz","NHL","NHR"},
                     //指示空速(3/Vi)，侧向过载(24/Nz)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition(){ ParameterID = "Nz", Operator = CompareOperator.GreaterThan , ParameterValue = 0.5F},
                     }},
                     new Decision(){ DecisionID = "038", DecisionName="倾斜角超限", EventLevel = 1, LastTime = 1, 
                       DecisionDescriptionStringTemplate = "@@HG#=##HG@°>65°，dT=##dT@s≥1s",
                      //倾斜角=00°>65°，dT=0s≥1s
                      SolutionInstruction = "（1）了解飞行科目；（2）查看空中其他状态时数据；（3）更换惯导设备",
                      RelatedParameters =new string[]{  "Vi","HG","NHL","NHR"},
                     //指示空速(3/Vi)，倾斜角(9/HG)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition(){ ParameterID = "HG", Operator = CompareOperator.GreaterThan , ParameterValue = 65},
                     }},
                     new Decision(){ DecisionID = "039", DecisionName="俯仰角超限", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@FY#=##FY@°>25°，dT=##dT@s≥1s",
                      //俯仰角=00°>25°，dT=0s≥1s
                      SolutionInstruction = "（1）了解飞行科目；（2）查看空中其他状态时数据；（3）更换惯导设备",
                     RelatedParameters =new string[]{  "Vi","FY","NHL","NHR"},
                     //指示空速(3/Vi)，俯仰角(10/FY)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition(){ ParameterID = "FY", Operator = CompareOperator.GreaterThan , ParameterValue = 25},
                     }},
                     new Decision(){ DecisionID = "040", DecisionName="总温传感器故障", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@Tt#=##Tt@℃ 绝对值>80℃，dT=##dT@s≥1s",
                      //大气总温=00℃绝对值>80℃，dT=0s≥1s
                      SolutionInstruction = "（1）更换飞参记录仪；（2）更换大气机；（3）更换大气总温传感器",
                     RelatedParameters =new string[]{  "Vi","Tt","NHL","NHR"},
                     //指示空速(3/Vi)，大气总温(7/Tt)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "Tt",  Operator = CompareOperator.GreaterThan , ParameterValue = 80},
                                 new SubCondition(){ ParameterID = "Tt",  Operator = CompareOperator.SmallerThan , ParameterValue = -80},
                             }
                         },
                     }},
                     new Decision(){ DecisionID = "041", DecisionName="攻角传感器故障", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@aT#=##aT@>15°，dT=##dT@≥1s",
                      //真攻角=00°>15°，dT=0s≥1s
                      SolutionInstruction = "（1）查看空中其他状态数据；（2）更换大气机；（3）更换攻角传感器",
                     RelatedParameters =new string[]{  "Vi","aT","NHL","NHR"},
                     //指示空速(3/Vi)，真攻角(5/aT)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition(){ ParameterID = "aT", Operator = CompareOperator.GreaterThan , ParameterValue = 15},
                     }},
                     new Decision(){ DecisionID = "042", DecisionName="着陆时坡度大于40°", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@HG#=##HG@>40°，dT=##dT@≥1s",
                      //倾斜角=00°>40°，dT=0s≥1s
                      SolutionInstruction = "（1）向飞行人员了解情况；（2）更换惯导",
                     RelatedParameters =new string[]{  "Vi","HG","NHL","NHR","KG5","KG6","KG8"},
                     //指示空速(3/Vi)，倾斜角(9/HG)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)，襟翼放下35°(32->8/Kg8)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition(){ ParameterID = "HG", Operator = CompareOperator.GreaterThan , ParameterValue =40},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.SmallerThan , ParameterValue = 80},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.SmallerThan , ParameterValue = 80},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 1},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue=1},
                         new SubCondition(){ ParameterID = "KG8", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "043", DecisionName="副翼偏角超限", EventLevel = 1, LastTime = 2, 
                    DecisionDescriptionStringTemplate = "@@Dx#=##Dx@° 绝对值>21°，dT=##dT@≥2s",
                      //副翼角位移=00°绝对值>21°，dT=0s≥2s
                      SolutionInstruction = "（1）查看当时状态；（2）调整副翼角位移传感器",
                      RelatedParameters =new string[]{  "Vi","Dx","NHL","NHR"},
                     //指示空速(3/Vi)，副翼角位移(25/Dx)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "Dx", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = -21},
                                 new SubCondition(){ ParameterID = "Dx", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = 21},
                             }
                         },
                     }},
                     new Decision(){ DecisionID = "044", DecisionName="副翼传感器故障", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "副翼角位移变化率=##Dx@° 绝对值<0.3°，dT=##dT@s≥1s",
                      //副翼角位移变化率=0°绝对值<0.3°，dT=0s≥1s
                      SolutionInstruction = "（1）查看当时状态；（2）更换副翼角位移传感器",
                      RelatedParameters =new string[]{ "Vi","Dx","KZB","NHL","NHR"},
                     //指示空速(3/Vi)，副翼角位移(25/Dx)，纵向状态标志(18/KZB)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "Dx", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = 0.3F},
                                 new SubCondition(){ ParameterID = "Dx", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -0.3F},
                             }
                         },
                         new SubCondition()
                         {
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[] 
                             {
                                 new SubCondition(){ 
                                     ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                                     {
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = -8600},
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -10100},
                                     }
                                 },
                                 new SubCondition(){
                                     ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                                     {
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = -4100},
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -8400},
                                     }
                                 },
                                 new SubCondition(){
                                     ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                                     {
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = 10100},
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -3900},
                                     }
                                 },
                             }
                         }
                     }},
                     new Decision(){ DecisionID = "045", DecisionName="平尾传感器故障", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "平尾角位移变化率=##Dz@° 绝对值<0.3°，dT=##dT@s≥1s",
                      //平尾角位移变化率=0°绝对值<0.3°，dT=0s≥1s
                      SolutionInstruction = "（1）查看当时状态；（2）更换平尾角位移传感器",
                      RelatedParameters =new string[]{ "Vi","Dz","KZB","NHL","NHR"},
                     //指示空速(3/Vi)，平尾角位移(27/Dz)，纵向状态标志(18/KZB)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "Dz", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = 0.3F},
                                 new SubCondition(){ ParameterID = "Dz", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -0.3F},
                             }
                         },
                         new SubCondition()
                         {
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[] 
                             {
                                 new SubCondition(){ 
                                     ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                                     {
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = -8600},
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -10100},
                                     }
                                 },
                                 new SubCondition(){
                                     ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                                     {
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = -4100},
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -8400},
                                     }
                                 },
                                 new SubCondition(){
                                     ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                                     {
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = 10100},
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -3900},
                                     }
                                 },
                             }
                         }
                     }},
                     new Decision(){ DecisionID = "046", DecisionName="方向舵传感器故障", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "方向舵角位移变化率=##Dy@°绝对值<0.3°，dT=##dT@s≥1s",
                      //方向舵角位移变化率=0°绝对值<0.3°，dT=0s≥1s
                      SolutionInstruction = "（1）查看当时状态；（2）更换方向舵角位移传感器",
                      RelatedParameters =new string[]{ "Vi","Dy","KZB","NHL","NHR"},
                     //指示空速(3/Vi)，方向舵角位移(26/Dy)，纵向状态标志(18/KZB)，左发高压转速(30/NHL)，右发高压转速(31/NHR)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =150},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "Dy", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = 0.3F},
                                 new SubCondition(){ ParameterID = "Dy", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -0.3F},
                             }
                         },
                         new SubCondition()
                         {
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[] 
                             {
                                 new SubCondition(){ 
                                     ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                                     {
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = -8600},
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -10100},
                                     }
                                 },
                                 new SubCondition(){
                                     ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                                     {
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = -4100},
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -8400},
                                     }
                                 },
                                 new SubCondition(){
                                     ParameterID = string.Empty, Relation = ConditionRelation.AND, SubConditions = new SubCondition[]
                                     {
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.SmallerThan , ParameterValue = 10100},
                                         new SubCondition(){ ParameterID = "KZB", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue = -3900},
                                     }
                                 },
                             }
                         }
                     }},
                     new Decision(){ DecisionID = "047", DecisionName="飞控出现自动拉起信号", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "dT=##dT@s≥1s",
                      //dT=0s≥1s
                      SolutionInstruction = "（1）查看当时状态；（2）更换无线电高度表",
                      RelatedParameters =new string[]{  "KZB","KG5","KG6"},
                     //纵向状态标志(18/KZB)，左起落架放下(32->5/Kg5)，右起落架放下(32->6/Kg6)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "KZB", Operator = CompareOperator.GreaterThan , ParameterValue = 3400},
                         new SubCondition(){ ParameterID = "KZB", Operator = CompareOperator.SmallerThan , ParameterValue = 3600},
                         new SubCondition(){ ParameterID = "KG5", Operator = CompareOperator.Equal , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG6", Operator = CompareOperator.Equal , ParameterValue=0}, 
                     }},
                     new Decision(){ DecisionID = "048", DecisionName="起飞时左发转速低", EventLevel = 2, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@NHL#=##NHL@%<95%，dT=##dT@s≥1s",
                      //左发转速=00%<95%，dT=00s≥1s
                      SolutionInstruction = "（1）调整左发动机军用转速；（2）更换左发主燃条",
                      RelatedParameters =new string[]{  "M","Nx","NHL","NHR","KG7"},
                     //马赫数(4/M)，纵向过载(23/Nx)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，襟翼放下25°(31->7/Kg7)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "M", Operator = CompareOperator.GreaterThan , ParameterValue = 0.19F},
                         new SubCondition(){ ParameterID = "M", Operator = CompareOperator.SmallerThan , ParameterValue = 0.22F},
                         new SubCondition(){ ParameterID = "Nx", Operator = CompareOperator.GreaterThan , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG7", Operator = CompareOperator.Equal , ParameterValue=1},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue = 70},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.SmallerThan , ParameterValue = 95},
                     }},
                     new Decision(){ DecisionID = "049", DecisionName="起飞时右发转速低", EventLevel = 2, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@NHR#=##NHR@%<95%，dT=##dT@s≥1s",
                      //右发转速=00%<95%，dT=00s≥1s
                      SolutionInstruction = "（1）调整右发动机军用转速；（2）更换右发主燃条",
                     RelatedParameters =new string[]{ "M","Nx","NHL","NHR","KG7"},
                     //马赫数(4/M)，纵向过载(23/Nx)，左发高压转速(30/NHL)，右发高压转速(30/NHR)，襟翼放下25°(32->7/Kg7)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "M", Operator = CompareOperator.GreaterThan , ParameterValue = 0.19F},
                         new SubCondition(){ ParameterID = "M", Operator = CompareOperator.SmallerThan , ParameterValue = 0.22F},
                         new SubCondition(){ ParameterID = "Nx", Operator = CompareOperator.GreaterThan , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG7", Operator = CompareOperator.Equal , ParameterValue=1},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 70},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.SmallerThan , ParameterValue = 95},
                     }},
                     new Decision(){ DecisionID = "050", DecisionName="左发起动时超温", EventLevel = 1, LastTime = 1, 
                          DecisionDescriptionStringTemplate = "@@T6L#=##T6L@℃>630℃，dT=##dT@s≥1s",
                          SolutionInstruction = "（1）更换左发起动机；（2）更换左发动机",
                     RelatedParameters =new string[]{ "T6L","T6R","KG13"},
                     //左发排气温度(28/T6L)，右发排气温度(29/T6R)，左主电源脱网(37->13/Kg13)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "T6L", Operator = CompareOperator.GreaterThan , ParameterValue =630},
                         new SubCondition(){ ParameterID = "KG13", Operator = CompareOperator.Equal , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "051", DecisionName="右发起动时超温", EventLevel = 1, LastTime = 1, 
                          DecisionDescriptionStringTemplate = "@@T6R#=##T6R@℃>630℃，dT=##dT@s≥1s",
                          SolutionInstruction = "（1）更换右发起动机；（2）更换右发动机",
                     RelatedParameters =new string[]{ "T6L","T6R","KG14"},
                     //左发排气温度(28/T6L)，右发排气温度(29/T6R)，右主电源脱网(37->14/Kg14)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "T6R", Operator = CompareOperator.GreaterThan , ParameterValue =630},
                         new SubCondition(){ ParameterID = "KG14", Operator = CompareOperator.Equal , ParameterValue = 1},
                     }},
                     new Decision(){ DecisionID = "052", DecisionName="左发起动后超温", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@T6L#=##T6L@℃>700℃，dT=##dT@s≥1s",
                      //左发排气温度=000℃>700℃，dT=00s≥1s
                      SolutionInstruction ="更换左发动机",
                      RelatedParameters =new string[]{ "T6L","T6R","KG13"},
                     //左发排气温度(28/T6L)，右发排气温度(29/T6R)，左主电源脱网(37->13/Kg13)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "T6L", Operator = CompareOperator.GreaterThan , ParameterValue =700},
                         new SubCondition(){ ParameterID = "KG13", Operator = CompareOperator.Equal , ParameterValue = 0},
                     }},
                     new Decision(){ DecisionID = "053", DecisionName="右发起动后超温", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@T6R#=##T6R@℃>700℃，dT=##dT@s≥1s",
                      //右发排气温度=000℃>700℃，dT=00s≥1s
                      SolutionInstruction    = "更换左发动机",
                      RelatedParameters =new string[]{ "T6L","T6R","KG14"},
                     //左发排气温度(28/T6L)，右发排气温度(29/T6R)，右主电源脱网(37->14/Kg14)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "T6R", Operator = CompareOperator.GreaterThan , ParameterValue =700},
                         new SubCondition(){ ParameterID = "KG14", Operator = CompareOperator.Equal , ParameterValue=0},
                     }},
                     new Decision(){ DecisionID = "054", DecisionName="放伞时指示空速大", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@Vi#=##Vi@km/h>305km/h，dT=##dT@≥1s",
                      //指示空速=0000km/h>305km/h，dT=0s≥1s
                      RelatedParameters =new string[]{  "Vi","Nx","NHL","NHR","KG8"},
                      SolutionInstruction  = "（1）向飞行员了解情况；（2）通知保伞室监控该减速伞",
                     //指示空速(3/Vi)，纵向过载(23/Nx)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，襟翼放下35°(32->8/Kg8)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue = 305},
                         new SubCondition(){ ParameterID = "Nx", Operator = CompareOperator.SmallerThan , ParameterValue = -0.25F},
                         new SubCondition(){ ParameterID = "KG8", Operator = CompareOperator.Equal , ParameterValue = 1},
                     }},
                     new Decision(){ DecisionID = "055", DecisionName="襟翼35°时指示空速小", EventLevel = 2, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "100km/h<（@@Vi#=##Vi@km/h）<280km/h，dT=##dT@≥1s",
                      //100km/h<（指示空速=000km/h）<280km/h，dT=0s≥1s
                      SolutionInstruction = "调整微动开关",
                      RelatedParameters =new string[]{  "Vi","NHL","NHR","KG8"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，襟翼放下35°(32->8/Kg8)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =100},
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.SmallerThan , ParameterValue = 280},
                         new SubCondition(){ ParameterID = "Nx", Operator = CompareOperator.GreaterThan , ParameterValue = 0},
                         new SubCondition(){ ParameterID = "KG8", ConditionType = SubConditionType.DeltaRate, Operator = CompareOperator.GreaterThan , ParameterValue=1},
                     }},
                     new Decision(){ DecisionID = "056", DecisionName="飞机滑行时俯仰角大", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "@@FY#=##FY@° 绝对值>6°，dT=##dT@s≥1s",
                      //俯仰角=0°绝对值>6°，dT=0s≥1s
                      SolutionInstruction = "（1）向飞行员了解情况；（2）更换惯导",
                      RelatedParameters =new string[]{ "Vi","NHL","NHR","FY"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，俯仰角(10/FY)
                     Conditions = new SubCondition[]{ new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.SmallerThan , ParameterValue =200},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.SmallerThan , ParameterValue = 90},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.SmallerThan , ParameterValue = 90},
                         new SubCondition()
                            { ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                                {
                                     new SubCondition(){ ParameterID = "FY", Operator = CompareOperator.GreaterThan , ParameterValue = 6},
                                     new SubCondition(){ ParameterID = "FY", Operator = CompareOperator.SmallerThan , ParameterValue = -6}
                                }
                            }
                     }},
                     new Decision(){ DecisionID = "057", DecisionName="飞机滑行时倾斜角大", EventLevel = 1, LastTime = 1, 
                      DecisionDescriptionStringTemplate = "@@HG#=##HG@° 绝对值>6°，dT=##dT@s≥1s",
                      //倾斜角=0°绝对值>6°，dT=0s≥1s
                      SolutionInstruction = "（1）向飞行员了解情况；（2）更换惯导",
                         RelatedParameters =new string[]{  "Vi","NHL","NHR","HG"},
                     //指示空速(3/Vi)，左发高压转速(30/NHL)，右发高压转速(31/NHR)，倾斜角(9/HG)
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.SmallerThan , ParameterValue =200},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "NHL", Operator = CompareOperator.SmallerThan , ParameterValue = 90},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.GreaterThan , ParameterValue = 50},
                         new SubCondition(){ ParameterID = "NHR", Operator = CompareOperator.SmallerThan , ParameterValue = 90},
                         new SubCondition()
                            { ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                                {
                                     new SubCondition(){ ParameterID = "HG", Operator = CompareOperator.GreaterThan , ParameterValue = 6},
                                     new SubCondition(){ ParameterID = "HG", Operator = CompareOperator.SmallerThan , ParameterValue = -6}
                                }
                            }
                     }},
            };

            return decisions;
        }

        internal Flight[] GetAllFlights(AircraftModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.ModelName))
            {
                LogHelper.Warn("model为空或ModelName为空（GetAllFlights）。", null);
                return null;
            }

            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {
                    MongoDatabase database = dal.GetMongoDatabaseCommon(mongoServer);
                    //这是实体，直接取Common吧
                    //dal.GetMongoDatabaseByAircraftModel(mongoServer, flight.Aircraft.AircraftModel);

                    if (database != null)
                    {
                        MongoCollection<Flight> modelCollection
                            = database.GetCollection<Flight>(AircraftMongoDb.COLLECTION_FLIGHT);

                        var queryable = modelCollection.AsQueryable();
                        var result = from one in queryable
                                     where one.Aircraft != null && one.Aircraft.AircraftModel != null
                                     && one.Aircraft.AircraftModel.ModelName == model.ModelName
                                     select one;

                        return result.ToArray();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("GetAllFlights", e);
                }
            }

            return null;
        }

        internal Flight[] GetAllFlights(AircraftModel model, AircraftInstance instance)
        {
            if (model == null || string.IsNullOrEmpty(model.ModelName))
            {
                LogHelper.Warn("model为空或ModelName为空（GetAllFlights）。", null);
                return null;
            }

            if (instance == null || string.IsNullOrEmpty(instance.AircraftNumber))
                return GetAllFlights(model);

            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {
                    MongoDatabase database = dal.GetMongoDatabaseCommon(mongoServer);
                    //这是实体，直接取Common吧
                    //dal.GetMongoDatabaseByAircraftModel(mongoServer, flight.Aircraft.AircraftModel);

                    if (database != null)
                    {
                        MongoCollection<Flight> modelCollection
                            = database.GetCollection<Flight>(AircraftMongoDb.COLLECTION_FLIGHT);

                        var queryable = modelCollection.AsQueryable();
                        var result = from one in queryable
                                     where one.Aircraft != null && one.Aircraft.AircraftModel != null
                                     && one.Aircraft.AircraftModel.ModelName == model.ModelName
                                     && one.Aircraft.AircraftNumber == instance.AircraftNumber //加上机号判断
                                     select one;

                        return result.ToArray();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("GetAllFlights", e);
                }
            }

            return null;
        }

        internal ExtremumReportDefinition GetExtremumReportDefinition(string aircraftModelName)
        {
            //debug，暂时只有一个机型
            return new ExtremumReportDefinition()
            {
                AircraftModelName = aircraftModelName,
                Items = new ExtremumReportItemDefinition[]{
                 /*   2	气压高度	Hp
3	指示空速	Vi
4	马赫数	M
5	真攻角	aT
6	升降速度	Vy
7	大气总温	Tt
8	真航向	ZH
9	倾斜角	HG
10	俯仰角	FY
13	偏流角	DR
14	地速	GS
15	俯仰角速度	Wy
16	横滚角速度	Wx
17	盘旋角速度	Wz
18	纵向状态标志	KZB
19	横向状态标志	KCB
20	平尾伺服器输入	ZS
21	副翼伺服器输入	CS
22	法向过载	Ny
23	纵向过载	Nx
24	侧向过载	Nz
25	副翼角位移	Dx
26	方向舵角位移	Dy
27	平尾角位移	Dz
28	左发排气温度	T6L
29	右发排气温度	T6R
30	左发高压转速	NHL
31	右发高压转速	NHR*/
                 new ExtremumReportItemDefinition(){ ParameterID = "Hp", Number = 2},
                 new ExtremumReportItemDefinition(){ ParameterID = "Vi", Number = 3},
                 new ExtremumReportItemDefinition(){ ParameterID = "M", Number = 4},
                 new ExtremumReportItemDefinition(){ ParameterID = "aT", Number = 5},
                 new ExtremumReportItemDefinition(){ ParameterID = "Vy", Number = 6},
                 new ExtremumReportItemDefinition(){ ParameterID = "Tt", Number = 7},
                 new ExtremumReportItemDefinition(){ ParameterID = "ZH", Number = 8},
                 new ExtremumReportItemDefinition(){ ParameterID = "HG", Number = 9},
                 new ExtremumReportItemDefinition(){ ParameterID = "FY", Number = 10},
                 new ExtremumReportItemDefinition(){ ParameterID = "DR", Number = 13},
                 new ExtremumReportItemDefinition(){ ParameterID = "GS", Number = 14},
                 new ExtremumReportItemDefinition(){ ParameterID = "Wy", Number = 15},
                 new ExtremumReportItemDefinition(){ ParameterID = "Wx", Number = 16},
                 new ExtremumReportItemDefinition(){ ParameterID = "Wz", Number = 17},
                 new ExtremumReportItemDefinition(){ ParameterID = "KZB", Number = 18},
                 new ExtremumReportItemDefinition(){ ParameterID = "KCB", Number = 19},
                 new ExtremumReportItemDefinition(){ ParameterID = "ZS", Number = 20},
                 new ExtremumReportItemDefinition(){ ParameterID = "CS", Number = 21},
                 new ExtremumReportItemDefinition(){ ParameterID = "Ny", Number = 22},
                 new ExtremumReportItemDefinition(){ ParameterID = "Nx", Number = 23},
                 new ExtremumReportItemDefinition(){ ParameterID = "Nz", Number = 24},
                 new ExtremumReportItemDefinition(){ ParameterID = "Dx", Number = 25},
                 new ExtremumReportItemDefinition(){ ParameterID = "Dy", Number = 26},
                 new ExtremumReportItemDefinition(){ ParameterID = "Dz", Number = 27},
                 new ExtremumReportItemDefinition(){ ParameterID = "T6L", Number = 28},
                 new ExtremumReportItemDefinition(){ ParameterID = "T6R", Number = 29},
                 new ExtremumReportItemDefinition(){ ParameterID = "NHL", Number = 30},
                 new ExtremumReportItemDefinition(){ ParameterID = "NHR", Number = 31},
                }
            };
        }

        internal FlightConditionDecision[] GetAllFlightConditionDecisions(AircraftModel aircraft)
        {
            FlightDataEntities.Decisions.FlightConditionDecision[] decisions = new FlightDataEntities.Decisions.FlightConditionDecision[]{
                new FlightConditionDecision(){ DecisionID = "1", DecisionName="停车通电状态", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "(Vi<5)&((NHL<5%)+(NHR<5%))",
                      //(Vi<5)&((NHL<5%)+(NHR<5%))
                      RelatedParameters =new string[]{"Vi","NHL","NHR" },
                     
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.SmallerOrEqual , ParameterValue =5},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "NHL",  Operator = CompareOperator.SmallerThan , ParameterValue = 5},
                                 new SubCondition(){ ParameterID = "NHR",  Operator = CompareOperator.SmallerThan , ParameterValue = 5},
                             }
                         },
                     }},

                      new FlightConditionDecision(){ DecisionID = "2", DecisionName="发动机地面开车状态", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "(Vi<5)&((NHL>5%)+(NHR>5%))",
                      //(Vi<5)&((NHL>5%)+(NHR>5%))
                      RelatedParameters =new string[]{"Vi","NHL","NHR" },
                     
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.SmallerOrEqual , ParameterValue =5},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "NHL",  Operator = CompareOperator.GreaterThan , ParameterValue = 5},
                                 new SubCondition(){ ParameterID = "NHR",  Operator = CompareOperator.GreaterThan , ParameterValue = 5},
                             }
                         },
                     }},
                     
                      new FlightConditionDecision(){ DecisionID = "3", DecisionName="正常飞行状态", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "(Vi>5)&((NHL>5%)+(NHR>5%))",
                      //(Vi>5)&((NHL>5%)+(NHR>5%))
                      RelatedParameters =new string[]{"Vi","NHL","NHR" },
                     
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =5},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "NHL",  Operator = CompareOperator.GreaterThan , ParameterValue = 5},
                                 new SubCondition(){ ParameterID = "NHR",  Operator = CompareOperator.GreaterThan , ParameterValue = 5},
                             }
                         },
                     }},
                     
                      new FlightConditionDecision(){ DecisionID = "4", DecisionName="最大军用转速状态", EventLevel = 1, LastTime = 1, 
                     DecisionDescriptionStringTemplate = "(Vi>5)&((NHL>96%)+(NHR>96%))",
                      //(Vi>5)&((NHL>96%)+(NHR>96%))
                      RelatedParameters =new string[]{"Vi","NHL","NHR" },
                     
                     Conditions = new SubCondition[]{
                         new SubCondition(){ ParameterID = "Vi", Operator = CompareOperator.GreaterThan , ParameterValue =5},
                         new SubCondition()
                         { 
                             ParameterID = string.Empty, Relation = ConditionRelation.OR, SubConditions = new SubCondition[]
                             {
                                 new SubCondition(){ ParameterID = "NHL",  Operator = CompareOperator.GreaterThan , ParameterValue = 96},
                                 new SubCondition(){ ParameterID = "NHR",  Operator = CompareOperator.GreaterThan , ParameterValue = 96},
                             }
                         },
                     }},
            };

            return decisions;
        }

        internal FlightRawDataRelationPoint[] GetFlightRawDataRelationPoints(AircraftModel aircraft, string flightID,
            string XAxisParameterID, string YAxisParameterID)
        {
            if (string.IsNullOrEmpty(flightID))
            {
                LogHelper.Warn("flight为空或FlightID为空（GetFlightRawDataRelationPoints）。", null);
                return null;
            }

            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，需要拆分存储的记录，最好在DAL里面去处理表名构建逻辑
                    MongoDatabase database =
                        dal.GetMongoDatabaseByAircraftModel(mongoServer,
                        aircraft);
                    if (database != null)
                    {
                        MongoCollection<FlightDataEntities.FlightRawDataRelationPoint> modelCollection
                            = dal.GetFlightRawDataRelationPointsMongoCollection(database, flightID);

                        List<IMongoQuery> qs = new List<IMongoQuery>();
                        IMongoQuery q1 = Query.EQ("FlightID", flightID);
                        if (!string.IsNullOrEmpty(XAxisParameterID))
                        {
                            IMongoQuery q2 = Query.EQ("XAxisParameterID", XAxisParameterID);
                            qs.Add(q2);
                        }
                        if (!string.IsNullOrEmpty(YAxisParameterID))
                        {
                            IMongoQuery q3 = Query.EQ("YAxisParameterID", YAxisParameterID);
                            qs.Add(q3);
                        }

                        if (qs.Count == 0)
                        {
                            var cursor = modelCollection.Find(q1);
                            var result = cursor.AsQueryable();
                            return result.ToArray();
                        }
                        else
                        {
                            qs.Insert(0, q1);
                            IMongoQuery all = Query.And(qs);
                            var cursor = modelCollection.Find(all);
                            var result = cursor.AsQueryable();
                            return result.ToArray();
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("GetFlightRawDataRelationPoints", e);
                }
            }
            return null;
        }

        internal ExtremumPointInfo[] GetExtremumPointInfos(AircraftInstance aircraftInstance)
        {
            if (aircraftInstance == null || string.IsNullOrEmpty(aircraftInstance.AircraftNumber))
            {
                LogHelper.Warn("aircraftInstance为空或AircraftNumber为空（GetExtremumPointInfos）。", null);
                return null;
            }

            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，需要拆分存储的记录，最好在DAL里面去处理表名构建逻辑
                    MongoDatabase database =
                        dal.GetMongoDatabaseByAircraftModel(mongoServer,
                        aircraftInstance.AircraftModel);
                    if (database != null)
                    {
                        MongoCollection<FlightDataEntities.ExtremumPointInfo> modelCollection
                            = dal.GetFlightExtremeMongoCollectionByFlight(database, null);

                        List<IMongoQuery> qs = new List<IMongoQuery>();
                        IMongoQuery q1 = Query.EQ("AircraftNumber", aircraftInstance.AircraftNumber);
                        var cursor = modelCollection.Find(q1);
                        var result = cursor.AsQueryable();
                        return result.ToArray();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("GetFlightRawDataRelationPoints", e);
                }
            }
            return null;
        }

        internal GlobeData[] GetGlobeDatas(string flightID, AircraftModel model, int startIndex, int endIndex)
        {
            if (model == null || string.IsNullOrEmpty(model.ModelName) || string.IsNullOrEmpty(flightID))
            {
                LogHelper.Warn("aircraftModel为空或aircraftModel.ModelName为空或FlightID为空（GetGlobeDatas）。", null);
                return null;
            }

            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，需要拆分存储的记录，最好在DAL里面去处理表名构建逻辑
                    MongoDatabase database =
                        dal.GetMongoDatabaseByAircraftModel(mongoServer, model);
                    if (database != null)
                    {
                        MongoCollection<FlightDataEntities.GlobeData> modelCollection
                            = dal.GetFlightGlobeDataMongoCollectionByFlight(database, flightID);

                        List<IMongoQuery> qs = new List<IMongoQuery>();

                        IMongoQuery q1 = Query.EQ("FlightID", new MongoDB.Bson.BsonString(flightID));
                        IMongoQuery q2 = Query.GTE("Index", new MongoDB.Bson.BsonInt32(startIndex));
                        IMongoQuery q3 = Query.LTE("Index", new MongoDB.Bson.BsonInt32(endIndex));
                        IMongoQuery q4 = Query.EQ("AircraftModelName", new MongoDB.Bson.BsonString(model.ModelName));
                        IMongoQuery query = Query.And(q1, q2, q3, q4);

                        var cursor = modelCollection.Find(query);
                        var result = cursor.AsQueryable();
                        return result.ToArray();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("GetGlobeDatas", e);
                }
            }
            return null;
        }

        internal AircraftInstance[] GetAllAircrafts(AircraftModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.ModelName))
            {
                LogHelper.Warn("aircraftModel为空或aircraftModel.ModelName为空或FlightID为空（GetAllAircrafts）。", null);
                return null;
            }

            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，需要拆分存储的记录，最好在DAL里面去处理表名构建逻辑
                    MongoDatabase database =
                        dal.GetMongoDatabaseByAircraftModel(mongoServer, model);
                    if (database != null)
                    {
                        MongoCollection<FlightDataEntities.AircraftInstance> modelCollection
                            = dal.GetAircraftInstanceCollection(database);

                        modelCollection.FindAll();

                        //List<IMongoQuery> qs = new List<IMongoQuery>();

                        //IMongoQuery q1 = Query.EQ("FlightID", new MongoDB.Bson.BsonString(flightID));
                        //IMongoQuery q2 = Query.GTE("Index", new MongoDB.Bson.BsonInt32(startIndex));
                        //IMongoQuery q3 = Query.LTE("Index", new MongoDB.Bson.BsonInt32(endIndex));
                        //IMongoQuery q4 = Query.EQ("AircraftModelName", new MongoDB.Bson.BsonString(model.ModelName));
                        //IMongoQuery query = Query.And(q1, q2, q3, q4);

                        var cursor = modelCollection.FindAll();// modelCollection.Find(query);
                        var result = cursor.AsQueryable();
                        return result.ToArray();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("GetAllAircrafts", e);
                }
            }
            return null;
        }

        internal int GetEarliestYear(AircraftModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.ModelName))
            {
                LogHelper.Warn("aircraftModel为空或aircraftModel.ModelName为空或FlightID为空（GetEarliestYear）。", null);
                return DateTime.Now.Year;
            }

            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，需要拆分存储的记录，最好在DAL里面去处理表名构建逻辑
                    MongoDatabase database = dal.GetMongoDatabaseCommon(mongoServer);
                    if (database != null)
                    {
                        if (database != null)
                        {
                            MongoCollection<Flight> modelCollection
                                = database.GetCollection<Flight>(AircraftMongoDb.COLLECTION_FLIGHT);

                            var queryable = modelCollection.AsQueryable();
                            var result = from one in queryable
                                         where one.Aircraft != null && one.Aircraft.AircraftModel != null
                                         && one.Aircraft.AircraftModel.ModelName == model.ModelName
                                         select Convert.ToInt32(one.FlightDate.Year);

                            if (result != null && result.Count() > 0)
                            {
                                return result.ToArray().Min();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("GetAllAircrafts", e);
                }
            }
            return DateTime.Now.Year;
        }

        internal DecisionRecord[] GetFlightConditionDecisionRecords(AircraftModel model,
            DateTime startYearMonth, DateTime endYearMonth, string[] aircraftNumbers)
        {
            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，但肯定LevelTopRecord需要包含趋势分析等信息，
                    //建议不要分表，存放在Common里面
                    MongoDatabase database = dal.GetMongoDatabaseByAircraftModel(mongoServer, model);

                    IMongoQuery q1 = Query.GTE("FlightDate", new BsonDateTime(startYearMonth));
                    IMongoQuery q2 = Query.LTE("FlightDate", new BsonDateTime(endYearMonth));

                    IMongoQuery q0 = null;
                    if (database != null)
                    {
                        if (aircraftNumbers != null && aircraftNumbers.Length > 0)
                        {
                            MongoCollection<Flight> modelCollection1 =
                                database.GetCollection<Flight>(AircraftMongoDb.COLLECTION_FLIGHT);

                            var queryableFlights = modelCollection1.AsQueryable();
                            var flights = from oneFlight in queryableFlights
                                          where oneFlight.Aircraft != null && oneFlight.Aircraft.AircraftModel != null
                                          && oneFlight.Aircraft.AircraftModel.ModelName == model.ModelName
                                          && oneFlight.FlightDate > startYearMonth && oneFlight.FlightDate <= endYearMonth
                                          && aircraftNumbers.Contains(oneFlight.Aircraft.AircraftNumber)
                                          select oneFlight.FlightID;

                            var flights2 = flights.Distinct();

                            List<MongoDB.Bson.BsonString> list = new List<BsonString>();
                            foreach (var f in flights2)
                            {
                                list.Add(new BsonString(f));
                            }
                            q0 = Query.In("FlightID", list);
                        }
                    }

                    MongoCollection<FlightDataEntities.Decisions.DecisionRecord> modelCollection2
                        = dal.GetFlightConditionDecisionRecordMongoCollectionByFlight(database, null);//没有按Flight分割的

                    IMongoQuery query = q0;//(q0 == null) ? Query.And(q1, q2) : Query.And(q1, q2, q0);

                    if (query != null)
                    {
                        var cursor = modelCollection2.Find(query);

                        return cursor.ToArray();
                    }

                    return new DecisionRecord[] { };
                }
                catch (Exception e)
                {
                    LogHelper.Error("GetFlightConditionDecisionRecords", e);
                    return new DecisionRecord[] { };
                }
            }

            return new DecisionRecord[] { };
        }

        internal string GetAppConfigValue(string appKey)
        {
            try
            {
                if (ConfigurationManager.AppSettings[appKey] != null)
                {
                    return ConfigurationManager.AppSettings[appKey];
                }

                return string.Empty;
            }
            catch (Exception e)
            {
                LogHelper.Error("GetFlightConditionDecisionRecords", e);
                return e.Message;
            }

            return string.Empty;
        }
    }
}