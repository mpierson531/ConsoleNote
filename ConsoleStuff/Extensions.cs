namespace ConsoleNote;

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

    public static void SpaceInsert(List<string> strings)
    {
        for (int i = 0; i < strings.Count; i++)
        {
            strings[i] += " ";
        }
    }

    public static void SpaceInsert(string fileContent)
    {
        fileContent += " ";
    }

    public static void InsertNewline(List<string> bigCopy)
    {
        for (int i = 0; i < bigCopy.Count; i++)
        {
            if (bigCopy[i].Contains("\\n"))
            {
                bigCopy[i] = bigCopy[i].Replace("\\n", Environment.NewLine);
            }
        }
    }

    public static string InsertNewline(string SingleStringContent)
    {
        try
        {
            if (SingleStringContent.Contains("\\n"))
            {
                SingleStringContent = SingleStringContent.Replace("\\n", Environment.NewLine);
            }
        }
        catch (NullReferenceException) { }

        return SingleStringContent;
    }
}