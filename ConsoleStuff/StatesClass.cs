using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStuff;

public class StatesClass
{
    private List<string> content;
    private GlobalState globalState;

    public StatesClass(string[] args)
    {
        globalState = GlobalState.DetectingState;
        string currentDirectory = Environment.CurrentDirectory;

        DetectingState(args, currentDirectory);
        //GlobalStateTransition(args, currentDirectory);
    }

    public enum GlobalState
    {
        DetectingState, InAppState, ArgumentState, Exiting
    }

    public enum GlobalModifier
    {
        ArgsNull, ArgsNotNull, Exit
    }

    public void DetectingState(string[] args, string currentDirectory)
    {
        static bool IsArgsNull(string[] args)
        {

            if (args is null || args.Length < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (IsArgsNull(args))
        {
            StateHandling(args, GlobalModifier.ArgsNull, currentDirectory);
        }
        else
        {
            StateHandling(args, GlobalModifier.ArgsNotNull, currentDirectory);
        }
    }

    public void StateHandling(string[] args, GlobalModifier globalModifier, string currentDirectory)
    {
        globalState = (globalState, globalModifier) switch
        {
            (GlobalState.DetectingState, GlobalModifier.ArgsNull) => GlobalState.InAppState,
            (GlobalState.DetectingState, GlobalModifier.ArgsNotNull) => GlobalState.ArgumentState,
            (GlobalState.ArgumentState, GlobalModifier.ArgsNull) => GlobalState.InAppState,
            (GlobalState.InAppState, GlobalModifier.ArgsNotNull) => GlobalState.ArgumentState,
            (GlobalState.ArgumentState, GlobalModifier.Exit) => GlobalState.Exiting
        };

        GlobalStateTransition(args, currentDirectory);
    }

    public void GlobalStateTransition(string[] args, string currentDirectory)
    {
        if (globalState == GlobalState.InAppState)
        {
            InAppState inApp = new InAppState(currentDirectory);
            StateHandling(args, GlobalModifier.Exit, currentDirectory);
        }
        else if (globalState == GlobalState.ArgumentState)
        {
            content = args.ToList();
            ArgumentState argState = new ArgumentState(content);
            StateHandling(args, GlobalModifier.Exit, currentDirectory);
        }
        else if (globalState == GlobalState.Exiting)
        {
            Environment.Exit(0);
        }
    }
}