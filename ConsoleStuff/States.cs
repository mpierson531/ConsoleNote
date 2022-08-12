using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNote
{
    public class States
    {
        string? FileName;
        string? FileContent;
        string? Command;
        string CurrentDirectory;
        bool IsArgsBig = false;
        bool FileNameHasTXT;
        List<string> content;
        static bool ArgsNull;
        public States(string[] args)
        {
            DetectState(args);
        }

        static bool IsArgsNull(string[] args)
        {
            if (args == null)
            {
                return ArgsNull = true;
            }
            else
            {
                return ArgsNull = false;
            }
        }

        static void DetectState(string[] args)
        {
            if (IsArgsNull(args))
            {
                InAppState inAppState = new InAppState();
            }
            else
            {
                ArgumentState argumentState = new ArgumentState();
            }
        }
    }
}
