using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppEldos.Models
{
    public class LogDetail
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Completion { get; set; }
        public string Size { get; set; }
        public string ProcessStyle { get; set; }
    }
}