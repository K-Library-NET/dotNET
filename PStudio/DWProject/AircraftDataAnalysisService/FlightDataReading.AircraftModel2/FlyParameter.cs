using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class FlyParameter
    {
        public const int COUNT_PERSECOND = 1;

        public FlyParameter()
        {
            SubIndex = -1;
        }

        public int Index { get; set; }
        public int SubIndex { get; set; }
        public string Caption { get; set; }
        public int Frequence { get; set; }
        public string Unit { get; set; }
        public string ID
        {
            get { return string.Format("{0}_{1}", this.Index, this.SubIndex); }
        }

        private static Dictionary<string, FlyParameter> mParameters;

        public static Dictionary<string, FlyParameter> Parameters
        {
            get
            {
                return mParameters;
            }
            set
            {
                mParameters = value;
            }
        }

        private static Dictionary<string, FlyParameter> mCaptionParameters;

        public static Dictionary<string, FlyParameter> CaptionParameters
        {
            get
            {
                return mCaptionParameters;
            }
            set
            {
                mCaptionParameters = value;
            }
        }

        //20130926 需要服务端初始化CaptionParameters和Parameters传过来
        //private static void InitParameterDict()
        //{
        //    mParameters = new Dictionary<string, FlyParameter>();
        //    mCaptionParameters = new Dictionary<string, FlyParameter>();

        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(Application.StartupPath + "\\FlyParameter.xml");
        //    XmlNodeList nodes = doc.DocumentElement.SelectNodes("Parameter");
        //    foreach (XmlNode n in nodes)
        //    {
        //        FlyParameter fp = new FlyParameter();
        //        fp.Index = Convert.ToInt32(n.Attributes["Index"].Value);
        //        fp.SubIndex = Convert.ToInt32(n.Attributes["SubIndex"].Value);
        //        fp.Unit = n.Attributes["Unit"].Value;
        //        if (fp.Unit == "")
        //        {
        //            fp.Caption = n.Attributes["Caption"].Value;
        //        }
        //        else
        //            fp.Caption = n.Attributes["Caption"].Value + "(" + fp.Unit + ")";
        //        fp.Frequence = Convert.ToInt32(n.Attributes["Frequence"].Value);
        //        mParameters.Add(fp.ID, fp);
        //        mCaptionParameters.Add(fp.Caption, fp);
        //    }
        //}

        /// <summary>
        /// 根据频率和每秒数据个数重新生成数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="freq"></param>
        /// <returns></returns>
        public static List<float> GetData4Frequence(List<float> data, int freq)
        {
            if (freq == COUNT_PERSECOND) return data;

            List<float> newData = new List<float>();
            if (freq > COUNT_PERSECOND)
            {
                int j = 0, step = freq / COUNT_PERSECOND;
                while (j < data.Count)
                {
                    int index = j * step;
                    if (index >= data.Count) break;

                    newData.Add(data[index]);
                    j++;
                }

            }
            else
            {
                foreach (float f in data)
                {
                    for (int i = 0; i < COUNT_PERSECOND; i++)
                    {
                        newData.Add(f);
                    }
                }
            }
            return newData;
        }

        //public static DataTable CreateTable()
        //{
        //    //构造记录集的列头
        //    DataTable dt = new DataTable();
        //    DataColumn dc = new DataColumn("时间");
        //    dt.Columns.Add(dc);
        //    dc = new DataColumn("秒值");
        //    dt.Columns.Add(dc);
        //    foreach (KeyValuePair<string, FlyParameter> fp in FlyParameter.Parameters)
        //    {
        //        dc = new DataColumn(fp.Value.Caption, typeof(float));
        //        dt.Columns.Add(dc);
        //    }
        //    return dt;
        //}

        public override string ToString()
        {
            return string.Format("{0}", this.Caption);
        }
    }
}
