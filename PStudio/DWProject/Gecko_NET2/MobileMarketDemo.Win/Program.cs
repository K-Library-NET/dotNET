using Gecko;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MobileMarketDemo.Win
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DlgLoading loadingForm = new DlgLoading();
            DialogResult result = loadingForm.ShowDialog();
            if (result != DialogResult.OK)
                return;
			//Xpcom.Initialize(GetXULRunnerLocation());

			// Uncomment the follow line to enable CustomPrompt's
			// GeckoPreferences.User["browser.xul.error_pages.enabled"] = false;
			
			GeckoPreferences.User["gfx.font_rendering.graphite.enabled"] = true;			
			
			Application.ApplicationExit += (sender, e) => 
			{
        		Xpcom.Shutdown();
			};

            Application.Run(new Form1());
        }

        private static string GetXULRunnerLocation()
        {
            return "xulrunner";

            //NB for shipping apps, we don't have a way to find their xulrunner, so they won't be running this code 
            //unless they depend on the customer installing a certain verion of Firefox and keeping it from auto-updating.
            //So this is more for unit tests and geckofx sample apps.

            //For unit tests, look for a xulrunner directory placed in the solution/PutXulRunnerFolderHere directory
            //var solutionXulRunnerFolder = PathCombine(DirectoryOfTheApplicationExecutable, "..", "..", "..",
            //                                           "PutXulRunnerFolderHere", "XulRunner");
            //if (Directory.Exists(solutionXulRunnerFolder))
            //    return solutionXulRunnerFolder;


            ////for test app, we have to go up one more level
            //solutionXulRunnerFolder = PathCombine(DirectoryOfTheApplicationExecutable, "..", "..", "..", "..",
            //                                "PutXulRunnerFolderHere", "XulRunner");
            //if (Directory.Exists(solutionXulRunnerFolder))
            //    return solutionXulRunnerFolder;

            ////look for firefox itself

            //string[] folderSearch = new string[] { solutionXulRunnerFolder, "Mozilla Firefox 18.0", "Mozilla Firefox 18", "Mozilla Firefox" };

            //var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            //foreach (string folderOne in folderSearch)
            //{
            //    string tempPath = Path.Combine(programFiles, folderOne);
            //    DirectoryInfo info = new DirectoryInfo(tempPath);
            //    if (info.Exists)
            //        return tempPath;
            //}
            return string.Empty;
        }
    }
}
