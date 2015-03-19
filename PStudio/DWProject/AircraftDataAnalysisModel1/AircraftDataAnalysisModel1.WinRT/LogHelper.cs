using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT
{
    public class LogHelper
    {
        public async static void Error(Exception exception)
        {
            try
            {
                if (exception != null)
                {
                    var dest1 = await Windows.Storage.DownloadsFolder.CreateFileAsync(
                        GetSelfLogFile(), Windows.Storage.CreationCollisionOption.GenerateUniqueName);

                    await Windows.Storage.FileIO.AppendLinesAsync(dest1,
                        GetExceptionLines(exception));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        private static IEnumerable<string> GetExceptionLines(Exception exception)
        {
            if (exception is AggregateException && exception.InnerException != null)
            {
                AggregateException agge = exception as AggregateException;
                List<string> str = new List<string>();
                str.Add(string.Format("{0} {1} \t{2}", DateTime.Now.ToString(),
                    agge.InnerException.Message, agge.InnerException.StackTrace));
                foreach (Exception e in agge.InnerExceptions)
                {
                    if (e != null)
                        str.Add(string.Format("{0} {1} \t{2}", DateTime.Now.ToString(),
                            e.Message, e.StackTrace));
                }
                return str.ToArray();
            }

            return new string[]{
            string.Format("{0} {1} \t{2}", DateTime.Now.ToString(), exception.Message, exception.StackTrace)};
        }

        private static string GetSelfLogFile()
        {
            return string.Format("AircraftDataAnalysisLog_{0}.log", DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}
