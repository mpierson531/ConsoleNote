namespace ConsoleStuff;

public class ConsoleNote
{
    public static void Main(string[] args)
    {
        Directory.SetCurrentDirectory("TestDirectory");
        GlobalStates statesClass = new GlobalStates(args);
    }
}