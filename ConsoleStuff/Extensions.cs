using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStuff;

public static class Extensions
{
    public static List<string> BigCopy(string[] args)
    {
        int fileContentsCounter = 0;
        string[] fileContents = new string[args.Length - 2];

        try
        {
            for (int i = 2; i < args.Length; i++)
            {
                fileContents[fileContentsCounter] = args[i];
                fileContentsCounter++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.InnerException);
        }

        return fileContents.ToList();
    }

    public static List<string> BigCopy(List<string> content)
    {
        int fileContentsCounter = 0;
        List<string> fileContents = new List<string>(content.Count - 2);

        try
        {
            for (int i = 2; i < content.Count; i++)
            {
                fileContents.Insert(fileContentsCounter, content[i]);
                fileContentsCounter++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.InnerException);
        }

        return fileContents;
    }

    public static void SpaceInsert(List<string> args)
    {
        for (int i = 0; i < args.Count; i++)
        {
            args[i] += " ";
        }

    }

    public static void SpaceInsert(string fileContent)
    {
        fileContent += " ";
    }
}