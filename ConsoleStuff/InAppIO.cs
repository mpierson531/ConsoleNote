using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNote
{
    internal class InAppIO
    {
        public string Filename { get; private set; }
        public string Command { get; private set; }

        public void Start()
        {
            Command = GetCommand();
            ProcessInput(Command);
        }

        private string GetCommand()
        {
            Logger.WriteLine("Please enter: 'Create', 'Write', 'Open', 'Remove', or 'Delete'.");

            string command = Logger.ReadLine();
            ProcessInput(command);

            return command.Trim();
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
            Logger.WriteLine("Enter the name or path of the file to create.");
            Filename = Logger.ReadLine();
            ProcessInput(Filename);
            Filename = Filename.Trim();

            string filePath = StorageHandler.GetValidFilename(Filename);
            StorageHandler.CreateFile(filePath).Close();
            Thread.Sleep(200);

            if (File.Exists(filePath))
            {
                Logger.WriteLine($"{filePath} created.");
            }
            else
            {
                Logger.Error($"Failed to create {filePath}");
            }
        }

        public void StartWriteDialogue()
        {
            Logger.WriteLine("Please enter the name of the file to write to.");
            Filename = Logger.ReadLine();
            ProcessInput(Filename);
            Filename = Filename.Trim();
            string filePath = StorageHandler.GetValidFilename(Filename);

            if (!StorageHandler.Exists(filePath))
            {
                Logger.WriteLine($"{filePath} doesn't exist. Create then write to it? Enter y/n.");
                string answer = Logger.ReadLine();
                ProcessInput(answer.ToString());

                if (answer.Trim().Equals("y"))
                {
                    StorageHandler.CreateFile(filePath).Close();
                } else
                {
                    return;
                }
            }

            Logger.WriteLine($"Enter the content to write to {Filename}.");
            string content = Logger.ReadLine();
            ProcessInput(content);

            if (!StorageHandler.WriteFile(filePath, content, false))
            {
                Logger.Error("Failed to write to " + filePath);
                return;
            }

            Logger.WriteLine($"Content written to '{filePath}'");
            StartEndDialogue();
        }

        public void StartOpenDialogue()
        {
            Logger.WriteLine("Enter the name or path of the file to open.");

            Filename = Logger.ReadLine();
            ProcessInput(Filename);
            Filename = Filename.Trim();

            string filePath = StorageHandler.GetValidFilename(Filename);
            string content = StorageHandler.ReadFile(filePath);

            if (content == "\0")
            {
                Logger.Error("Failed to open " + filePath);
                return;
            }

            Logger.WriteLine($"{filePath}: \n{content}");
        }

        public void StartRemoveDialogue()
        {
            Logger.WriteLine("Enter the name or path of the file to remove from.");

            Filename = Logger.ReadLine();
            ProcessInput(Filename);
            Filename = Filename.Trim();

            string filePath = StorageHandler.GetValidFilename(Filename);
            string content = StorageHandler.ReadFile(filePath);

            if (content == "\0")
            {
                Logger.Error($"Failed to access {filePath}.");
                return;
            }

            Logger.WriteLine($"{filePath}: \n{content}");
            Logger.WriteLine("Enter the content to remove.");
            string contentToRemove = Logger.ReadLine();
            ProcessInput(contentToRemove);

            var contentToRemoveMatches = Parser.Parse(contentToRemove, @"\w+");
            string leftoverContent = Parser.Replace(content, contentToRemoveMatches, "");

            if (!StorageHandler.WriteFile(filePath, leftoverContent, false))
            {
                Logger.Error("Failed to access " + filePath);
                return;
            }

            Console.WriteLine("Content removed. New file content: ");
            Console.WriteLine(leftoverContent);
        }

        public void StartDeleteDialogue()
        {
            Logger.WriteLine("Enter the name or path of the file to delete.");

            Filename = Logger.ReadLine();
            ProcessInput(Filename);
            Filename = Filename.Trim();
            string filePath = StorageHandler.GetValidFilename(Filename);

            if (StorageHandler.Delete(filePath))
            {
                Logger.WriteLine($"{filePath} deleted.");
            } else
            {
                Logger.Error($"Failed to delete {filePath}.");
            }
        }

        public void StartEndDialogue() // Runs when one command has finished
        {
            Logger.WriteLine("Process finished.");
        }

        private void StartInputInvalidDialogue()
        {
            Logger.WriteLine("Input was invalid. Please enter it again.");
            string input = Logger.ReadLine();
            ProcessInput(input);
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

            switch (input.Trim())
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
    }
}