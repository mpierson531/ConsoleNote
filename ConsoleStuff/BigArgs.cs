﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStuff;

public static class BigArgs
{
    public static string[] BigCopy(string[] args) 
    {
        string j;
        int fileContentsCounter = 0;
        string[] fileContents = new string[args.Length - 2];
        try
        {
            if (args.Length > 3)
            {
                for (int i = 2; i < args.Length; i++)
                {
                    j = args[i] += " ";
                    fileContents[fileContentsCounter] = j;
                    fileContentsCounter++;
                }

                //Array.Copy(args, 2, fileContents, 0, args.Length - 2);
                //Array.ConstrainedCopy(args, 3, fileContents, 0, args.Length - 2);
            }
        } catch (Exception e)
        {
            Console.WriteLine(e.InnerException);
            //Array.ConstrainedCopy(args, 3, fileContents, 0, args.Length - 2);
        }

        return fileContents;
    }
}
