using System.Collections.Generic;

namespace WebAppEldos.Models
{
    public class LoadConfiguration
    {
        public string FileSizes { get; set; }
        public int NumberOfFiles { get; set; }
        public int NumberOfThreads { get; set; }
        public int LoadTime { get; set; }
        public int NumberOfLoops { get; set; }
    }
}
