namespace ConsoleStuff;

public class ConsoleClass1
{
    static string? FileName;
    static string? FileContent;
    static string? Command;
    static string CurrentDirectory = Environment.CurrentDirectory;
    static bool IsArgsBig;
    static bool FileNameHasTXT;
    static List<string> content;
    public static void Main(string[] args)
    {
        StatesClass statesClass = new StatesClass(args);
    }
}