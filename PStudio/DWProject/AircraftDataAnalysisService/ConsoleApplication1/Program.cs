using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightDataReading.AircraftModel1;
using FlightDataReading;
using System.Xml.Linq;
using System.IO;
using MongoDB.Driver;
using FlightDataEntities;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program1.Main1(args);



            ServiceReference1.AircraftServiceClient client =
                new ServiceReference1.AircraftServiceClient();
            var model = client.GetCurrentAircraftModel();

            var test2 = client.GetFlightData(
                new Flight()
                {
                    FlightID = "781102221",
                    Aircraft = new AircraftInstance() { AircraftModel = model, AircraftNumber = "0004", LastUsed = DateTime.Now },
                },
                    new string[] { "Et" }, 0, 100);

            LogHelper.Info("OK,good", null);

            log4net.ILog log = log4net.LogManager.GetLogger("AircraftDataAnalysis");
            //System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            log4net.ThreadContext.Properties["session"] = 21;


            // Log an info level message
            if (log.IsInfoEnabled) log.Info("Application [ConsoleApp] Start");

            // Log a debug message. Test if debug is enabled before
            // attempting to log the message. This is not required but
            // can make running without logging faster.
            if (log.IsDebugEnabled) log.Debug("This is a debug message");

            log.Error("Hey this is an error!");

            // Log an info level message
            if (log.IsInfoEnabled) log.Info("Application [ConsoleApp] End");

            Console.Write("Press Enter to exit...");
            Console.ReadLine();

            log4net.ILog logger = log4net.LogManager.GetLogger("FlightDataAnalysis");

            logger.Info("OK");

            logger = log4net.LogManager.GetLogger(typeof(Program));

            MongoServer server = MongoServer.Create("mongodb://sa:sa@42.96.198.241");

            server.Connect();
            var dbnames = server.GetDatabaseNames();
            foreach (var db in dbnames)
            {
                Console.WriteLine(db);
            }

            server.Disconnect();
            AircraftDataAnalysisWcfService.AircraftService serv = new AircraftDataAnalysisWcfService.AircraftService();

            serv.GetFlightData(new FlightDataEntities.Flight()
                {
                    FlightID = "781102221",
                    Aircraft =
                        new FlightDataEntities.AircraftInstance()
                        {
                            AircraftModel = serv.GetCurrentAircraftModel(),
                            AircraftNumber = "0004"
                        },
                    EndSecond = 7374,
                    FlightName = "781102221-1.phy"
                }, new string[] { "Et", "Hp" }, 0, 200);

            AircraftDataAnalysisWcfService.DataInputService service = new AircraftDataAnalysisWcfService.DataInputService();
            service.AddOneParameterValue(
                new FlightDataEntities.Flight()
                {
                    FlightID = "781102221",
                    Aircraft =
                        new FlightDataEntities.AircraftInstance()
                        {
                            AircraftModel = serv.GetCurrentAircraftModel(),
                            AircraftNumber = "0004"
                        },
                    EndSecond = 7374,
                    FlightName = "781102221-1.phy"
                },
                         "Vi", new FlightDataEntities.Level1FlightRecord[] { 
                             new FlightDataEntities.Level1FlightRecord(){ FlightID = "781102221",
                                 EndSecond = 8, ParameterID = "Vi", StartSecond = 0, Values = new float[]{1,2,3,4,5,6,7,8}}});

            //Program1.Main1(args);
            //Program1.Main2(args);
            //Program1.Main3(args);
            //return;
            //FlightDataEntities.FlightParameter[] parameters = ReadParameters(@"XMLFile1.xml");


            //string path = @"D:\home\09100223-1（右发空中超温）.phy";

            //FlightDataReading.FlightDataReadingHandler handler =
            //    new FlightDataReading.FlightDataReadingHandler(path);
            //handler.Definition = definition;

            //var header = handler.ReadHeader();

            //FormatOutput(header);

            //handler.Read();

            //FormatOutput(handler.Frames);

            //Dictionary<int, Dictionary<string, float[]>> valuesMap = new Dictionary<int, Dictionary<string, float[]>>();

            //for (int i = 0; i < handler.Frames.Count; i++)
            //{
            //    Dictionary<string, float[]> map = new Dictionary<string, float[]>();

            //    var frame = handler.Frames[i];

            //    var result = from one in frame.Segments
            //                 group one.Value by one.SegmentName into pid
            //                 select new { pid.Key, pid };

            //    foreach (var one in result)
            //    {
            //        List<float> v = new List<float>();
            //        foreach (var two in one.pid)
            //        {
            //            v.Add(Convert.ToSingle(two));
            //        }

            //        map.Add(one.Key, v.ToArray());
            //    }

            //    var notP = from one in parameters
            //               where !map.ContainsKey(one.ParameterID)
            //               select one;
            //    foreach (var p in notP)
            //    {
            //        map.Add(p.ParameterID, new float[] { });
            //    }

            //    valuesMap.Add(i, map);
            //}

            //FormatOutput(valuesMap, parameters);

            //FormatMaxMinValueOutput(valuesMap, parameters);

            //System.Console.Read();

            //handler.PreSetAircraftModelName = "A0004";
            //handler.MongoDBConnectionString = "mongodb://localhost/?w=1";

            //FlightDataReading.AircraftModel1.FlightRawDataWrapper wrapper = new FlightDataReading.AircraftModel1.FlightRawDataWrapper(path) { Definition = definition };
            //wrapper.Open();

            //FormatOutput(wrapper.Header);

        }

        private static void FormatMaxMinValueOutput(Dictionary<int, Dictionary<string, float[]>> valuesMap,
            FlightDataEntities.FlightParameter[] parameters)
        {
            var keys = from onekey in parameters
                       select onekey.ParameterID;

            Dictionary<string, MaxHelper> maxHelperMap = new Dictionary<string, MaxHelper>();
            Dictionary<string, MinHelper> minHelperMap = new Dictionary<string, MinHelper>();

            foreach (var key in (from k in valuesMap.Keys orderby k ascending select k).Take(valuesMap.Keys.Count - 1))
            {
                var oneSecMap = valuesMap[key];
                foreach (var one in oneSecMap.Keys)
                {
                    if (maxHelperMap.ContainsKey(one))
                    {
                        MaxHelper helper = maxHelperMap[one];
                        float max = oneSecMap[one].Max();
                        if (max > helper.Value)
                        {
                            helper.Value = max;
                            helper.Second = key;
                            maxHelperMap[one] = helper;
                        }
                    }
                    else
                    {
                        MaxHelper helper = new MaxHelper() { ParameterID = one, Second = key, Value = oneSecMap[one].Max() };
                        maxHelperMap.Add(one, helper);
                    }

                    if (minHelperMap.ContainsKey(one))
                    {
                        MinHelper helper = minHelperMap[one];
                        float min = oneSecMap[one].Min();
                        if (min < helper.Value)
                        {
                            helper.Value = min;
                            helper.Second = key;
                            minHelperMap[one] = helper;
                        }
                    }
                    else
                    {
                        MinHelper helper = new MinHelper() { ParameterID = one, Second = key, Value = oneSecMap[one].Min() };
                        minHelperMap.Add(one, helper);
                    }
                }
            }

            string headers = string.Empty;
            //foreach (string kk in keys)
            //{
            //    headers += kk;
            //headers += "\t";
            headers += "MaxValue";
            headers += "\t";
            headers += "Second";
            headers += "\t";
            headers += "MinValue";
            headers += "\t";
            headers += "Second";
            headers += "\t";
            //}

            System.Diagnostics.Debug.WriteLine(string.Empty);
            System.Diagnostics.Debug.WriteLine("MaxMin Value Report_____________________________________________________________________________________________________________________________________");
            System.Diagnostics.Debug.WriteLine(string.Empty);
            string header = string.Format("ParameterID\t{0}", headers);
            System.Diagnostics.Debug.WriteLine(header);

            foreach (string paramID in keys)
            {
                StringBuilder output = new StringBuilder();
                output.Append(paramID);
                output.Append("\t");

                var max = maxHelperMap[paramID];
                output.Append(max.Value);
                output.Append("\t");
                output.Append(max.Second);
                output.Append("\t");
                var min = minHelperMap[paramID];
                output.Append(min.Value);
                output.Append("\t");
                output.Append(min.Second);
                output.Append("\t");

                System.Diagnostics.Debug.WriteLine(output.ToString());
            }
        }

        class MaxHelper
        {
            public string ParameterID
            {
                get;
                set;
            }

            public int Second
            {
                get;
                set;
            }

            public float Value
            {
                get;
                set;
            }
        }

        class MinHelper
        {
            public string ParameterID
            {
                get;
                set;
            }

            public int Second
            {
                get;
                set;
            }

            public float Value
            {
                get;
                set;
            }
        }

        private static void FormatOutput(Dictionary<int, Dictionary<string, float[]>> valuesMap,
            FlightDataEntities.FlightParameter[] parameters)
        {
            var keys = from onekey in parameters
                       select onekey.ParameterID;

            string headers = string.Empty;
            foreach (string kk in keys)
            {
                headers += kk;
                headers += "\t";
            }

            System.Diagnostics.Debug.WriteLine(string.Empty);
            string header = string.Format("Secs\tIndex\t{0}", headers);
            System.Diagnostics.Debug.WriteLine(header);
            foreach (var key in (from k in valuesMap.Keys orderby k ascending select k))
            {
                string line = ToOneLine(key, valuesMap[key], keys);
                System.Diagnostics.Debug.Write(line);
            }
        }

        private static string ToOneLine(int sec, Dictionary<string, float[]> dictionary, IEnumerable<string> keys)
        {
            var val = ToOneSecondValues(sec, dictionary, keys);
            return string.Format(val, sec);
        }

        private static string ToOneSecondValues(int sec, Dictionary<string, float[]> dictionary, IEnumerable<string> keys)
        {
            StringBuilder builder = new StringBuilder();

            var lineMax = (from t in dictionary.Values
                           select t.Length).Max();

            List<StringBuilder> lines = new List<StringBuilder>();
            for (int j = 0; j < lineMax; j++)
            {
                lines.Add(new StringBuilder());
            }

            foreach (string k in keys)
            {
                for (int i = 0; i < lineMax; i++)
                {
                    StringBuilder line = lines[i];
                    if (dictionary[k].Length > i)
                        line.Append(dictionary[k][i]);
                    else
                        line.Append(string.Empty);
                    line.Append("\t");
                }
                //foreach (float f in dictionary[k])
                //{
                //    builder.Append(f);
                //    builder.Append(',');
                //}
                //builder.Append("\t");
            }

            for (int k = 1; k <= lines.Count; k++)
            {
                var line = lines[k - 1];
                //foreach (var line in lines)
                //{
                builder.Append(sec);
                builder.Append("\t");
                builder.Append(k);
                builder.Append("\t");
                builder.AppendLine(line.ToString());
            }

            return builder.ToString();
        }

        private static FlightDataEntities.FlightParameter[] ReadParameters(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                XDocument doc = XDocument.Load(reader);
                IEnumerable<XElement> elements = doc.Descendants("FlightParameter");

                var result = from one in elements
                             select new FlightDataEntities.FlightParameter() { ParameterID = one.Attribute("ParameterID").Value };

                return result.ToArray();
            }
        }

        //private static void FormatOutput(List<FlightDataContentFrame> list)
        //{
        //    StringBuilder b = new StringBuilder();
        //    //int i = 1;
        //    foreach (var frame in list)
        //    {
        //        foreach (var seg in frame.Segments)
        //        {
        //            b.Append(seg.Value);
        //            b.Append('\t');
        //        }
        //        b.AppendLine();
        //    }
        //    //Console.WriteLine(b.ToString());
        //    System.Diagnostics.Debug.WriteLine(b.ToString());
        //    System.Diagnostics.Debug.WriteLine("______________________________________________________________________________________________________________________________________________________");
        //    System.Diagnostics.Debug.WriteLine(string.Empty);
        //}

        //private static void FormatOutput(FlightDataReading.AircraftModel1.FlightDataHeader flightDataHeader)
        //{
        //    StringBuilder b = new StringBuilder();
        //    int i = 1;
        //    foreach (var seg in flightDataHeader.Segments)
        //    {
        //        b.Append(seg.Value);

        //        if (i % 8 == 0)
        //            b.AppendLine();
        //        else b.Append('\t');

        //        i++;
        //    }
        //    //Console.WriteLine(b.ToString());
        //    System.Diagnostics.Debug.WriteLine(b.ToString());
        //}
    }
}
