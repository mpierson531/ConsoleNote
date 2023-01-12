namespace ConsoleNote;

public class ConsoleNote
{
    public static void Main(string[] args)
    {
        Directory.SetCurrentDirectory("TestDirectory");
        Console.ForegroundColor = ConsoleColor.Green;
        GlobalStates statesClass = new GlobalStates(args);
    }
}