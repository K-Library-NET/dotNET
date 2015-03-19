using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    /// <summary>
    /// 生成极值和统一的第二层值
    /// </summary>
    public class DataPointReducer
    {
        public Level1FlightRecord[] ReduceFlightRawDataPoints(string parameterID, string flightID,
            List<ParameterRawData> points, int startSecond, int endSecond, int secondGap)
        {
            if (points == null || points.Count == 0)
                return new Level1FlightRecord[] { };

            //1. 每秒钟取一个点
            var wrapped = from one in points
                          select new ParameterRawDataWrap(one);

            //2. 每secondGap取一个值
            List<Level1FlightRecord> records = new List<Level1FlightRecord>();
            List<ParameterRawDataWrap> tempList = new List<ParameterRawDataWrap>();

            int startSec = startSecond;
            int endSec = Math.Min(startSec + secondGap, points[points.Count - 1].Second);
            if (startSec + secondGap >= endSecond)
                endSec = endSecond;
            foreach (var one in wrapped)
            {
                if (one.m_RawData.Second >= startSec
                    && one.m_RawData.Second < endSec)
                {
                    tempList.Add(one);
                }
                else
                {
                    Level1FlightRecord rec = new Level1FlightRecord()
                    {
                        FlightID = flightID,
                        ParameterID = parameterID,
                        StartSecond = startSec,
                        EndSecond = endSec,
                        Values = (from o in tempList
                                  select o.SummaryValue).ToArray()
                    };
                    records.Add(rec);
                    tempList.Clear();

                    startSec = endSec;
                    endSec = Math.Min(endSec + secondGap, points[points.Count - 1].Second);
                    if (startSec + secondGap >= endSecond)
                        endSec = endSecond;
                    tempList.Add(one);
                }
            }

            if (tempList.Count > 0)
            {
                Level1FlightRecord rec2 = new Level1FlightRecord()
                {
                    FlightID = flightID,
                    ParameterID = parameterID,
                    StartSecond = startSec,
                    EndSecond = endSec,
                    Values = (from o in tempList
                              select o.SummaryValue).ToArray()
                };
                records.Add(rec2);
                tempList.Clear();
            }

            return records.ToArray();
        }

        class ParameterRawDataWrap
        {
            public ParameterRawData m_RawData = null;

            public ParameterRawDataWrap(ParameterRawData rawData)
            {
                if (rawData == null)
                    throw new NullReferenceException();
                m_RawData = rawData;
            }

            ///<summary>
            /// 写死只要第一个值
            ///</summary>
            public float SummaryValue
            {
                get
                {
                    if (m_RawData.Values != null && m_RawData.Values.Length > 0)
                        return m_RawData.Values[0];
                    return 0;
                }
            }
        }

        public Level2FlightRecord[] GenerateLevel2FlightRecord(string flightID, string parameterID,
            Level1FlightRecord[] level1Points)
        {
            Level2FlightRecord level2Records = GenerateLevel2FlightRecordByOnlyOneResult(flightID, parameterID, level1Points);

            if (level2Records == null)
                return null;
            return new Level2FlightRecord[] { level2Records };
        }

        private static Level2FlightRecord GenerateLevel2FlightRecordByOnlyOneResult(string flightID,
            string parameterID, Level1FlightRecord[] level1Points)
        {
            if (level1Points == null || level1Points.Length == 0)
                return null;

            Level2FlightRecord level2Records = new Level2FlightRecord()
            {
                StartSecond = 0,
                EndSecond = level1Points[level1Points.Length - 1].EndSecond,
                ParameterID = parameterID,
                FlightID = flightID,
                ExtremumPointInfo = new ExtremumPointInfo()
                {
                    FlightID = flightID, 
                    ParameterID = parameterID,
                    MaxValue =
                        (from one in level1Points
                         select one.Values.Max()).Max(),
                    MinValue = (from one in level1Points
                                select one.Values.Min()).Min()
                }
            };

            float maxValue = float.MinValue;
            float minValue = float.MaxValue;

            Level1FlightRecord minRec = null;
            Level1FlightRecord maxRec = null;

            foreach (Level1FlightRecord rec in level1Points)
            {
                if (rec.Values.Max() > maxValue)
                {
                    maxValue = rec.Values.Max();
                    maxRec = rec;
                }
                if (rec.Values.Min() < minValue)
                {
                    minValue = rec.Values.Min();
                    minRec = rec;
                }
            }

            if (maxRec != null)
            {
                for (int i = 0; i < maxRec.Values.Length; i++)
                {
                    if (maxRec.Values[i] == maxValue)
                    {
                        level2Records.ExtremumPointInfo.MaxValueSecond
                            = maxRec.StartSecond + (i * Convert.ToSingle(maxRec.EndSecond - maxRec.StartSecond) / maxRec.Values.Length);
                        break;
                    }
                }

                level2Records.ExtremumPointInfo.MaxValue = maxRec.Values.Max();
            }
            if (minRec != null)
            {
                for (int i = 0; i < minRec.Values.Length; i++)
                {
                    if (minRec.Values[i] == minValue)
                    {
                        level2Records.ExtremumPointInfo.MinValueSecond
                            = minRec.StartSecond + (i * Convert.ToSingle(maxRec.EndSecond - maxRec.StartSecond) / maxRec.Values.Length);
                        break;
                    }
                }

                level2Records.ExtremumPointInfo.MinValue = minRec.Values.Min();
            }
            return level2Records;
        }

        public List<LevelTopFlightRecord> GenerateLevelTopFlightRecord(string flightID,
            Dictionary<string, List<Level2FlightRecord>> level2RecordMap, int startSecond, int endSecond)
        {
            List<LevelTopFlightRecord> topRecords = new List<LevelTopFlightRecord>();

            foreach (string key in level2RecordMap.Keys)
            {
                LevelTopFlightRecord top = new LevelTopFlightRecord()
                {
                    ParameterID = key,
                    FlightID = flightID,
                    StartSecond = startSecond,
                    EndSecond = endSecond,
                    ExtremumPointInfo = this.FindExtremumPointInfo(flightID, key, level2RecordMap[key]),
                    Level2FlightRecord = level2RecordMap[key].ToArray()
                };

                topRecords.Add(top);
            }

            return topRecords;
        }

        private ExtremumPointInfo FindExtremumPointInfo(string flightID, string parameterID, List<Level2FlightRecord> list)
        {
            ExtremumPointInfo info = new ExtremumPointInfo()
            {
                FlightID = flightID,
                ParameterID = parameterID,
                MaxValue = float.MinValue,
                MinValue = float.MaxValue
            };

            foreach (var lev2 in list)
            {
                if (lev2.ExtremumPointInfo.MaxValue > info.MaxValue)
                {
                    info.MaxValue = lev2.ExtremumPointInfo.MaxValue;
                    info.MaxValueSecond = lev2.ExtremumPointInfo.MaxValueSecond;
                }
                if (lev2.ExtremumPointInfo.MinValue < info.MinValue)
                {
                    info.MinValue = lev2.ExtremumPointInfo.MinValue;
                    info.MinValueSecond = lev2.ExtremumPointInfo.MinValueSecond;
                }
            }

            return info;
        }
    }
}
