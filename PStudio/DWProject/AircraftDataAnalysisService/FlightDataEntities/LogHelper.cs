using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace FlightDataEntities
{
    public class LogHelper
    {
        public const string DEFAULT_LOGGER_NAME = "AircraftDataAnalysis";

        private static log4net.ILog m_logger = null;

        static LogHelper()
        {
            m_logger = log4net.LogManager.GetLogger(DEFAULT_LOGGER_NAME);
        }

        /// <summary>
        /// Debug级别日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="e">异常可以为空</param>
        public static void Debug(string message, Exception e)
        {
            if (e == null)
                m_logger.Debug(message);
            else m_logger.Debug(message, e);
        }

        /// <summary>
        /// Info级别日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="e">异常可以为空</param>
        public static void Info(string message, Exception e)
        {
            if (e == null)
                m_logger.Info(message);
            else m_logger.Info(message, e);
        }

        /// <summary>
        /// Warning级别日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="e">异常可以为空</param>
        public static void Warn(string message, Exception e)
        {
            if (e == null)
                m_logger.Warn(message);
            else m_logger.Warn(message, e);
        }

        /// <summary>
        /// Error级别日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="e">异常可以为空</param>
        public static void Error(string message, Exception e)
        {
            if (e == null)
                m_logger.Error(message);
            else m_logger.Error(message, e);
        }

        /// <summary>
        /// Fatal级别日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="e">异常可以为空</param>
        public static void Fatal(string message, Exception e)
        {
            if (e == null)
                m_logger.Fatal(message);
            else m_logger.Fatal(message, e);
        }
    }
}
