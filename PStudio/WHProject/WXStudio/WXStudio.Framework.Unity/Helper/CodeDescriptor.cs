using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace WXStudio.Framework.Unity.Helper
{
    public class CodeDescriptor
    {
        public ErrorCorrectionLevel Ecl;
        public string Content;
        public QuietZoneModules QuietZones;
        public int ModuleSize;
        public BitMatrix Matrix;
        public string ContentType;

        /// <summary>
        /// Parse QueryString that define the QR code properties
        /// </summary>
        /// <param name="request">HttpRequest containing HTTP GET data</param>
        /// <returns>A QR code descriptor object</returns>
        public static CodeDescriptor Init(ErrorCorrectionLevel level, string content, QuietZoneModules qzModules, int moduleSize)
        {
            var cp = new CodeDescriptor();

            //// Error correction level
            cp.Ecl = level;
            //// Code content to encode
            cp.Content = content;
            //// Size of the quiet zone
            cp.QuietZones = qzModules;
            //// Module size
            cp.ModuleSize = moduleSize;
            return cp;
        }

        /// <summary>
        /// Encode the content with desired parameters and save the generated Matrix
        /// </summary>
        /// <returns>True if the encoding succeeded, false if the content is empty or too large to fit in a QR code</returns>
        public bool TryEncode()
        {
            var encoder = new QrEncoder(Ecl);
            QrCode qr;
            if (!encoder.TryEncode(Content, out qr))
                return false;

            Matrix = qr.Matrix;
            return true;
        }

        /// <summary>
        /// Render the Matrix as a PNG image
        /// </summary>
        /// <param name="ms">MemoryStream to store the image bytes into</param>
        public void Render(MemoryStream ms)
        {
            var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones));
            render.WriteToStream(Matrix, System.Drawing.Imaging.ImageFormat.Png, ms);
            ContentType = "image/png";
        }
    }


    //示例
    //string content = string.IsNullOrEmpty(textBox1.Text.Trim()) ? "http://www.nnbh.cn" : textBox1.Text.Trim();
    //var codeParams = CodeDescriptor.Init(ErrorCorrectionLevel.H, content, QuietZoneModules.Two, 5);
    //codeParams.TryEncode();
    //// Render the QR code as an image
    //using (var ms = new MemoryStream())
    //{
    // codeParams.Render(ms);
    //Image image = Image.FromStream(ms);
    // pictureBox1.Image = image;
    //if (image != null)
    //pictureBox1.SizeMode = image.Height > pictureBox1.Height ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.Normal;
    //        }
}
