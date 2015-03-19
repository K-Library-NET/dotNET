using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class BaseFunction
    {
        #region  牛顿插值算法
        /// <summary>
        /// 牛顿插值算法
        /// </summary>
        /// <param name="sourceNumber">存放有效插值节点,n+1点数组</param>
        /// <param name="sourceArray">存放有效插值节点上的函数值,n+1点数组</param>
        /// <returns>指定插值点函数值，10点数组</returns>
        public static float[] NEWTON(float[] sourceNumber, float[] sourceArray)
        {
            int k, i;
            int n = sourceNumber.Length - 1;
            float[] f = new float[n + 1];
            float[] pointArray = new float[10];
            float[] pointNumber = { 0, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f };

            pointArray[0] = sourceArray[0];

            for (k = 1; k <= n; k++)
            {
                f[0] = sourceArray[k];
                for (i = 0; i < k; i++)
                    f[i + 1] = (f[i] - sourceArray[i]) / (sourceNumber[k] - sourceNumber[i]);
                sourceArray[k] = f[k];
            }

            for (k = 1; k < 10; k++)
            {
                pointArray[k] = sourceArray[n];
                for (i = n - 1; i >= 0; i--)
                    pointArray[k] = pointArray[k] * (pointNumber[k] - sourceNumber[i]) + sourceArray[i];
            }

            return pointArray;
        }
        #endregion

        #region  浮点数组插值
        /// <summary>
        /// 浮点数组插值
        /// </summary>
        /// <param name="sourceArray">需被插值的浮点数组(20Hz,21点)</param>
        /// <param name="number">需被插值的参数的采样率</param>
        /// <returns>插值后的浮点数组目标采样率（10Hz）</returns>
        public static float[] InsertValue(float[] sourceArray, int number)
        {
            float[] objectArray = new float[10];
            int i;
            switch (number)
            {
                case 1:
                    objectArray[0] = sourceArray[0];
                    for (int j = 1; j < 10; j++)
                    {
                        objectArray[j] = sourceArray[0] + ((sourceArray[20] - sourceArray[0]) * j / 10);
                    }
                    break;
                case 2:
                    float[] source2Array = { sourceArray[0], sourceArray[1], sourceArray[20] };
                    float[] source2Number = { 0, 0.5f, 1 };
                    objectArray = NEWTON(source2Number, source2Array);
                    break;
                case 4:
                    float[] source4Array = { sourceArray[0], sourceArray[1], sourceArray[2], sourceArray[3], sourceArray[20] };
                    float[] source4Number = { 0, 0.25f, 0.5f, 0.75f, 1 };
                    objectArray = NEWTON(source4Number, source4Array);
                    break;
                case 8:
                    float[] source8Array = { sourceArray[0], sourceArray[1], sourceArray[2], sourceArray[3], sourceArray[4], 
                                               sourceArray[5], sourceArray[6], sourceArray[7], sourceArray[20] };
                    float[] source8Number = { 0, 0.125f, 0.25f, 0.375f, 0.5f, 0.625f, 0.75f, 0.875f, 1 };
                    objectArray = NEWTON(source8Number, source8Array);
                    break;
                case 10:
                    for (i = 0; i < 10; i++)
                    {
                        objectArray[i] = sourceArray[i];
                    }
                    break;
                case 16:
                    float[] source16Array = { sourceArray[0], sourceArray[1], sourceArray[2], sourceArray[3], sourceArray[4], 
                                               sourceArray[5], sourceArray[6], sourceArray[7],sourceArray[8], sourceArray[9], sourceArray[10], 
                                               sourceArray[11], sourceArray[12],sourceArray[13], sourceArray[14], sourceArray[15],  sourceArray[20] };
                    float[] source16Number = { 0, 0.0625f, 0.125f, 0.1875f, 0.25f, 0.3125f, 0.375f, 0.4375f, 0.5f, 0.5625f, 
                                                 0.625f, 0.6875f, 0.75f, 0.8125f, 0.875f, 0.9375f, 1 };
                    objectArray = NEWTON(source16Number, source16Array);
                    break;
                case 20:
                    for (i = 0; i < 10; i++)
                    {
                        objectArray[i] = sourceArray[2 * i];
                    }
                    break;
            }
            return objectArray;
        }
        #endregion

        #region  二进制文件读字符数组
        /// <summary>
        /// 二进制文件读字符数组
        /// </summary>
        /// <param name="binReader">BinaryReader对象</param>
        /// <param name="charArray">读取的字符长度</param>
        /// <returns></returns>
        public static string ReadChar(BinaryReader binReader, int length)
        {
            byte[] bytes = binReader.ReadBytes(length);

            char[] chars = Encoding.GetEncoding("ASCII").GetChars(bytes);

            return CharArrayToString(chars);
        }
        #endregion

        #region  字符数组转变成字符串，同时除 '\0'截尾
        /// <summary>
        /// 字符数组转变成字符串，同时除 '\0'截尾
        /// </summary>
        /// <param name="charArray">被转换的字符数组</param>
        /// <returns>转换后获得的字符串</returns>
        public static string CharArrayToString(char[] charArray)
        {
            StringBuilder strBuider = new StringBuilder();
            foreach (char c in charArray)
            {
                if (c != '\0')
                {
                    strBuider.Append(c);
                }
            }
            return strBuider.ToString();
        }
        #endregion

        #region  DateTime对象转换为毫秒
        /// <summary>
        /// DateTime对象转换为毫秒
        /// </summary>
        /// <param name="datetime">被转换的DateTime对象</param>
        /// <returns>转换后获得的毫秒数</returns>
        public static int DateTimeToMSecond(DateTime datetime)
        {
            return (datetime.Hour * 3600 + datetime.Minute * 60 + datetime.Second) * 1000 + datetime.Millisecond;
        }
        #endregion

        #region  毫秒转换为时间字符串
        /// <summary>
        /// 毫秒转换为时间字符串
        /// </summary>
        /// <param name="msecond">被转换的毫秒数</param>
        /// <returns>转换后获得的时间字符串</returns>
        public static string MSecondToTimeString(int msecond)
        {
            int hour, minute, second;
            string hourstr, minutestr, secondstr, msecondstr;

            hour = msecond / (1000 * 60 * 60);
            if (hour < 10)
                hourstr = "0" + hour.ToString();
            else hourstr = hour.ToString();

            minute = (msecond % (1000 * 60 * 60)) / (1000 * 60);
            if (minute < 10)
                minutestr = "0" + minute.ToString();
            else minutestr = minute.ToString();

            second = msecond % (1000 * 60) / 1000;
            if (second < 10)
                secondstr = "0" + second.ToString();
            else secondstr = second.ToString();

            msecond = msecond % 1000;
            if (msecond < 10)
                msecondstr = "00" + msecond.ToString();
            else if (msecond < 100)
                msecondstr = "0" + msecond.ToString();
            else msecondstr = msecond.ToString();

            return hourstr + ":" + minutestr + ":" + secondstr + ":" + msecondstr;
        }
        #endregion

        #region  毫秒转换为时间字符串
        /// <summary>
        /// 毫秒转换为时间字符串
        /// </summary>
        /// <param name="msecond">被转换的毫秒数</param>
        /// <returns>转换后获得的时间字符串</returns>
        public static string MSecondToTimeStringNew(int msecond)
        {
            int hour, minute, second;
            string hourstr, minutestr, secondstr, msecondstr;

            hour = msecond / (1000 * 60 * 60);
            if (hour < 10)
                hourstr = "0" + hour.ToString();
            else hourstr = hour.ToString();

            minute = (msecond % (1000 * 60 * 60)) / (1000 * 60);
            if (minute < 10)
                minutestr = "0" + minute.ToString();
            else minutestr = minute.ToString();

            second = msecond % (1000 * 60) / 1000;
            if (second < 10)
                secondstr = "0" + second.ToString();
            else secondstr = second.ToString();

            msecond = msecond % 1000;
            if (msecond < 10)
                msecondstr = "00" + msecond.ToString();
            else if (msecond < 100)
                msecondstr = "0" + msecond.ToString();
            else msecondstr = msecond.ToString();

            return hourstr + ":" + minutestr + ":" + secondstr;
        }
        #endregion

        #region  GPS记录时间转换为秒值
        /// <summary>
        /// GPS记录时间转换为秒值
        /// </summary>
        /// <param name="GPSTh">被转换的GPS时间高字节</param>
        /// <param name="GPSTl">被转换的GPS时间低字节</param>
        /// <returns>转换后获得的秒值</returns>
        public static float GPSToSecond(float GPSTh, float GPSTl)
        {
            float msecond;
            msecond = GPSTh * 65536 + GPSTl;
            if (msecond > 8 * 3600000)
                msecond -= 8 * 3600000;
            else msecond += 16 * 3600000;
            return msecond / 1000;
        }
        #endregion

        #region  GPS记录时间转换为毫秒值
        /// <summary>
        /// GPS记录时间转换为毫秒值
        /// </summary>
        /// <param name="GPSTh">被转换的GPS时间高字节</param>
        /// <param name="GPSTl">被转换的GPS时间低字节</param>
        /// <returns>转换后获得的毫秒值</returns>
        public static int GPSToMSecond(float GPSTh, float GPSTl)
        {
            float msecond;
            msecond = GPSTh * 65536 + GPSTl;
            if (msecond > 8 * 3600000)
                msecond -= 8 * 3600000;
            else msecond += 16 * 3600000;
            return (int)msecond;
        }
        #endregion

        #region  获得4个字节（int）低16位中第n位的二进制值（0，1），int对象的第17位n=1，第18位n=2，...
        /// <summary>
        /// 获得4个字节（int）低16位中第n位的二进制值
        /// </summary>
        /// <param name="m">int对象</param>
        /// <param name="n">获取的二进制值在int对象中的位数</param>
        /// <returns>转换后获得的位二进制值（0或1）</returns>
        public static int GetBit(int m, int n)
        {
            int i = 0;
            switch (n)
            {
                case 20:
                    i = m & 1048576;
                    break;
                case 19:
                    i = m & 524288;
                    break;
                case 18:
                    i = m & 262144;
                    break;
                case 17:
                    i = m & 131072;
                    break;
                case 16:
                    i = m & 65536;
                    break;
                case 15:
                    i = m & 32768;
                    break;
                case 14:
                    i = m & 16384;
                    break;
                case 13:
                    i = m & 8192;
                    break;
                case 12:
                    i = m & 4096;
                    break;
                case 11:
                    i = m & 2048;
                    break;
                case 10:
                    i = m & 1024;
                    break;
                case 9:
                    i = m & 512;
                    break;
                case 8:
                    i = m & 256;
                    break;
                case 7:
                    i = m & 128;
                    break;
                case 6:
                    i = m & 64;
                    break;
                case 5:
                    i = m & 32;
                    break;
                case 4:
                    i = m & 16;
                    break;
                case 3:
                    i = m & 8;
                    break;
                case 2:
                    i = m & 4;
                    break;
                case 1:
                    i = m & 2;
                    break;
                case 0:
                    i = m & 1;
                    break;
            }

            if (i == 0)
                return 0;
            else return 1;
        }
        #endregion

        #region  单开关量插值
        /// <summary>
        /// 单开关量插值
        /// </summary>
        /// <param name="sourceBit">需被插值的开关量数组</param>
        /// <param name="n">单开关量插值前的采样率</param>
        /// <returns>插值后的开关量数组，10Hz</returns>
        public static int[] InsertBit(int[] sourceBit, int n)
        {
            int[] pointBit = new int[10];
            switch (n)
            {
                case 2:
                    for (int i = 0; i < 10; i++)
                    {
                        if (i < 5)
                            pointBit[i] = sourceBit[0];
                        else pointBit[i] = sourceBit[1];
                    }
                    break;
                case 4:
                    for (int i = 0; i < 10; i++)
                    {
                        if (i < 3)
                            pointBit[i] = sourceBit[0];
                        else if (i < 5) pointBit[i] = sourceBit[1];
                        else if (i < 8) pointBit[i] = sourceBit[2];
                        else pointBit[i] = sourceBit[3];
                    }
                    break;
                case 8:
                    for (int i = 0; i < 10; i++)
                    {
                        if (i < 2)
                            pointBit[i] = sourceBit[0];
                        else if (i < 3) pointBit[i] = sourceBit[1];
                        else if (i < 4) pointBit[i] = sourceBit[2];
                        else if (i < 5) pointBit[i] = sourceBit[3];
                        else if (i < 7) pointBit[i] = sourceBit[4];
                        else if (i < 8) pointBit[i] = sourceBit[5];
                        else if (i < 9) pointBit[i] = sourceBit[6];
                        else pointBit[i] = sourceBit[7];
                    }
                    break;
                case 16:
                    {
                        pointBit[0] = sourceBit[0];
                        pointBit[1] = sourceBit[2];
                        pointBit[2] = sourceBit[3];
                        pointBit[3] = sourceBit[5];
                        pointBit[4] = sourceBit[6];
                        pointBit[5] = sourceBit[8];
                        pointBit[6] = sourceBit[10];
                        pointBit[7] = sourceBit[11];
                        pointBit[8] = sourceBit[13];
                        pointBit[9] = sourceBit[14];
                    }
                    break;
                case 20:
                    for (int i = 0; i < 10; i++)
                    {
                        pointBit[i] = sourceBit[2 * i];
                    }
                    break;
            }
            return pointBit;
        }
        #endregion
    }
}
