using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.AircraftModel1
{
    class FlightBinaryDataParser
    {
        internal static FlightDataHeader Parse(byte[] bytes,
            FlightBinaryDataHeaderDefinition definition)
        {
            FlightDataHeader header = new FlightDataHeader();
            string testVal = new string(System.Text.Encoding.GetEncoding("ASCII").GetChars(bytes));
            System.Diagnostics.Debug.WriteLine(testVal);
            List<FlightDataContentSegment> segments = new List<FlightDataContentSegment>();

            int current = 0;

            foreach (var seg in definition.Segments)
            {
                if (seg.SegmentName == "(NULL)")
                {
                    current = seg.BytesCount;
                    continue;
                }

                int step = seg.BytesCount;
                StringBuilder builder = new StringBuilder();
                List<byte> bts = new List<byte>();
                for (int i = current; i < current + step; i++)
                {
                    if (i >= bytes.Length)
                        break;
                    bts.Add(bytes[i]);
                    //builder.Append(bytes[i]);
                }

                if (bts.Count > 0)
                //builder.Length > 0)
                {
                    FlightDataContentSegment ds = new FlightDataContentSegment();
                    if (seg.DataTypeStr == DataTypeConverter.LONG)
                    {
                    }
                    else if (seg.DataTypeStr == DataTypeConverter.FLOAT)
                    {
                    }
                    else if (seg.DataTypeStr == DataTypeConverter.INT32)
                    {

                        string v = new string(System.Text.Encoding.GetEncoding("ASCII").GetChars(bts.ToArray()));
                        builder.Append(v);
                        // FlightDataContentSegment ds = new FlightDataContentSegment();
                        ds.DataTypeStr = seg.DataTypeStr;
                        ds.SegmentName = seg.SegmentName;
                        ds.Value = 0;//debug //builder.ToString().Trim('\0');
                    }
                    else
                    {
                        string v = new string(System.Text.Encoding.GetEncoding("ASCII").GetChars(bts.ToArray()));
                        builder.Append(v);
                        ds.DataTypeStr = seg.DataTypeStr;
                        ds.SegmentName = seg.SegmentName;
                        ds.Value = 0;//debug builder.ToString().Trim('\0');
                    }
                    segments.Add(ds);
                }

                current += step;
            }

            return header;
        }
    }
}
