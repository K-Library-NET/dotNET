using Gecko;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MobileMarketDemo.Win
{
    public partial class DlgLoading : Form
    {
        public DlgLoading()
        {
            InitializeComponent();

            this.Load += DlgLoading_Load;
            this.FormClosing += DlgLoading_FormClosing;
        }

        void DlgLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_selfClose == false)
            {
                DialogResult result = MessageBox.Show("正在检查组件注册状态，确定要退出程序？", "关闭程序", MessageBoxButtons.OKCancel);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
                else e.Cancel = true;
            }
        }

        private BackgroundWorker m_worker = null;

        void DlgLoading_Load(object sender, EventArgs e)
        {
            m_worker = new BackgroundWorker();
            m_worker.WorkerReportsProgress = true;
            m_worker.DoWork += m_worker_DoWork;
            m_worker.RunWorkerCompleted += m_worker_RunWorkerCompleted;
            m_worker.ProgressChanged += m_worker_ProgressChanged;
            m_worker.RunWorkerAsync();
            //throw new NotImplementedException();
        }

        void m_worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Invoke(new EventHandler(delegate(object o, EventArgs args)
            {
                this.progressBar1.Value = e.ProgressPercentage;
            }));
            //throw new NotImplementedException();
        }

        private bool m_selfClose = false;

        void m_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string location = GetXULRunnerLocation();
            if (!string.IsNullOrEmpty(location))
            {
                Xpcom.Initialize(location);
                this.Invoke(new EventHandler(delegate(object o, EventArgs arg)
                {
                    this.progressBar1.Value = 100;
                }));
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.m_selfClose = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("找不到浏览器内核文件，点击“确定”退出。");
                Application.Exit();
            }
        }

        void m_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string xulrunnerLocation = GetXULRunnerLocation();
            if (!string.IsNullOrEmpty(xulrunnerLocation))
                return;

            this.Invoke(new EventHandler(delegate(object o, EventArgs arg)
            {
                this.m_worker.ReportProgress(10);
            }));

            string dir = System.IO.Path.Combine(System.IO.Path.Combine(
                System.IO.Path.GetTempPath(), "GeckoFx"), "Gecko18");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            this.Invoke(new EventHandler(delegate(object o, EventArgs arg)
            {
                this.m_worker.ReportProgress(15);
            }));

            FileCompression.Decompress(GeckoFxWinForm.StaticResource.Gecko18, dir);

            this.Invoke(new EventHandler(delegate(object o, EventArgs arg)
            {
                this.m_worker.ReportProgress(85);
            }));

        }

        private static string GetXULRunnerLocation()
        {
            if (Directory.Exists("xulrunner"))
                return "xulrunner";

            string dir = System.IO.Path.Combine(System.IO.Path.Combine(
                System.IO.Path.GetTempPath(), "GeckoFx"), "Gecko18");

            DirectoryInfo info = new DirectoryInfo(dir);
            if (info.Exists)
            {
                var files = info.GetFiles("xpcom.dll");
                if (files != null && files.Length > 0)
                    return dir;
            }

            return string.Empty;
        }
    }

    /// <summary>
    /// 文件(夹)压缩、解压缩
    /// </summary>
    public abstract class FileCompression
    {
        #region 压缩文件
        /// <summary>  
        /// 压缩文件  
        /// </summary>  
        /// <param name="fileNames">要打包的文件列表</param>  
        /// <param name="GzipFileName">目标文件名</param>  
        /// <param name="CompressionLevel">压缩品质级别（0~9）</param>  
        /// <param name="deleteFile">是否删除原文件</param>
        public static void CompressFile(List<FileInfo> fileNames, string GzipFileName, int CompressionLevel, bool deleteFile)
        {
            ZipOutputStream s = new ZipOutputStream(File.Create(GzipFileName));
            try
            {
                s.SetLevel(CompressionLevel);   //0 - store only to 9 - means best compression  
                foreach (FileInfo file in fileNames)
                {
                    FileStream fs = null;
                    try
                    {
                        fs = file.Open(FileMode.Open, FileAccess.ReadWrite);
                    }
                    catch
                    { continue; }
                    //  方法二，将文件分批读入缓冲区  
                    byte[] data = new byte[2048];
                    int size = 2048;
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file.Name));
                    entry.DateTime = (file.CreationTime > file.LastWriteTime ? file.LastWriteTime : file.CreationTime);
                    s.PutNextEntry(entry);
                    while (true)
                    {
                        size = fs.Read(data, 0, size);
                        if (size <= 0) break;
                        s.Write(data, 0, size);
                    }
                    fs.Close();
                    if (deleteFile)
                    {
                        file.Delete();
                    }
                }
            }
            finally
            {
                s.Finish();
                s.Close();
            }
        }
        /// <summary>  
        /// 压缩文件夹  
        /// </summary>  
        /// <param name="dirPath">要打包的文件夹</param>  
        /// <param name="GzipFileName">目标文件名</param>  
        /// <param name="CompressionLevel">压缩品质级别（0~9）</param>  
        /// <param name="deleteDir">是否删除原文件夹</param>
        public static void CompressDirectory(string dirPath, string GzipFileName, int CompressionLevel, bool deleteDir)
        {
            //压缩文件为空时默认与压缩文件夹同一级目录  
            if (GzipFileName == string.Empty)
            {
                GzipFileName = dirPath.Substring(dirPath.LastIndexOf("//") + 1);
                GzipFileName = dirPath.Substring(0, dirPath.LastIndexOf("//")) + "//" + GzipFileName + ".zip";
            }
            //if (Path.GetExtension(GzipFileName) != ".zip")
            //{
            //    GzipFileName = GzipFileName + ".zip";
            //}
            using (ZipOutputStream zipoutputstream = new ZipOutputStream(File.Create(GzipFileName)))
            {
                zipoutputstream.SetLevel(CompressionLevel);
                Crc32 crc = new Crc32();
                Dictionary<string, DateTime> fileList = GetAllFies(dirPath);
                foreach (KeyValuePair<string, DateTime> item in fileList)
                {
                    FileStream fs = File.OpenRead(item.Key.ToString());
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(item.Key.Substring(dirPath.Length));
                    entry.DateTime = item.Value;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zipoutputstream.PutNextEntry(entry);
                    zipoutputstream.Write(buffer, 0, buffer.Length);
                }
            }
            if (deleteDir)
            {
                Directory.Delete(dirPath, true);
            }
        }
        /// <summary>  
        /// 获取所有文件  
        /// </summary>  
        /// <returns></returns>  
        private static Dictionary<string, DateTime> GetAllFies(string dir)
        {
            Dictionary<string, DateTime> FilesList = new Dictionary<string, DateTime>();
            DirectoryInfo fileDire = new DirectoryInfo(dir);
            if (!fileDire.Exists)
            {
                throw new System.IO.FileNotFoundException("目录:" + fileDire.FullName + "没有找到!");
            }
            GetAllDirFiles(fileDire, FilesList);
            GetAllDirsFiles(fileDire.GetDirectories(), FilesList);
            return FilesList;
        }
        /// <summary>  
        /// 获取一个文件夹下的所有文件夹里的文件  
        /// </summary>  
        /// <param name="dirs"></param>  
        /// <param name="filesList"></param>  
        private static void GetAllDirsFiles(DirectoryInfo[] dirs, Dictionary<string, DateTime> filesList)
        {
            foreach (DirectoryInfo dir in dirs)
            {
                foreach (FileInfo file in dir.GetFiles("*.*"))
                {
                    filesList.Add(file.FullName, file.LastWriteTime);
                }
                GetAllDirsFiles(dir.GetDirectories(), filesList);
            }
        }
        /// <summary>  
        /// 获取一个文件夹下的文件  
        /// </summary>  
        /// <param name="dir">目录名称</param>  
        /// <param name="filesList">文件列表HastTable</param>  
        private static void GetAllDirFiles(DirectoryInfo dir, Dictionary<string, DateTime> filesList)
        {
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                filesList.Add(file.FullName, file.LastWriteTime);
            }
        }
        #endregion
        #region 解压缩文件
        /// <summary>  
        /// 解压缩文件  
        /// </summary>  
        /// <param name="GzipFile">压缩包文件名</param>  
        /// <param name="targetPath">解压缩目标路径</param>         
        public static void Decompress(string GzipFile, string targetPath)
        {
            //string directoryName = Path.GetDirectoryName(targetPath + "//") + "//";  
            string directoryName = targetPath;
            if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);//生成解压目录  
            string CurrentDirectory = directoryName;
            byte[] data = new byte[2048];
            int size = 2048;
            ZipEntry theEntry = null;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(GzipFile)))
            {
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.IsDirectory)
                    {// 该结点是目录  
                        if (!Directory.Exists(CurrentDirectory + theEntry.Name)) Directory.CreateDirectory(CurrentDirectory + theEntry.Name);
                    }
                    else
                    {
                        if (theEntry.Name != String.Empty)
                        {
                            //  检查多级目录是否存在
                            if (theEntry.Name.Contains("//"))
                            {
                                string parentDirPath = theEntry.Name.Remove(theEntry.Name.LastIndexOf("//") + 1);
                                if (!Directory.Exists(parentDirPath))
                                {
                                    Directory.CreateDirectory(CurrentDirectory + parentDirPath);
                                }
                            }

                            //解压文件到指定的目录  
                            using (FileStream streamWriter = File.Create(CurrentDirectory + theEntry.Name))
                            {
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size <= 0) break;
                                    streamWriter.Write(data, 0, size);
                                }
                                streamWriter.Close();
                            }
                        }
                    }
                }
                s.Close();
            }
        }


        /// <summary>  
        /// 解压缩文件  
        /// </summary>  
        /// <param name="bytes">流</param>  
        /// <param name="targetPath">解压缩目标路径</param>         
        public static void Decompress(byte[] bytes, string targetPath)
        {
            //string directoryName = Path.GetDirectoryName(targetPath + "//") + "//";  
            string directoryName = targetPath;
            if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);//生成解压目录  
            string CurrentDirectory = directoryName;
            byte[] data = new byte[2048];
            int size = 2048;
            ZipEntry theEntry = null;

            MemoryStream stream = new MemoryStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Position = 0;
            using (ZipInputStream s = new ZipInputStream(stream))
            //File.OpenRead(GzipFile)))
            {
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.IsDirectory)
                    {// 该结点是目录  
                        if (!Directory.Exists(CurrentDirectory + theEntry.Name)) Directory.CreateDirectory(CurrentDirectory + theEntry.Name);
                    }
                    else
                    {
                        if (theEntry.Name != String.Empty)
                        {
                            //  检查多级目录是否存在
                            if (theEntry.Name.Contains("//"))
                            {
                                string parentDirPath = theEntry.Name.Remove(theEntry.Name.LastIndexOf("//") + 1);
                                if (!Directory.Exists(parentDirPath))
                                {
                                    Directory.CreateDirectory(CurrentDirectory + parentDirPath);
                                }
                            }

                            //解压文件到指定的目录  
                            using (FileStream streamWriter = File.Create(CurrentDirectory + theEntry.Name))
                            {
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size <= 0) break;
                                    streamWriter.Write(data, 0, size);
                                }
                                streamWriter.Close();
                            }
                        }
                    }
                }
                s.Close();
            }
        }
        #endregion
    }

}
