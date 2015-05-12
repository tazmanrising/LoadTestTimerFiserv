using EldosFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppEldos.Models;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace WebAppEldos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            LogDetail logDetail = new LogDetail();
            List<LogFileDetail> model = null;
            var stopwatch = new Stopwatch();
            var files = new List<string>();

            using (var s = new EldosFileSystem(@"C:\projects\eldosFile\WebAppEldos\WebAppEldos\VirtualDrive\Weekend6.st"))  //@"C:\projects\eldosFile\WebAppEldos\WebAppEldos\VirtualDrive\Friday.st")) //Friday.st"))
            {
                stopwatch.Start();
                var dir = @"C:\Upload";

                //files.Add(@"1.jpg");   //  859 kb
                //files.Add(@"twentymb.doc");
                //files.Add(@"fiftymb.xls");
                //files.Add(@"hstpov2.xls");
                //files.Add(@"hstpov3.xls");
                //files.Add(@"hstpov4.xlsx");
                //files.Add(@"hstpov5.xlsx");
                //files.Add(@"hstpov6.xlsx");
                //files.Add(@"hstpov7.xlsx");
                //files.Add(@"hstpov8.xlsx");
                //files.Add(@"[Tadashi_Ozawa]_How_to_Draw_Anime__Game_Character(BookZZ.org).pdf");
                //files.Add(@"[Thomas_Erl]_Service_Oriented_Architecture_Princi(BookZZ.org).pdf");
                //files.Add(@"[Trung_Le]_How_to_Draw_Anime_for_Beginner(Bokos-Z1).pdf");
                //files.Add(@"2.jpg");


                // Single File Tested for Sync and ASync
                //files.Add(@"1.jpg");


                // NEW TEST CASES  #######################


                //[4/30/2015 2:51 PM] Mark Carter: 
                //Usage Scenarios :  2 Users, 4,6,10,15,20      (  2 threads with files of 4, 6, 10, 15, 20 mb.  each)
                //[4/30/2015 2:51 PM] Mark Carter: 
                //Load Times : 3 minutes, 6,9, 15, 20        ( 3 minutes of repeatedly have  6 mb, 9 mb, 15 mb, 20 mb  files being tested again the eldos
                //[4/30/2015 2:52 PM] Mark Carter: 
                //File Sizes : 50 K, 75 K, 100 K, 5 Mb, 10 Mb, 50 Mb,, 75Mb, 100 Mb        ( testing single files with ranging small to large )

                //
                //Below appears to be the same thing revised a bit????

                //Usage Scenarios :  2 Users, 4,6,10,15,20 
                //Load Times : 3 minutes, 6,9, 15, 20
                //File Sizes : 50 K, 75 K, 100 K, 5 Mb, 10 Mb, 50 Mb,, 75Mb, 100 Mb
                //2 Users => 2 Async Files ... 
                //4 Users => 4 Async Files
                //Each User will load a 50K, 75K, ... 100 Mb


                //  with a goal of ALSO trying to swap out the DLL for the OS DLL  
                //C:\Program Files (x86)\EldoS\SolFS.OS\dotNet\NET_451\x64\SolFS5DrvNet.dll

                files.Add(@"2.jpg");



                //Add Async
                s.AddFiles(dir, files);




                //Add Sync
                foreach (var file in files)
                    using (var fileStream = new FileStream(string.Format(@"{0}\{1}", dir, file), FileMode.Open, FileAccess.Read))
                        s.AddFile(null, file, fileStream, true);


                model = s.Logs.ToList();
            }
            stopwatch.Stop();

            logDetail.Configuration = new LoadConfiguration() { FileSize = "500K", LoadTime = 0, NumberOfFiles = 1, NumberOfLoops = 0, NumberOfThreads = 1, TestGroup = "Default Run" };

            logDetail.LogFileDetails = model.ToList();
            logDetail.NumberOfTest = files.Count;
            logDetail.ElapseTime = (stopwatch.ElapsedMilliseconds / 1000) == 0 ? string.Format("0.{0}s", stopwatch.ElapsedMilliseconds) : string.Format("{0}s", stopwatch.ElapsedMilliseconds / 1000);

            ToFile(logDetail);

            if (logDetail != null && logDetail.LogFileDetails != null)
                return View("Results", logDetail);


            return View();
        }
        [HttpGet]
        public ActionResult LoadTest()
        {
            return View(new LoadConfiguration());
        }
        [HttpPost]
        public ActionResult LoadTest(LoadConfiguration loadConfiguration)
        {
            var stopwatch = new Stopwatch();
            var loadDuration = new StopWatch(loadConfiguration.LoadTime);
            List<LogFileDetail> model = null;
            LogDetail logDetail = new LogDetail();
            var path = @"C:\Upload";
            var fileSizesDictionary = new Dictionary<string, string>();
            fileSizesDictionary.Add("50K", "50K.xls");
            fileSizesDictionary.Add("75K", "75K.xls");
            fileSizesDictionary.Add("100K", "100K.xls");
            fileSizesDictionary.Add("5MB", "5MB.doc");
            fileSizesDictionary.Add("10MB", "10MB.xls");
            fileSizesDictionary.Add("50MB", "50MB.xls");
            fileSizesDictionary.Add("100MB", "100mb.xls");

            string fileName = string.Empty;

            if (fileSizesDictionary.TryGetValue(loadConfiguration.FileSize, out fileName))
            {
                using (var eldosFileSystem = new EldosFileSystem(@"C:\projects\eldosFile\WebAppEldos\WebAppEldos\VirtualDrive\Weekend6.st"))  //@"C:\projects\eldosFile\WebAppEldos\WebAppEldos\VirtualDrive\Friday.st")) //Friday.st"))
                {
                    var numberOfLoops = 0;
                    stopwatch.Start();
                    do
                    {
                        var i = 0;
                        do
                        {
                            using (var fileStream = new FileStream(string.Format(@"{0}\{1}", path, fileName), FileMode.Open, FileAccess.Read))
                            {
                                eldosFileSystem.LoadTestAdd(fileStream, fileName, loadConfiguration.NumberOfFiles, loadConfiguration.NumberOfThreads);
                            }
                            i++;
                            numberOfLoops++;
                        }
                        while (i < loadConfiguration.NumberOfLoops);
                    }
                    while (loadDuration.IsRunning);
                    stopwatch.Stop();
                    logDetail.Configuration = loadConfiguration;
                    logDetail.LogFileDetails = eldosFileSystem.Logs.ToList();
                    logDetail.NumberOfTest = numberOfLoops;
                    logDetail.ElapseTime = (stopwatch.ElapsedMilliseconds / 1000) == 0 ? string.Format("0.{0}s", stopwatch.ElapsedMilliseconds) : string.Format("{0}s", stopwatch.ElapsedMilliseconds / 1000);
                }
            }
            ToFile(logDetail);

            if (logDetail != null && logDetail.LogFileDetails != null)
                return View("Results", logDetail);

            return View();
        }

        private void ToFile(LogDetail logDetail)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\n",
                "NumberOfTest",
                "ElapseTime",
                "FileSize.Config",
                "LoadTime.Config",
                "NumberOfFiles.Config",
                "NumberOfLoops.Config",
                "NumberOfThreads.Config",
                "TestGroup.Config",
                "FileName",
                "FilePath",
                "FileType",
                "ProcessStyle",
                "ProcessTime",
                "Size"

                );
            var strLogDetail = string.Empty;
            var strConfiguration = string.Empty;
            string fileName = null;
            if (logDetail != null)
            {
                strLogDetail = string.Format("{0}\t{1}", logDetail.NumberOfTest, logDetail.ElapseTime);

                if (logDetail.Configuration != null)
                {
                    fileName = logDetail.Configuration.TestGroup;
                    strConfiguration = string.Format("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                                           logDetail.Configuration.FileSize,
                                           logDetail.Configuration.LoadTime,
                                           logDetail.Configuration.NumberOfFiles,
                                           logDetail.Configuration.NumberOfLoops,
                                           logDetail.Configuration.NumberOfThreads,
                                           logDetail.Configuration.TestGroup);
                }

                if (logDetail.LogFileDetails != null)
                    foreach (var logFileDetail in logDetail.LogFileDetails)
                    {
                        sb.AppendLine(string.Format("{0}{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\n", strLogDetail, strConfiguration,
                            logFileDetail.FileName,
                            logFileDetail.FilePath,
                            logFileDetail.FileType,
                            logFileDetail.ProcessStyle,
                            logFileDetail.ProcessTime,
                            logFileDetail.Size));
                    }

                string filePath = string.Format(@"C:\projects\eldosFile\WebAppEldos\WebAppEldos\VirtualDrive\{0}.tsv", fileName ?? DateTime.Now.ToString("MM-dd-yyyy-hh-mm-ss"));
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                System.IO.File.WriteAllText(filePath, sb.ToString());
            }
        }

        public ActionResult Results(List<LogFileDetail> model)
        {
            return View(model);
        }

        public ActionResult Load(int id = 1)
        {
            using (var s = new EldosFileSystem(@"C:\projects\eldosFile\WebAppEldos\WebAppEldos\VirtualDrive\Friday.st"))
            {
                var dir = @"C:\Upload";

                var files = new List<string>();
                for (int i = 0; i < id; i++)
                {
                    files.Add(@"5.jpg");
                }


                s.AddFiles(dir, files);

                ViewBag.Logs = s.Logs;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.InputStream != null)
                {
                    //if (System.IO.File.Exists(@"C:\projects\eldosFile\WebAppEldos\WebAppEldos\VirtualDrive\Default1.st"))
                    //    System.IO.File.Delete(@"C:\projects\eldosFile\WebAppEldos\WebAppEldos\VirtualDrive\Default1.st");

                    using (var fileSystem = new EldosFileSystem())
                    {
                        fileSystem.AddFile(null, file.FileName, file.InputStream, true);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {

            }


            return View();
        }
    }
}