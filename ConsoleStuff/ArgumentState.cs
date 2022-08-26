﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConsoleStuff;

internal class ArgumentState
{
    private string SingleStringContent;
    private string CurrentDirectory = Directory.GetCurrentDirectory();
    private readonly StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase;
    private bool IsArgsBig;
    private bool FileNameHasTXT;
    public ArgumentState(List<string> content)
    {
        ///
        /// NEED TO FIGURE OUT WHY IT'S WRITING THE CONTENT TWICE WHEN IsArgsBig IS TRUE. 
        /// 
        /// Figured it out: when ArgumentState was done, it kept returning to StatesClass and running again with same variable values
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

    private void CreatingWriting(string command, string fileName, List<string> content)
    {
        List<string> bigCopyOfContent;
        if (command.Equals("Create", stringComparison))
        {
            CreateFile(fileName);
        }
        else if (command.Equals("Write", stringComparison))
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
        else if (command.Equals("Open", stringComparison))
        {
            OpenFile(fileName);
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

    private void OpenFile(string fileName)
    {
        ConsoleColor color = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Yellow;

        FileStream openStream = File.Open(fileName + ".txt", FileMode.Open, FileAccess.Read);

        StreamReader openReader = new StreamReader(openStream);

        Console.WriteLine($"From '{fileName}':");
        Console.WriteLine("");
        Console.WriteLine(openReader.ReadToEnd());
        Console.WriteLine("");

        openReader.Close();

        Console.ForegroundColor = color;
    }

    private void IsBig(List<string> content)
    {
        if (content.Count > 0)
        {
            IsArgsBig = true;
        }
        else
        {
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

    private void InsertNewline(List<string> bigCopy)
    {
        for (int i = 0; i < bigCopy.Count; i++)
        {
            if (bigCopy[i].Contains("\\n"))
            {
                bigCopy[i] = bigCopy[i].Replace("\\n", Environment.NewLine);
            }

        }
    }

    private string InsertNewline(string SingleStringContent)
    {
        try
        {
            if (SingleStringContent.Contains("\\n"))
            {
                SingleStringContent = SingleStringContent.Replace("\\n", Environment.NewLine);
            }
        } catch (NullReferenceException) { }

        return SingleStringContent;
    }
}