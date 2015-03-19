using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FlightDataReading.AircraftModel1
{
    public class FlightRawDataExtractorFactory
    {
        public static IFlightRawDataExtractor CreateFlightRawDataExtractor(StorageFile file, FlightParameters parameters)
        {
            if (file == null)
                return null;

            var readStreamTask = file.OpenStreamForReadAsync();
            readStreamTask.Wait();
            MemoryStream stream = new MemoryStream(102400);
            byte[] bytes = new byte[readStreamTask.Result.Length];
            readStreamTask.Result.Read(bytes, 0, Convert.ToInt32(readStreamTask.Result.Length));

            stream.Write(bytes, 0, Convert.ToInt32(readStreamTask.Result.Length));

            //Task temp1 = readStreamTask.AsTask();
            //temp1.Wait();
            //var temp2 = readStreamTask.GetResults();
            BinaryReader reader = new BinaryReader(stream); //temp2.AsStreamForRead(1024000)); //readStreamTask.Result);

            var handler = new FlightDataReadingHandler(reader);

            //if (parameters != null)
            //{
            //    handler.Definition = CreateDefinition(handler.Definition, parameters);
            //}
            return handler;
        }

        //DEBUG : 写死机型1，暂时没办法
        private static FlightBinaryDataDefinition CreateDefinition(
            FlightBinaryDataDefinition flightBinaryDataDefinition, FlightParameters parameters)
        {
            FlightBinaryDataDefinition definition = new FlightBinaryDataDefinition()
            {
                AircraftModel = flightBinaryDataDefinition.AircraftModel,
                HeaderDefinition = flightBinaryDataDefinition.HeaderDefinition,

                FrameDefinition = new FlightBinaryDataContentFrameDefinition()
                {
                    BytesCount = parameters.BytesCount
                }
            };

            System.Collections.Generic.List<FlightBinaryDataContentSegmentDefinition> defs
            = new System.Collections.Generic.List<FlightBinaryDataContentSegmentDefinition>();

            Dictionary<int, List<FlightParameter>> tempList1 = new Dictionary<int, List<FlightParameter>>();

            foreach (var temp1 in parameters.Parameters)
            {
                if (tempList1.ContainsKey(temp1.Index))
                {
                    tempList1[temp1.Index].Add(temp1);
                }
                else
                {
                    tempList1.Add(temp1.Index, new List<FlightParameter>());
                    tempList1[temp1.Index].Add(temp1);
                }
            }

            var temp2 = from k in tempList1
                        orderby k.Key ascending
                        select k;

            foreach (var p in temp2)
            {
                if (p.Value.Count < 2)
                {
                    foreach (var p2 in p.Value)
                    {
                        FlightBinaryDataContentSegmentDefinition def = new FlightBinaryDataContentSegmentDefinition()
                        {
                            BytesCount = 4,
                            DataTypeStr = p2.ParameterDataType,
                            SegmentName = p2.ParameterID
                        };
                        defs.Add(def);
                    }
                }
                else
                {
                    var list = from o in p.Value
                               orderby o.SubIndex
                               select
                      new FlightBitDataContentSegmentDefinition()
                         {
                             BitsCount = 1,
                             BytesCount = -1,
                             DataTypeStr = o.ParameterDataType,
                             SegmentName = o.ParameterID
                         };

                    FlightBinaryDataContentSegmentDefinition def = new FlightBinaryDataContentSegmentDefinition()
                    {
                        BytesCount = 4,
                        DataTypeStr = list.First().DataTypeStr,
                        SegmentName = list.First().SegmentName
                    };
                    def.BitsDefinition = list.ToArray();
                    defs.Add(def);
                }
            }

            definition.FrameDefinition.Segments = defs.ToArray();

            return definition;
        }
    }
}
