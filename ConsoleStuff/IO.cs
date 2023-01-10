using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNote
{
    internal class IO
    {
        private TextReader In;
        private TextWriter Out;
        public string Filename { get; private set; }
        public string Command { get; private set; }

        public IO()
        {
            In = Console.In;
            Out = Console.Out;
        }

        public void Start()
        {
            Command = GetCommand();
            ProcessInput(Command);
        }

        public string GetCommand()
        {
            ConsoleColor oldConsoleColor = Console.ForegroundColor;

            WriteLine("Please enter: 'Create', 'Write', 'Open', 'Remove', or 'Delete'.");

            Console.ForegroundColor = ConsoleColor.White;

            string command = ReadLine();
            ProcessInput(command);

            Console.ForegroundColor = oldConsoleColor;

            return command;
        }

        public void RunCommand()
        {
            if (Command.Equals("Create", StringComparison.InvariantCultureIgnoreCase))
            {
                StartCreateDialogue();
            }
            else if (Command.Equals("Write", StringComparison.InvariantCultureIgnoreCase))
            {
                StartWriteDialogue();
            }
            else if (Command.Equals("Open", StringComparison.InvariantCultureIgnoreCase))
            {
                StartOpenDialogue();
            }
            else if (Command.Equals("Remove", StringComparison.InvariantCultureIgnoreCase))
            {
                StartRemoveDialogue();
            }
            else if (Command.Equals("Delete", StringComparison.InvariantCultureIgnoreCase))
            {
                StartDeleteDialogue();
            }
        }

        private void StartCreateDialogue()
        {
            ConsoleColor oldConsoleColor = Console.ForegroundColor;

            WriteLine("Enter the name or path of the file to create.");

            Console.ForegroundColor = ConsoleColor.White;

            Filename = ReadLine();
            ProcessInput(Filename);

            string filePath = StorageHandler.GetValidFilename(Filename);
            StorageHandler.CreateFile(filePath).Close();
            Thread.Sleep(200);

            if (File.Exists(filePath))
            {
                WriteLine($"{filePath} created.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteLine($"Failed to create {filePath}");
            }

            Console.ForegroundColor = oldConsoleColor;
        }

        public void StartWriteDialogue()
        {
            ConsoleColor oldConsoleColor = Console.ForegroundColor;
            WriteLine("Please enter the name of the file to write to.");
            Console.ForegroundColor = ConsoleColor.White;

            Filename = ReadLine();
            ProcessInput(Filename);

            Console.ForegroundColor = oldConsoleColor;

            ConsoleColor color = Console.ForegroundColor;
            WriteLine($"Enter the content to write to {Filename}.");
            Console.ForegroundColor = ConsoleColor.White;

            string content = ReadLine();
            ProcessInput(content);

            string filePath = StorageHandler.GetValidFilename(Filename);

            StorageHandler.WriteFile(filePath, content);

            WriteLine($"Content written to '{filePath}'");
            Console.ForegroundColor = color;
        }

        public void StartOpenDialogue()
        {
            ConsoleColor oldConsoleColor = Console.ForegroundColor;
            WriteLine("Enter the name or path of the file to open.");
            Console.ForegroundColor = ConsoleColor.White;

            Filename = ReadLine();
            ProcessInput(Filename);

            string filePath = StorageHandler.GetValidFilename(Filename);
            string content = StorageHandler.ReadFile(filePath);

            Console.ForegroundColor = oldConsoleColor;
            WriteLine($"{filePath}: \n{content}");
        }

        public void StartRemoveDialogue()
        {
            ConsoleColor oldConsoleColor = Console.ForegroundColor;
            WriteLine("Enter the name or path of the file to remove from.");
            Console.ForegroundColor = ConsoleColor.White;

            Filename = ReadLine();
            ProcessInput(Filename);

            string filePath = StorageHandler.GetValidFilename(Filename);
            string content = StorageHandler.ReadFile(filePath);

            Console.ForegroundColor = oldConsoleColor;
            WriteLine($"{filePath}: \n{content}");
            WriteLine("Enter the content to remove.");

            Console.ForegroundColor = ConsoleColor.White;
            string contentToRemove = ReadLine();
            Console.ForegroundColor = oldConsoleColor;

            StorageHandler.WriteFile(filePath, content);
        }

        public void StartDeleteDialogue()
        {
            ConsoleColor oldConsoleColor = Console.ForegroundColor;

            WriteLine("Enter the name or path of the file to delete.");

            Console.ForegroundColor = ConsoleColor.White;

            Filename = ReadLine();
            ProcessInput(Filename);
            string filePath = StorageHandler.GetValidFilename(Filename);

            Console.ForegroundColor = oldConsoleColor;

            if (StorageHandler.Delete(filePath))
            {
                WriteLine($"{filePath} deleted.");
            } else
            {
                WriteLine($"Failed to delete {filePath}.");
            }
        }

        public void StartEndDialogue() // Runs when one command has finished
        {
            ConsoleColor color = Console.ForegroundColor;

            WriteLine("Enter: 'Continue' or 'Exit'");

            Console.ForegroundColor = ConsoleColor.White;

            string input = ReadLine();
            ProcessInput(input);
            Console.ForegroundColor = color;
        }

        private void StartInputInvalidDialogue()
        {
            ConsoleColor color = Console.ForegroundColor;

            WriteLine("Input was invalid. Please enter it again.");

            Console.ForegroundColor = ConsoleColor.White;

            string input = ReadLine();
            ProcessInput(input);
            Console.ForegroundColor = color;
        }

        private void ProcessInput(string input) // Not working in StartEndDialogue() method or in CreateCommandDialogue(method)
        {
            switch (input)
            {
                case null:
                case "":
                case " ":
                    StartInputInvalidDialogue();
                    break;
            }

            switch (input)
            {
                case "E":
                case "e":
                case "Exit":
                case "exit":
                    Console.ResetColor();
                    Environment.Exit(0);
                    break;
            }
        }

        public void WriteLine(string text)
        {
            Out.WriteLine(text);
        }

        public void WriteLine(float value)
        {
            Out.WriteLine(value);
        }

        public void Write(string text)
        {
            Out.Write(text);
        }

        public void Write(float value)
        {
            Out.Write(value);
        }

        public string ReadLine()
        {
            string input = In.ReadLine();

            if (!string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                return "\0";
            }
        }

        public char Read()
        {
            char input = (char)In.Read();

            if (!char.IsWhiteSpace(input))
            {
                return input;
            }
            else
            {
                return '\0';
            }
        }

    }
}
