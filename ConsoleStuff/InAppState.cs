using ConsoleStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNote
{
    public class InAppState : StatesClass, IFileInterface
    {
        private AppState appState;
        public InAppState() : base()
        {
            appState = AppState.Introduction;
            Introduction();
        }

        public enum AppState
        {
            Introduction, CreateWrite, CommandNull, FileNameNull, ReadyForNext, CreatingWriting
        }

        public enum AppModifier
        {
            IsReady, CommandNull, FileNameNull, CommandExecuted
        }


        void Introduction()
        {
            string fileName = String.Empty;

            IntroDialogue(fileName);

            bool IsCommandNull()
            {
                if (String.IsNullOrEmpty(Command))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            bool IsFileNameNull()
            {
                if (String.IsNullOrEmpty(fileName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (IsCommandNull())
            {
                HandlingState(fileName, AppModifier.CommandNull);
            }
            else if (IsFileNameNull())
            {
                HandlingState(fileName, AppModifier.FileNameNull);
            }
            else
            {
                HandlingState(fileName, AppModifier.IsReady);
            }
        }

        void IntroDialogue(string fileName) // Intro dialogue for greeting user and capturing fileName and Command
        {
            Console.WriteLine("Please enter a command. You may either 'Create' or 'Write'.");
            Command = Console.ReadLine();

            if (Command.Equals("Create", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Please enter the name of the file to create.");
                fileName = Console.ReadLine();
            }
            else if (Command.Equals("Write", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Please enter the name of the file to write to.");
                fileName = Console.ReadLine();
            }
        }

        void HandlingState(string fileName, AppModifier modifier) // Handles what state the program is in
        {
            appState = (appState, modifier) switch
            {
                (AppState.Introduction, AppModifier.IsReady) => AppState.CreateWrite,
                (AppState.CreateWrite, AppModifier.CommandNull) => AppState.CommandNull,
                (AppState.CreateWrite, AppModifier.FileNameNull) => AppState.FileNameNull,
                (AppState.CreateWrite, AppModifier.IsReady) => AppState.CreatingWriting,
                (AppState.CreateWrite, AppModifier.CommandExecuted) => AppState.ReadyForNext,
                (AppState.ReadyForNext, AppModifier.IsReady) => AppState.CreateWrite
                // Note to self: Add second dialogue for if user wants to create/write again
            };

            StateTransition(fileName, modifier);
        }

        void StateTransition(string fileName, AppModifier modifier) // Handles transition of one state to another, i.e. Introduction - CreateWrite
        {
            if (appState == AppState.CommandNull || appState == AppState.FileNameNull)
            {
                IntroDialogue(fileName);
            }
            else if (appState == AppState.CreateWrite && modifier == AppModifier.IsReady)
            {

            }

            /*if (appState == AppState.CreateWrite && modifier == AppModifier.IsReady)
            {
                Console.WriteLine("Please enter a command. You may either 'Create' or 'Write'.");
                Command = Console.ReadLine();
            }
            else if (appState == AppState.Introduction && modifier == AppModifier.FileNameNull)
            {
                Console.WriteLine("Please enter a file name");
                fileName = Console.ReadLine();
            }
            else if ()*/

        }

        /*void TransitionToContent(string fileName)
        {
            if (Command.Equals("Create", StringComparison.CurrentCultureIgnoreCase))
            {
                CreateFile(fileName);
                Console.WriteLine($"{fileName} created.");
            }
            else if (Command.Equals("Write", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine($"Please enter the content to write to {fileName}.");
                WriteToFile(fileName);
            }
        }*/

        public void CreatingWriting(string fileName)
        {
            if (Command.Equals("Create", StringComparison.CurrentCultureIgnoreCase))
            {
                DoesFileNameHaveTXT(fileName);
                if (FileNameHasTXT)
                {
                    CreateFile(@fileName);
                }
                else
                {
                    CreateFile(@fileName + ".txt");
                }
            }
            else
            {
                WriteToFile(@fileName);
            }
        }

        public void CreateFile(string fileName) // Creates files
        {
            File.Create(CurrentDirectory + @$"\{fileName}");
        }

        public void WriteToFile(string fileName) // Writes to files (with string)
        {
            string content = Console.ReadLine();
            File.AppendAllText(fileName, content);
        }

        public void DoesFileNameHaveTXT(string fileName) // Checks to see if fileName has ".txt"
        {
            if (fileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
            {
                FileNameHasTXT = true;
            }
            else
            {
                FileNameHasTXT = false;
            }
        }
    }
}
