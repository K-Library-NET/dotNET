using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXStudio.DataMgt.Web
{
    public class LogHelper
    {
        private static string m_loggerName = "DefaultLogger";

        static LogHelper()
        {
            log4net.Config.XmlConfigurator.Configure();

            if (!string.IsNullOrEmpty(
                System.Configuration.ConfigurationManager.AppSettings["Log4NetLoggerName"]))
            {
                m_loggerName = System.Configuration.ConfigurationManager.AppSettings["Log4NetLoggerName"];
            }
        }

        public static void Error(string p, Exception e)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Error(p, e);
        }

        public static void Error(string p)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Error(p);
        }

        public static void Info(string p)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Info(p);
        }

        public static void Debug(string p)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Debug(p);
        }

        public static void Fatal(string p, Exception e)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Fatal(p, e);
        }
    }
}