using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class PHYHelper
    {
        public const int PARAM_LENGTH = 80;

        public static PHYHeader ReadPHYHeader(BinaryReader reader)
        {
            PHYHeader header = new PHYHeader();

            reader.BaseStream.Position = 0;

            #region 1.1文件头
            header.OPName = BaseFunction.ReadChar(reader, 20);//操作员
            header.BTime = Convert.ToDateTime(BaseFunction.ReadChar(reader, 20));//文件生成时间,产生本工程值数据文件的时间。如：“2006-05-01 12:12:12”
            header.FlyPlanAddr = reader.ReadInt32();//机型机号信息表的偏移首地址
            header.ParaListAddr = reader.ReadInt32();//工程值参数表的偏移首地址,固定为256
            header.PhyValueAddr = reader.ReadInt32();//数据流格式表的偏移首地址
            #endregion

            #region 1.2 机型机号信息表
            reader.BaseStream.Position = header.FlyPlanAddr;
            header.AircrfName = BaseFunction.ReadChar(reader, 8);//飞机机型
            header.AircrfNum = BaseFunction.ReadChar(reader, 12);//飞机机号
            header.StartTime = Convert.ToDateTime(BaseFunction.ReadChar(reader, 20));//起飞时间
            header.EndTime = Convert.ToDateTime(BaseFunction.ReadChar(reader, 20));//结束时间
            #endregion

            #region 1.3工程值参数表
            reader.BaseStream.Position = header.ParaListAddr;

            header.PNum = reader.ReadUInt16();//组参数数目
            header.SWNum = reader.ReadUInt16();//单开关量参数数目
            #endregion

            #region 1.4 获取飞参和GPS时间误差
            int fp_startTime = BaseFunction.DateTimeToMSecond(header.StartTime);//飞参记录初始毫秒时间

            header.FlySeconds = (header.EndTime.Hour * 60 + header.EndTime.Minute) * 60 + header.EndTime.Second -
                            (header.StartTime.Hour * 60 + header.StartTime.Minute) * 60 - header.StartTime.Second;//飞参记录时间长度（秒）

            float time1 = 0, time2 = 0;
            int totalCount = 0;
            float[] fp10data = new float[10];//计算过程值
            int errorTime;//飞参和GPS的误差时间（毫秒）
            bool booleen1 = true;
            bool booleen2 = ((header.FlySeconds - totalCount) > 10);

            while (booleen1 && booleen2)
            {
                for (int m = 0; m < 10; m++)
                {
                    reader.BaseStream.Position = header.PhyValueAddr + totalCount * 80 * header.PNum + 93 * 80;
                    fp10data[m] = reader.ReadSingle();
                    totalCount++;
                }
                time1 = fp10data[9] - fp10data[0];
                time2 = fp10data[7] - fp10data[3];
                booleen1 = !((time1 == 9000) && (time2 == 4000));
                booleen2 = ((header.FlySeconds - totalCount) > 10);
            }

            if (booleen2 == false)
                throw new PHYException();
            else
            {
                reader.BaseStream.Position = header.PhyValueAddr + totalCount * 80 * header.PNum + 92 * 80;
                float timeH = reader.ReadSingle();
                reader.BaseStream.Position = header.PhyValueAddr + totalCount * 80 * header.PNum + 93 * 80;
                float timeL = reader.ReadSingle();
                int GPS_time = BaseFunction.GPSToMSecond(timeH, timeL);//GPS时间（秒）
                int fp_time = fp_startTime + totalCount * 1000;
                errorTime = fp_time - GPS_time;
            }

            //GPS初始时间
            header.GPSStartTime = fp_startTime - errorTime;

            #endregion

            #region 1.5 截头去尾
            #endregion
            float T1 = 0, T2 = 0;
            int timeCount = 0;
            int timeCountEnd = 0;
            bool isEnd = false;

            while (T1 == T2)
            {
                reader.BaseStream.Position = header.PhyValueAddr + timeCount * 80 * header.PNum + 64 * 80;
                T1 = reader.ReadSingle();
                timeCount++;
                reader.BaseStream.Position = header.PhyValueAddr + timeCount * 80 * header.PNum + 64 * 80;
                T2 = reader.ReadSingle();
            }

            header.PhyValueAddr += (timeCount - 1) * 80 * header.PNum;
            header.GPSStartTime += (timeCount - 1) * 1000;

            while (!isEnd)
            {
                reader.BaseStream.Position = header.PhyValueAddr + timeCountEnd * 80 * header.PNum + 64 * 80;
                T1 = reader.ReadSingle();
                timeCountEnd++;
                reader.BaseStream.Position = header.PhyValueAddr + timeCountEnd * 80 * header.PNum + 64 * 80;
                T2 = reader.ReadSingle();
                if (T1 == T2)
                    isEnd = true;
                else
                    isEnd = false;
            }

            header.PhyValueEndAddr = header.PhyValueAddr + timeCountEnd * 80 * header.PNum;
            header.FlySeconds = timeCountEnd;

            //GPS结束时间
            header.GPSEndTime = header.GPSStartTime + header.FlySeconds * 1000;

            return header;
        }
        /// <summary>
        /// 读取某一秒的一个飞参数据
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="second">第几秒</param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static float[] ReadFlyParameter(BinaryReader reader, int second, PHYHeader header, FlyParameter parameter)
        {
            //设置要读取的飞参的起始位置
            reader.BaseStream.Position = header.PhyValueAddr + (second - 1) * PARAM_LENGTH * header.PNum + (parameter.Index - 1) * PARAM_LENGTH;

            float[] values = new float[parameter.Frequence];
            for (int i = 0; i < parameter.Frequence; i++)
            {
                if (parameter.SubIndex == -1)
                    values[i] = reader.ReadSingle();
                else
                    values[i] = Convert.ToSingle(BaseFunction.GetBit((int)reader.ReadSingle(), parameter.SubIndex));
            }
            return values;
        }

        public static int GetFlyParamSeconds(PHYHeader header)
        {
            return (int)(header.PhyValueEndAddr - header.PhyValueAddr) / (header.PNum * PARAM_LENGTH);
        }
    }
}
