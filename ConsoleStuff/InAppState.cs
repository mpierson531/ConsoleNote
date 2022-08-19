using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStuff;

public class InAppState
{
    private string _CurrentDirectory;
    private string Command;
    private bool FileNameHasTXT;
    private AppState appState;
    public InAppState(string currentDirectory)
    {
        _CurrentDirectory = currentDirectory;
        //Command = command;

        appState = AppState.Introduction;
        Introduction();
    }

    public enum AppState
    {
        Introduction, PreCreateWrite, CommandNull, FileNameNull, ReadyForNext, CreatingWriting
    }

    public enum AppModifier
    {
        IsReady, CommandNull, FileNameNull, Pre
    }

    void Introduction()
    {
        string fileName = String.Empty;
        //HandlingState(fileName, AppModifier.IsReady);

        IntroDialogue(fileName);
        HandlingState(fileName, AppModifier.IsReady);

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
        CommandDialogue();

        CommandCreateDialogue(fileName);

        CommandWriteDialogue(fileName);

        //HandlingState(fileName, AppModifier.IsReady);      **ADD AGAIN ONCE APP IS WORKING**
    }

    private void CommandDialogue() // Also serves as a "CommandNullDialogue"
    {
        Console.WriteLine("Please enter a command. You may either 'Create' or 'Write'.");
        Command = Console.ReadLine();
    }

    private void CommandCreateDialogue(string fileName)
    {
        if (Command.Equals("Create", StringComparison.CurrentCultureIgnoreCase))
        {
            Console.WriteLine("Please enter the name of the file to create.");
            fileName = Console.ReadLine();
        }
    }

    private void CommandWriteDialogue(string fileName)
    {
        if (Command.Equals("Write", StringComparison.CurrentCultureIgnoreCase))
        {
            Console.WriteLine("Please enter the name of the file to write to.");
            fileName = Console.ReadLine();
        }
    }

    private void FileNameNullDialogue(string fileName)
    {
        Console.WriteLine("Please enter a file name.");
        fileName = Console.ReadLine();
    }

    void HandlingState(string fileName, AppModifier modifier) // Handles what state the program is in
    {
        appState = (appState, modifier) switch
        {
            (AppState.Introduction, AppModifier.IsReady) => AppState.PreCreateWrite,
            (AppState.PreCreateWrite, AppModifier.IsReady) => AppState.CreatingWriting,
            (AppState.PreCreateWrite, AppModifier.CommandNull) => AppState.CommandNull,
            (AppState.PreCreateWrite, AppModifier.FileNameNull) => AppState.FileNameNull,
            (AppState.CommandNull, AppModifier.IsReady) => AppState.CreatingWriting,
            (AppState.FileNameNull, AppModifier.IsReady) => AppState.CreatingWriting,
            (AppState.CreatingWriting, AppModifier.IsReady) => AppState.ReadyForNext,
            (AppState.ReadyForNext, AppModifier.IsReady) => AppState.Introduction
            /*(AppState.PreCreateWrite, AppModifier.CommandExecuted) => AppState.ReadyForNext,*/
            // Note to self: Add second dialogue for if user wants to create/write again
        };

        StateTransition(fileName/*, modifier*/);
    }

    void StateTransition(string fileName/*, AppModifier modifier*/) // Handles transition of one state to another, i.e. Introduction - PreCreateWrite
    {
        if (appState == AppState.CommandNull)
        {
            CommandDialogue();
            HandlingState(fileName, AppModifier.IsReady);
        }
        else if (appState == AppState.FileNameNull)
        {
            FileNameNullDialogue(fileName);
            HandlingState(fileName, AppModifier.IsReady);
        }
        else if (appState == AppState.PreCreateWrite)
        {
            HandlingState(fileName, AppModifier.IsReady);
        }

        if (appState == AppState.CreatingWriting)
        {
            CreatingWriting(fileName);
            HandlingState(fileName, AppModifier.IsReady);
        }

        if (appState == AppState.ReadyForNext)
        {
            HandlingState(fileName, AppModifier.IsReady);
        }

        if (appState == AppState.Introduction)
        {
            HandlingState(fileName, AppModifier.IsReady);
        }

        /*if (appState == AppState.Introduction)
        {
            GC.Collect();
            Introduction();
        }*/

        /*if (appState == AppState.PreCreateWrite && modifier == AppModifier.IsReady)
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
            CreateFile(fileName);
            //DoesFileNameHaveTXT(fileName);
            /*if (FileNameHasTXT)
            {
                CreateFile(@fileName);
            }
            else
            {
                CreateFile(@fileName + ".txt");
            }*/
        }
        else if (Command.Equals("Write", StringComparison.CurrentCultureIgnoreCase))
        {
            WriteToFile(@fileName);
        }
    }

    public void CreateFile(string fileName) // Creates files
    {
        File.Create(_CurrentDirectory + @$"\{fileName}" + ".txt");
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
