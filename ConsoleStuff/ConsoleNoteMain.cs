using static System.Console;

namespace ConsoleStuff
{
    public class ConsoleClass1
    {
        static string? fileName;
        static string? fileContent;
        static string? command;
        static string currentDirectory = Environment.CurrentDirectory;
        public static void Main(string[] args)
        {
            command = args[0];
            fileName = args[1];

            try
            {
                fileContent = args[2];
            }
            catch (Exception e)
            { WriteLine(e.Message); }

            if (fileName == null)
            {
                WriteLine("Enter the file name you want to create or change");
                fileName = ReadLine();
            }

            if (command.Contains("Create", StringComparison.CurrentCultureIgnoreCase))
            {
                CreateFile(fileName);
            }
            else if (command.Contains("Write", StringComparison.CurrentCultureIgnoreCase))
            {
                WriteToFile(fileName, fileContent);
            }
            else
            {
                WriteLine("Please enter either 'Create\' or 'Write'.");
            }

            ReadLine();
        }

        static void CreateFile(string @fileName)
        {
            File.Create(currentDirectory + @$"\{fileName}" + ".txt");
        }

        static void WriteToFile(string @fileName, string @fileContent)
        {
            if (fileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
            {
                File.AppendAllText(fileName, fileContent);
            }
            else if (!fileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
            {
                File.AppendAllText(@fileName + ".txt", @fileContent);
            }
        }
    }
}