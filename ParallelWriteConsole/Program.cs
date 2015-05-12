using EldosFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelWriteConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var s = new EldosFileSystem(@"C:\projects\eldosFile\WebAppEldos\ParallelWriteConsole\VirutalStorage\Default5.st"))
            {
                var dir = @"C:\Users\c-tstickel\Downloads\";

                var files = new List<string>();

                files.Add(@"en_sql_server_2014_developer_edition_x64_dvd_3940406.iso");
                files.Add(@"IE11-Windows6.1-x64-en-us.exe");
                files.Add(@"solfsapp_eval.zip");
                files.Add(@"File Library Feature Summary.docx");
                files.Add(@"Firefox Setup Stub 37.0.exe");

                s.AddFiles(dir, files);
            }
        }


    }
}
