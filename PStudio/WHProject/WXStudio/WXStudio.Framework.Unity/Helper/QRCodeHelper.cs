using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace WXStudio.Framework.Unity.Helper
{
    public class QRCodeHelper
    {

        string ImgPath = AppDomain.CurrentDomain.BaseDirectory + "QRCode";

        private Bitmap CreatQRCode(string qrCodeContent)
        {
            Bitmap bt;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE; //  support different mode
            qrCodeEncoder.QRCodeScale = 10;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

            //动态调整二维码版本号,上限40，过长返回空白图片，编码后字符最大字节长度2953
            while (true)
            {
                try
                {
                    bt = qrCodeEncoder.Encode(qrCodeContent, System.Text.Encoding.UTF8);
                    break;
                }
                catch (IndexOutOfRangeException e)
                {
                    if (qrCodeEncoder.QRCodeVersion < 40)
                    {
                        qrCodeEncoder.QRCodeVersion++;
                    }
                    else
                    {
                        bt = new Bitmap(100, 100);
                        break;
                    }
                }
            }

            return bt;
        }

        public string GetQRCode(string qrCodeContent, string path, string fileName)
        {
            Bitmap bt = CreatQRCode(qrCodeContent);
            string fullFilePath = Path.Combine(path, fileName);
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                bt.Save(fullFilePath);
                return fullFilePath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetQRCode(string qrCodeContent, string fileName)
        {
            return GetQRCode(qrCodeContent, ImgPath, fileName);
        }





    }
}
