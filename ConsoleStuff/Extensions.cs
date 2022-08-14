using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStuff;

public static class Extensions
{
    public static List<string> BigCopy(string[] args)
    {
        //string j;
        int fileContentsCounter = 0;
        string[] fileContents = new string[args.Length - 2];
        try
        {
            for (int i = 2; i < args.Length; i++)
            {
                //j = args[i] += " ";
                fileContents[fileContentsCounter] = args[i];
                fileContentsCounter++;
            }

            //Array.Copy(args, 2, fileContents, 0, args.Length - 2);
            //Array.ConstrainedCopy(args, 3, fileContents, 0, args.Length - 2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.InnerException);
            //Array.ConstrainedCopy(args, 3, fileContents, 0, args.Length - 2);
        }

        return fileContents.ToList();
    }

    public static void SpaceInsert(List<string> args)
    {
        for (int i = 0; i < args.Count; i++)
        {
            args[i] += " ";
        }

        //return args;
    }

    public static void SpaceInsert(string fileContent)
    {
        fileContent += " ";
        //return fileContent;
    }

    //public static string EqualsCheck(this string str)
    //{
    //    if (!str.Equals("Create", StringComparison.CurrentCultureIgnoreCase) || str == null)
    //    {
    //        return str;
    //    }
    //    else if (!str.Equals("Write", StringComparison.CurrentCultureIgnoreCase) || str == null)
    //    {
    //        return str;
    //    }
    //    else
    //    {
    //        return str;
    //    }
    //}

    //public static bool EqualsCheck(string str, string str2)
    //{
    //    if (!str.Equals("Create", StringComparison.CurrentCultureIgnoreCase) || str == null)
    //    {
    //        return false;
    //    }
    //    else if (!str.Equals("Write", StringComparison.CurrentCultureIgnoreCase) || str == null)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
}