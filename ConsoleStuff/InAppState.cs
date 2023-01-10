using ConsoleNote;
using System.Diagnostics;

namespace ConsoleStuff;

public class InAppState
{
    private string Command;
    private AppState appState;
    private string FileName = String.Empty;
    private IO IO;
    private StringComparison IgnoreCase = StringComparison.InvariantCultureIgnoreCase;

    public InAppState()
    {
        IO = new IO();
        ChangeState(AppState.Start);
    }

    private enum AppState
    {
        Start, Running, Done
    }

    private void ChangeState(AppState state) // Handles transition of one state to another, like Running to PreCreateWrite
    {
        if (state == AppState.Start)
        {
            IO.Start();
            IO.RunCommand();
            ChangeState(AppState.Running);
        }

        if (state == AppState.Running)
        {
            IO.StartEndDialogue();
            ChangeState(AppState.Done);
        }
        else if (state == AppState.Done)
        {
            ChangeState(AppState.Start);
        }
    }
}