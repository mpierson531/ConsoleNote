namespace ConsoleStuff;

public class ConsoleClass1
{
    static string? FileName;
    static string? FileContent;
    static string? Command;
    static string CurrentDirectory = Environment.CurrentDirectory;
    static bool IsArgsBig = false;
    static bool FileNameHasTXT;
    static List<string> content;
    public static void Main(string[] args)
    {
        IsBig(args);

        HasNewline(content);

        ArgVarAssigning(args);

        ArgumentChecking();

        Console.ReadLine();
    }

    static void CreateFile(string @fileName)
    {
        File.Create(CurrentDirectory + @$"\{fileName}" + ".txt");
    }

    static void WriteToFile(string @fileName, string @fileContent, List<string> @content)
    {
        if (IsArgsBig && FileNameHasTXT)
        {
            foreach (string i in @content)
            {
                File.AppendAllText(fileName, i);
            }
        } else if (IsArgsBig && !FileNameHasTXT)
        {
            foreach (string j in content)
            {
                File.AppendAllText(fileName + ".txt", j);
            }
        }
        else if (!IsArgsBig && FileNameHasTXT)
        {
            File.AppendAllText(fileName, fileContent + "\n");
        }
        else if (!IsArgsBig && !FileNameHasTXT)
        {
            File.AppendAllText(@fileName + ".txt", @fileContent);
        }
    }

    static void IsBig(string[] args)
    {
        if (args.Length > 3)
        {
            //content = new List<string>();
            content = Extensions.BigCopy(args);
            //.GetRange(3, args.Length - 2);
            IsArgsBig = true;
        }
        else
        {
            IsArgsBig = false;
            try
            {
                //content = args.ToList();
                FileContent = content[2];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //HasNewline(ArgVarAssigning(content));
        //return content;
    }

    static void ArgVarAssigning(string[] args)
    {
        try
        {
            Command = args[0];
            FileName = args[1];
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.Source);
            Console.WriteLine(e.InnerException);
        }
        //return args;
    }


    static void HasNewline(List<string> args)
    {
        if (IsArgsBig)
        {
            int argsIndexCounter = 0;
            foreach (string i in args)
            {
                if (i.Contains("\n"))
                {
                    args[argsIndexCounter] += "\n";
                }

                argsIndexCounter++;
            }
        }
        //return;
    }
    static void ArgumentChecking()
    {
        DoesFileNameHaveTXT();
        if (Command is null)
        {
            Console.WriteLine("Please enter Command: create or write");
            Command = Console.ReadLine();
        }

        if (FileName is null)
        {
            Console.WriteLine("Enter the file name you want to create or change");
            FileName = Console.ReadLine();
        }

        if (Command.Contains("Create", StringComparison.CurrentCultureIgnoreCase))
        {
            CreateFile(FileName);
        }
        else if (Command.Contains("Write", StringComparison.CurrentCultureIgnoreCase))
        {
            WriteToFile(FileName, FileContent, content);
        }
        else
        {
            Console.WriteLine("Please enter either 'Create' or 'Write'.");
        }
    }
    static void DoesFileNameHaveTXT()
    {
        if (@FileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
        {
            FileNameHasTXT = true;
        }
        else
        {
            FileNameHasTXT = false;
        }
    }
}