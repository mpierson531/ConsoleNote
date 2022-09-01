using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace ConsoleStuff;

public class InAppState
{
    private string Command;
    private AppState appState;
    private string FileName = String.Empty;
    public InAppState(string currentDirectory)
    {
        appState = AppState.Introduction;
        Introduction();
    }

    private enum AppState
    {
        Introduction, PreCreateWrite, CommandNull, FileNameNull, ReadyForNext, CreatingWriting, Done
    }

    private enum AppModifier
    {
        IsReady, CommandNull, FileNameNull, Done
    }

    private void Introduction()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        IntroDialogue();
        HandlingState(AppModifier.IsReady); // Changing state to PreCreateWrite
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

    private void IntroDialogue() // Intro dialogue for greeting user and capturing FileName and Command
    {
        CommandDialogue();

        CreateDialogue();

        WriteDialogue();

        OpenDialogue();

        DeleteDialogue();
    }

    private void CommandDialogue() // Also serves as a "CommandNullDialogue"
    {
        ConsoleColor color = Console.ForegroundColor;

        Console.WriteLine("Please enter a command: You can 'Open', 'Write', 'Create', or 'Delete'.");

        Console.ForegroundColor = ConsoleColor.White;

        Command = Console.ReadLine().Trim();
        ExitChecking(Command);

        Console.ForegroundColor = color;
    }

    private void CreateDialogue() // When Command == Create
    {
        if (Command.Equals("Create", StringComparison.CurrentCultureIgnoreCase))
        {
            ConsoleColor color = Console.ForegroundColor;

            Console.WriteLine("Please enter the name of the file to create.");

            Console.ForegroundColor = ConsoleColor.White;

            FileName = Console.ReadLine().Trim();
            ExitChecking(FileName);

            Console.ForegroundColor = color;
        }
    }

    private void WriteDialogue() // When Command == Write
    {
        if (Command.Equals("Write", StringComparison.CurrentCultureIgnoreCase))
        {
            ConsoleColor color = Console.ForegroundColor;

            Console.WriteLine("Please enter the name of the file to write to.");

            Console.ForegroundColor = ConsoleColor.White;

            FileName = Console.ReadLine().Trim();
            ExitChecking(FileName);

            Console.ForegroundColor = color;
        }
    }

    private void OpenDialogue() // When Command == Open
    {
        switch (Command)
        {
            case "Open":
            case "open":
                ConsoleColor color = Console.ForegroundColor;

                Console.WriteLine("Enter the name or path of the file to read.");

                Console.ForegroundColor = ConsoleColor.White;

                FileName = Console.ReadLine().Trim();
                ExitChecking(FileName);

                Console.ForegroundColor = color;
                break;
        }
    }

    private void DeleteDialogue()
    {
        switch (Command)
        {
            case "Delete":
            case "delete":
                ConsoleColor color = Console.ForegroundColor;

                Console.WriteLine("Enter the name or path of the file to delete.");

                Console.ForegroundColor = ConsoleColor.White;

                FileName = Console.ReadLine().Trim();
                ExitChecking(FileName);

                Console.ForegroundColor = color;
                break;
        }
    }

    private void FileNameNullDialogue()
    {
        ConsoleColor color = Console.ForegroundColor;

        Console.WriteLine("Please enter a file name.");

        Console.ForegroundColor = ConsoleColor.White;

        FileName = Console.ReadLine().Trim();
        ExitChecking(FileName);

        Console.ForegroundColor = color;
    }

    private void HandlingState(AppModifier modifier) // Handles what state the program is in
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

    private void StateTransition() // Handles transition of one state to another, like Introduction to PreCreateWrite
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
        switch (Command)
        {
            case "Create":
            case "create":
                CreateFile();
                break;
            case "Write":
            case "write":
                WriteToFile();
                break;
            case "Open":
            case "open":
                OpenFile();
                break;
            case "Delete":
            case "delete":
                DeleteFile();
                break;
        }
    }

    private void CreateFile() // Creates files
    {
        ConsoleColor color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;

        FileStream createdFile;
        string filePath;

        if (DoesFileNameHaveTXT())
        {
            filePath = FileName;
        }
        else
        {
            filePath = FileName + ".txt";
        }

        createdFile = File.Create(filePath);
        createdFile.Close();
        Thread.Sleep(200);

        if (File.Exists(filePath))
        {
            Console.WriteLine($"{FileName} created.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed to create {FileName}");
        }

        Console.ForegroundColor = color;
    }

    private void WriteToFile() // Writes to files (with string)
    {
        ConsoleColor color = Console.ForegroundColor;

        Console.WriteLine($"Enter the content to write to {FileName}.");

        Console.ForegroundColor = ConsoleColor.White;

        string content = Console.ReadLine();
        content = InsertNewline(content);
        ExitChecking(content);

        string filePath;

        if (DoesFileNameHaveTXT())
        {
            filePath = FileName;
        }
        else
        {
            filePath = FileName + ".txt";
        }
        
        File.AppendAllText(filePath, content);
        Console.ForegroundColor = color;
        Console.WriteLine($"Content written to '{FileName}'");
    }

    private void OpenFile() // Reads the contents of .txt files to the console
    {
        ConsoleColor color = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Yellow;

        string filePath;
        if (DoesFileNameHaveTXT())
        {
            filePath = FileName;
        }
        else
        {
            filePath = FileName + ".txt";
        }

        try
        {
            FileStream openStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            StreamReader openReader = new StreamReader(openStream);

            Console.WriteLine($"From '{FileName}':");
            Console.WriteLine("");
            Console.WriteLine(openReader.ReadToEnd());
            Console.WriteLine("");

            openReader.Close();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File '{FileName}' could not be found.");
        }


        Console.ForegroundColor = color;
    }

    private void DeleteFile() // Not working
    {
        string filePath;

        if (DoesFileNameHaveTXT())
        {
            filePath = FileName;
        }
        else
        {
            filePath = FileName + ".txt";
        }

        if (File.Exists(filePath))
        {
            File.Delete(FileName + ".txt");
            Console.WriteLine(FileName + " deleted.");
        }
        else
        {
            Console.WriteLine($"'{filePath}' could not be found.");
        }
    }

    private bool DoesFileNameHaveTXT() // Checks to see if FileName has ".txt"
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

    private void Done() // Runs when one command has finished
    {
        ConsoleColor color = Console.ForegroundColor;

        Console.WriteLine("Enter: 'Continue' or 'Exit'");

        Console.ForegroundColor = ConsoleColor.White;

        string input = Console.ReadLine().Trim();
        ExitChecking(input);

        Console.ForegroundColor = color;

        switch (input)
        {
            case "Continue":
            case "continue":
            case "Exit":
            case "exit":
                HandlingState(AppModifier.IsReady);
                StateTransition();
                break;
        }

        ExitChecking(input);
    }

    private void ExitChecking(string input) // Not working in Done() method or in CommandDialogue(method)
    {
        switch (input)
        {
            case "Exit":
            case "exit":
                Console.ResetColor();
                Environment.Exit(0);
                break;
        }
    }
}