using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleStuff;

public class InAppState
{
    private string _CurrentDirectory;
    private string Command;
    private bool FileNameHasTXT;
    private AppState appState;
    private string FileName = String.Empty;
    public InAppState(string currentDirectory)
    {
        _CurrentDirectory = currentDirectory;
        //Command = command;

        appState = AppState.Introduction;
        Introduction();
    }

    public enum AppState
    {
        Introduction, PreCreateWrite, CommandNull, FileNameNull, ReadyForNext, CreatingWriting, Done
    }

    public enum AppModifier
    {
        IsReady, CommandNull, FileNameNull, Done
    }

    void Introduction()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        IntroDialogue();
        HandlingState(AppModifier.IsReady);
        StateTransition();

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
            if (String.IsNullOrEmpty(FileName))
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
            HandlingState(AppModifier.CommandNull);
            StateTransition();
        }
        else if (IsFileNameNull())
        {
            HandlingState(AppModifier.FileNameNull);
            StateTransition();
        }
        else
        {
            HandlingState(AppModifier.IsReady);
            StateTransition();
        }
    }

    void IntroDialogue() // Intro dialogue for greeting user and capturing FileName and Command
    {
        CommandDialogue();

        CommandCreateDialogue();

        CommandWriteDialogue();

        //HandlingState(FileName, AppModifier.IsReady);      **ADD AGAIN ONCE APP IS WORKING** Might still add, have to test more
    }

    private void CommandDialogue() // Also serves as a "CommandNullDialogue"
    {
        ConsoleColor color = Console.ForegroundColor;

        Console.WriteLine("Please enter a command. You may either 'Create' or 'Write'.");

        Console.ForegroundColor = ConsoleColor.White;

        Command = Console.ReadLine();
        ExitChecking(Command);

        Console.ForegroundColor = color;
    }

    private void CommandCreateDialogue()
    {
        if (Command.Equals("Create", StringComparison.CurrentCultureIgnoreCase))
        {
            ConsoleColor color = Console.ForegroundColor;

            Console.WriteLine("Please enter the name of the file to create.");

            Console.ForegroundColor = ConsoleColor.White;

            FileName = Console.ReadLine();
            ExitChecking(FileName);

            Console.ForegroundColor = color;
        }
    }

    private void CommandWriteDialogue()
    {
        if (Command.Equals("Write", StringComparison.CurrentCultureIgnoreCase))
        {
            ConsoleColor color = Console.ForegroundColor;

            Console.WriteLine("Please enter the name of the file to write to.");

            Console.ForegroundColor = ConsoleColor.White;

            FileName = Console.ReadLine();
            ExitChecking(FileName);

            Console.ForegroundColor = color;
        }
    }

    private void FileNameNullDialogue()
    {
        ConsoleColor color = Console.ForegroundColor;

        Console.WriteLine("Please enter a file name.");

        Console.ForegroundColor = ConsoleColor.White;

        FileName = Console.ReadLine();
        ExitChecking(FileName);

        Console.ForegroundColor = color;
    }

    void HandlingState(AppModifier modifier) // Handles what state the program is in
    {
        appState = (appState, modifier) switch
        {
            (AppState.Introduction, AppModifier.IsReady) => AppState.PreCreateWrite,
            (AppState.PreCreateWrite, AppModifier.IsReady) => AppState.CreatingWriting,
            (AppState.PreCreateWrite, AppModifier.CommandNull) => AppState.CommandNull,
            (AppState.PreCreateWrite, AppModifier.FileNameNull) => AppState.FileNameNull,
            (AppState.CommandNull, AppModifier.IsReady) => AppState.CreatingWriting,
            (AppState.FileNameNull, AppModifier.IsReady) => AppState.CreatingWriting,
            (AppState.CreatingWriting, AppModifier.Done) => AppState.Done,
            (AppState.Done, AppModifier.IsReady) => AppState.Introduction,
            (AppState.CreatingWriting, AppModifier.IsReady) => AppState.ReadyForNext,
            (AppState.ReadyForNext, AppModifier.IsReady) => AppState.Introduction
        };
    }

    void StateTransition() // Handles transition of one state to another, like Introduction to PreCreateWrite
    {
        if (appState == AppState.CommandNull)
        {
            CommandDialogue();
            HandlingState(AppModifier.IsReady);
            StateTransition();
        }
        else if (appState == AppState.FileNameNull)
        {
            FileNameNullDialogue();
            HandlingState(AppModifier.IsReady);
            StateTransition();
        }
        else if (appState == AppState.PreCreateWrite)
        {
            HandlingState(AppModifier.IsReady);
            StateTransition();
        }

        if (appState == AppState.CreatingWriting)
        {
            CreatingWriting();
            HandlingState(AppModifier.Done);
            StateTransition();
        }

        if (appState == AppState.ReadyForNext)
        {
            HandlingState(AppModifier.IsReady);
            StateTransition();
        }

        if (appState == AppState.Introduction)
        {
            Introduction();
        }

        if (appState == AppState.Done)
        {
            Done();
        }
    }


    private void CreatingWriting()
    {
        if (Command.Equals("Create", StringComparison.CurrentCultureIgnoreCase))
        {
            CreateFile();
        }
        else if (Command.Equals("Write", StringComparison.CurrentCultureIgnoreCase))
        {
            WriteToFile();
        }
    }

    public void CreateFile() // Creates files
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        FileStream createdFile;
        string filePath = _CurrentDirectory + @$"\{FileName}" + ".txt";

        if (DoesFileNameHaveTXT())
        {
            filePath = _CurrentDirectory + @$"\{FileName}";
        }

        createdFile = File.Create(filePath);
        createdFile.Close();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
        //Thread.Sleep(500);

        if (File.Exists(filePath))
        {
            Console.WriteLine($"{FileName} created.");
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
        else
        {
            Console.WriteLine($"Failed to create {FileName}");
        }
    }

    public void WriteToFile() // Writes to files (with string)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Console.WriteLine($"Enter the content to write to {FileName}.");

        string content = Console.ReadLine();
        content = InsertNewline(content);
        ExitChecking(content);
        Console.WriteLine(stopwatch.ElapsedMilliseconds);

        File.AppendAllText(FileName + ".txt", content);
        Console.WriteLine(stopwatch.ElapsedMilliseconds);

        Console.WriteLine($"Content written to {FileName}");
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);

    }

    public bool DoesFileNameHaveTXT() // Checks to see if FileName has ".txt"
    {
        if (!FileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private string InsertNewline(string content)
    {
        try
        {
            if (content.Contains("\\n"))
            {
                content = content.Replace("\\n", Environment.NewLine);
            }
        }
        catch (NullReferenceException) { }

        return content;
    }

    private void Done()
    {
        ConsoleColor color = Console.ForegroundColor;

        Console.WriteLine("Enter a new command: 'Continue' or 'Exit'");

        Console.ForegroundColor = ConsoleColor.White;

        string input = Console.ReadLine();
        ExitChecking(input);

        Console.ForegroundColor = color;

        if (input.Equals("Continue", StringComparison.CurrentCultureIgnoreCase))
        {
            HandlingState(AppModifier.IsReady);
            StateTransition();
        }

        ExitChecking(input);

    }

    private void ExitChecking(string input) // Not working in Done() method or in CommandDialogue(method)
    {
        switch (input)
        {
            case "Exit":
                Console.ResetColor();
                Environment.Exit(0);
                break;
            case "exit":
                Console.ResetColor();
                Environment.Exit(0);
                break;
        }
    }
}