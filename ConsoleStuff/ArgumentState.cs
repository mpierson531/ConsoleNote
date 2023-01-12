using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleNote;

internal class ArgumentState
{
    private string SingleStringContent;
    private bool IsArgsBig;

    public ArgumentState(List<string> args)
    {
        List<string> Args = args;

        string command = Args[0].Trim();
        string filename = Args[1].Trim();
        string filePath = StorageHandler.GetValidFilename(filename);

        try
        {
            SingleStringContent = Args[2];
        }
        catch (ArgumentOutOfRangeException) { }

        IsBig(Args);
        RunCommand(command, filePath, Args);
    }

    private void RunCommand(string command, string filePath, List<string> content)
    {
        List<string> bigCopyOfContent;

        switch (command)
        {
            case "Create":
            case "create":
                StartFileCreation(filePath);
                break;
            case "Write":
            case "write":
                if (IsArgsBig)
                {
                    bigCopyOfContent = GetArgsContent(content);
                    StorageHandler.WriteFile(filePath, bigCopyOfContent, false);
                }
                else
                {
                    StorageHandler.WriteFile(filePath, SingleStringContent, false);
                }
                break;
            case "Open":
            case "open":
                StartOpenFile(filePath);
                break;
            case "Delete":
            case "delete":
                StartFileDeletion(filePath);
                break;
            case "Remove":
            case "remove":
                if (IsArgsBig)
                {
                    bigCopyOfContent = GetArgsContent(content);
                    RemoveFromFile(filePath, bigCopyOfContent);
                } else
                {
                    RemoveFromFile(filePath, SingleStringContent);
                }
                break;
        }
    }

    private void StartFileCreation(string filename) // Creates files
    {
        string filePath = StorageHandler.GetValidFilename(filename);
        var file = StorageHandler.CreateFile(filePath);
        file.Close();
        Thread.Sleep(250);

        if (File.Exists(filePath))
        {
            Logger.WriteLine($"{filePath} created.");
        }
        else
        {
            Logger.WriteLine($"Failed to create {filePath}");
        }
    }

    private void StartOpenFile(string filename)
    {
        try
        {
            string fileContent = StorageHandler.ReadFile(filename);
            Logger.WriteLine($"From '{filename}': \n");
            Logger.WriteLine(fileContent + "\n");
        }
        catch (FileNotFoundException)
        {
            Logger.Error($"File '{filename}' could not be found.");
        }
    }

    private void StartFileDeletion(string filename) // Not working
    {
        string filePath = StorageHandler.GetValidFilename(filename);

        if (File.Exists(filePath))
        {
            File.Delete(filename);
            Logger.WriteLine(filePath + " deleted.");
        }
        else
        {
            Logger.Error($"'{filePath}' could not be found.");
        }
    }

    private void RemoveFromFile(string filename, List<string> contentToRemove) // Removes any string or line containing "contentToRemove"
    {
        string filePath = StorageHandler.GetValidFilename(filename);
        if (!File.Exists(filePath))
        {
            Logger.Error($"File '{filename}' could not be found.");
            return;
        }

        string fileContent = StorageHandler.ReadFile(filePath);
        string leftoverContent = Parser.Replace(fileContent, contentToRemove, "");
        StorageHandler.WriteFile(filePath, leftoverContent, false);

        Logger.WriteLine("Content removed.");
    }

    private void RemoveFromFile(string filePath, string removalContent)
    {
        if (!File.Exists(filePath)) 
        {
            Logger.Error($"File '{filePath}' could not be found.");
            return;
        }

        string fileContent = StorageHandler.ReadFile(filePath);
        string leftoverContent = Parser.Replace(fileContent, removalContent, "");

        StorageHandler.WriteFile(filePath, leftoverContent, false);

        Logger.WriteLine("Content removed.");
    }

    private void IsBig(List<string> content)
    {
        if (content.Count > 3)
        {
            IsArgsBig = true;
        }
        else
        {
            IsArgsBig = false;
        }
    }

    private List<string> GetArgsContent(List<string> content)
    {
        int fileContentsCounter = 0;
        List<string> fileContents = new List<string>(content.Count - 2);

        for (int i = 2; i < content.Count; i++)
        {
            fileContents.Insert(fileContentsCounter, content[i]);
            fileContentsCounter++;
        }

        return fileContents;
    }
}