namespace ConsoleStuff;

public class GlobalStates
{
    private List<string> content;
    private GlobalState globalState;

    public GlobalStates(string[] args)
    {
        globalState = GlobalState.DetectingState;
        string currentDirectory = Environment.CurrentDirectory;

        DetectState(args, currentDirectory);
    }

    private enum GlobalState
    {
        DetectingState, InAppState, ArgumentState, Exiting
    }

    private enum GlobalModifier
    {
        ArgsNull, ArgsNotNull, Exit
    }

    private void DetectState(string[] args, string currentDirectory)
    {
        if (args == null || args.Length < 2)
        {
            HandleState(args, GlobalModifier.ArgsNull, currentDirectory);
        }
        else
        {
            HandleState(args, GlobalModifier.ArgsNotNull, currentDirectory);
        }
    }

    private void HandleState(string[] args, GlobalModifier globalModifier, string currentDirectory)
    {
        globalState = (globalState, globalModifier) switch
        {
            (GlobalState.DetectingState, GlobalModifier.ArgsNull) => GlobalState.InAppState,
            (GlobalState.DetectingState, GlobalModifier.ArgsNotNull) => GlobalState.ArgumentState,
            (GlobalState.ArgumentState, GlobalModifier.ArgsNull) => GlobalState.InAppState,
            (GlobalState.InAppState, GlobalModifier.ArgsNotNull) => GlobalState.ArgumentState,
            (GlobalState.ArgumentState, GlobalModifier.Exit) => GlobalState.Exiting,
            (GlobalState.InAppState, GlobalModifier.Exit) => GlobalState.Exiting
        };

        GlobalStateTransition(args, currentDirectory);
    }

    private void GlobalStateTransition(string[] args, string currentDirectory)
    {
        if (globalState == GlobalState.InAppState)
        {
            InAppState inApp = new InAppState();
            HandleState(args, GlobalModifier.Exit, currentDirectory);
        }
        else if (globalState == GlobalState.ArgumentState)
        {
            content = args.ToList();
            ArgumentState argState = new ArgumentState(content);
            HandleState(args, GlobalModifier.Exit, currentDirectory);
        }
        else if (globalState == GlobalState.Exiting)
        {
            Environment.Exit(0);
        }
    }
}