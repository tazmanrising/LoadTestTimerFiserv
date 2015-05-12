using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EldosFileLib
{
    public class LogFileDetail
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string ProcessTime { get; set; }
        public string Size { get; set; }
        public string ProcessStyle { get; set; }
    }

    public class LoadConfiguration
    {
        public string FileSize { get; set; }
        public int NumberOfFiles { get; set; }
        public int NumberOfThreads { get; set; }
        public int LoadTime { get; set; }
        public int NumberOfLoops { get; set; }
        public string TestGroup { get; set; }
    }

    public class LogDetail
    {
        public int NumberOfTest { get; set; }
        public string ElapseTime { get; set; }
        public LoadConfiguration Configuration { get; set; }
        public List<LogFileDetail> LogFileDetails { get; set; }

    }
}