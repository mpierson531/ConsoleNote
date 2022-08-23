using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStuff;

internal class ArgumentState
{
    //private string command;
    //private string fileName;
    //private List<string> bigCopy;
    //private string SingleStringContent;
    //private bool IsArgsBig;
    //private bool FileNameHasTXT;
    //private List<string> Content;
    private string SingleStringContent;
    private string CurrentDirectory = Directory.GetCurrentDirectory();
    readonly StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase;
    private bool IsArgsBig;
    private bool FileNameHasTXT;
    public ArgumentState(List<string> content)
    {
        ///
        /// NEED TO FIGURE OUT WHY IT'S WRITING THE CONTENT TWICE WHEN IsArgsBig IS TRUE
        /// 

        List<string> Content = content;

        string command = Content[0];
        string fileName = Content[1];
        try
        {
            SingleStringContent = Content[2];
        }
        catch (ArgumentOutOfRangeException) { }

        IsBig(Content);
        DoesFileNameHaveTXT(fileName);
        CreatingWriting(command, fileName, Content); 
    }

    private void CreatingWriting(string Command, string fileName, List<string> content)
    {
        List<string> bigCopyOfContent;
        if (Command.Equals("Create", stringComparison))
        {
            CreateFile(fileName);
        }
        else if (Command.Equals("Write", stringComparison))
        {
            if (IsArgsBig)
            {
                bigCopyOfContent = Extensions.BigCopy(content);
                WriteToFile(fileName, bigCopyOfContent);
            }
            else
            {
                WriteToFile(fileName, content);
            }
        }
    }

    private void CreateFile(string fileName) // Creates files
    {
        FileStream createdFile;
        string filePath = CurrentDirectory + @$"\{fileName}" + ".txt";

        if (FileNameHasTXT)
        {
            filePath = CurrentDirectory + @$"\{fileName}";
        }

        createdFile = File.Create(filePath);
        createdFile.Close();
        Thread.Sleep(250);

        if (File.Exists(filePath))
        {
            Console.WriteLine($"{fileName} created.");
        }
        else
        {
            Console.WriteLine($"Failed to create {fileName}");
        }
    }

    private void WriteToFile(string fileName, List<string> bigCopy) // Also inserts spaces and newlines
    {
        /*string filePath = CurrentDirectory + @$"\{fileName}" + ".txt";

        if (FileNameHasTXT)
        {
            filePath = CurrentDirectory + @$"\{fileName}";
        }*/

        if (IsArgsBig && FileNameHasTXT)
        {
            InsertNewline(bigCopy);
            Extensions.SpaceInsert(bigCopy);

            foreach (string i in bigCopy)
            {
                File.AppendAllText(fileName, i);
            }
        }
        else if (IsArgsBig && !FileNameHasTXT)
        {
            InsertNewline(bigCopy);
            Extensions.SpaceInsert(bigCopy);

            foreach (string j in bigCopy)
            {
                File.AppendAllText(fileName + ".txt", j);
            }
        }
        else if (!IsArgsBig && FileNameHasTXT)
        {
            SingleStringContent = InsertNewline(SingleStringContent);
            Extensions.SpaceInsert(SingleStringContent);
            File.AppendAllText(fileName, SingleStringContent);
        }
        else if (!IsArgsBig && !FileNameHasTXT)
        {
            SingleStringContent = InsertNewline(SingleStringContent);             
            Extensions.SpaceInsert(SingleStringContent);
            File.AppendAllText(fileName + ".txt", SingleStringContent);
        }
    }

    private void IsBig(List<string> content)
    {
        if (content.Count > 3)
        {
            //bigCopy = Extensions.SpaceInsert(bigCopy);
            IsArgsBig = true;
        }
        else
        {
            //bigCopy = Extensions.SpaceInsert(bigCopy);
            IsArgsBig = false;
        }
    }

    public void DoesFileNameHaveTXT(string fileName) // Checks to see if fileName has ".txt"
    {
        if (!fileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
        {
            FileNameHasTXT = false;
        }
        else
        {
            FileNameHasTXT = true;
        }
    }

    private void InsertNewline(List<string> args)
    {
        for (int i = 0; i < args.Count; i++)
        {
            if (args[i].Contains("\\n"))
            {
                args[i] = "\r\n"; // Note to self: rename List<string> args to bigCopy
            }

            //argsIndexCounter++;
        }
        //return args;
    }

    private string InsertNewline(string SingleStringContent)
    {
        try
        {
            if (SingleStringContent.Contains("\\n"))
            {
                SingleStringContent = "\r\n";
            }
        } catch (NullReferenceException) { }

        return SingleStringContent;
    }
}
