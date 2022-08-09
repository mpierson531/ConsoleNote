namespace ConsoleStuff;

public class ConsoleClass1
{
    static string? fileName;
    static string? fileContent;
    static string? command;
    static string currentDirectory = Environment.CurrentDirectory;
    static bool argsBig = false;
    static List<string> content;
    public static void Main(string[] args)
    {
        try
        {
            command = args[0];
            fileName = args[1];
        } catch (Exception e2)
        {
            Console.WriteLine(e2.Message);
            Console.WriteLine(e2.Source);
            Console.WriteLine(e2.InnerException);
        }

        if (args.Length > 3)
        {
            content = new List<string>(args.Count());
            argsBig = true;
            content = ArgumentExtension.BigCopy(args);
                //.GetRange(3, args.Length - 2);
        }
        else
        {
            argsBig = false;
            try
            {
                fileContent = args[2];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        if (command == null)
        {
            Console.WriteLine("Please enter command: create or write");
            command = Console.ReadLine();
        }

        if (fileName == null)
        {
            Console.WriteLine("Enter the file name you want to create or change");
            fileName = Console.ReadLine();
        }

        if (command.Contains("Create", StringComparison.CurrentCultureIgnoreCase))
        {
            CreateFile(fileName);
        }
        else if (command.Contains("Write", StringComparison.CurrentCultureIgnoreCase))
        {
            WriteToFile(fileName, fileContent, content);
        }
        else
        {
            Console.WriteLine("Please enter either 'Create' or 'Write'.");
        }

        Console.ReadLine();
    }

    static void CreateFile(string @fileName)
    {
        File.Create(currentDirectory + @$"\{fileName}" + ".txt");
    }

    static void WriteToFile(string @fileName, string @fileContent, List<string> @content)
    {
        if (argsBig && @fileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
        {
            foreach (string i in @content)
            {
                File.AppendAllText(fileName, i);
            }
        } else if (argsBig && !fileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
        {
            foreach (string j in content)
            {
                File.AppendAllText(fileName + ".txt", j);
            }
        }
        else if (!argsBig && fileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
        {
            File.AppendAllText(fileName, fileContent);
        }
        else if (!argsBig && !fileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
        {
            File.AppendAllText(@fileName + ".txt", @fileContent);
        }
    }
}