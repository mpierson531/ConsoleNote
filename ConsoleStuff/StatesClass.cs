using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStuff;

public class StatesClass
{
    //string? FileContent;
    public string? Command;
    public string CurrentDirectory;
    public bool IsArgsBig;
    public bool FileNameHasTXT;
    public List<string> content;
    private GlobalState globalState;
    //public GlobalModifier globalModifier;
    //public GlobalState CurrentState { get { return globalState; } }

    static bool IsArgsNullOrEmpty;

    public StatesClass(string[] args)
    {
        globalState = GlobalState.DetectingState;
        CurrentDirectory = Environment.CurrentDirectory;

        DetectingState(args);
        GlobalStateTransition(args);
    }

    public enum GlobalState
    {
        DetectingState, InAppState, ArgumentState
    }

    public enum GlobalModifier
    {
        ArgsNull, ArgsNotNull
    }

    public void DetectingState(string[] args)
    {
        static bool IsArgsNull(string[] args)
        {
            //args.ToList<string>();

            if (args is null || args.Count() <= 1)
            {
                return true;
                //IsArgsNullOrEmpty = true;
            }
            else
            {
                return false;
                //IsArgsNullOrEmpty = false;
            }
        }

        //IsArgsNull(args);

        if (IsArgsNull(args))
        {
            StateHandling(args, GlobalModifier.ArgsNull);
        }
        else
        {
            StateHandling(args, GlobalModifier.ArgsNotNull);
        }

        //Console.ReadLine();
    }

    public void StateHandling(string[] args, GlobalModifier globalModifier)
    {
        globalState = (globalState, globalModifier) switch
        {
            (GlobalState.DetectingState, GlobalModifier.ArgsNull) => GlobalState.InAppState,
            (GlobalState.DetectingState, GlobalModifier.ArgsNotNull) => GlobalState.ArgumentState,
            (GlobalState.ArgumentState, GlobalModifier.ArgsNull) => GlobalState.InAppState,
            (GlobalState.InAppState, GlobalModifier.ArgsNotNull) => GlobalState.ArgumentState
        };

        if (globalState == GlobalState.InAppState)
        {
            InAppState inApp = new InAppState(CurrentDirectory);
        }
        else if (globalState == GlobalState.ArgumentState)
        {
            ArgumentState argState = new ArgumentState(args);
        }

        //GlobalStateTransition(args);

        /*if (globalState.Equals(GlobalState.DetectingState) && globalModifier == GlobalModifier.IsArgsNullOrEmpty)
        {
            globalState = GlobalState.InAppState;
        }
        else if (globalState.Equals(GlobalState.DetectingState) && globalModifier == GlobalModifier.ArgsNotNull)
        {
            globalState = GlobalState.ArgumentState;
        }
        else if (globalState.Equals(GlobalState.ArgumentState) && globalModifier.Equals(GlobalModifier.IsArgsNullOrEmpty))
        {
            globalState = GlobalState.InAppState;
        }
        else if (globalState.Equals(GlobalState.InAppState) && globalModifier.Equals(GlobalModifier.ArgsNotNull))
        {
            globalState = GlobalState.ArgumentState;
        }*/
    }

    public void GlobalStateTransition(string[] args)
    {
        if (globalState == GlobalState.InAppState)
        {
            InAppState inApp = new InAppState(CurrentDirectory);
        }
        else if (globalState == GlobalState.ArgumentState)
        {
            ArgumentState argState = new ArgumentState(args);
        }
        /*else if ((globalState == GlobalState.ArgumentState) && (args == null || args == Array.Empty<string>()))
        {
            //InAppState inApp = new InAppState();
            StateHandling(args, GlobalModifier.ArgsNull);
        }
        else if (globalState == GlobalState.InAppState && (args != null || args != Array.Empty<string>()))
        {
            //ArgumentState argState = new ArgumentState(args);
            StateHandling(args, GlobalModifier.ArgsNotNull);
        }*/


        /*if (globalState == GlobalState.InAppState)
        {
            InAppState inApp = new InAppState();
        }
        else if (globalState is GlobalState.ArgumentState)
        {
            ArgumentState argState = new ArgumentState(args);
        }*/
    }
}

    /*public enum InAppStateModifiers
    {
        CommandNull, IsFileOrCmdNull, NeitherNull
    }

    public enum ArgumentStateModifiers
    {
        CommandNull, IsFileOrCmdNull, NeitherNull
    }*/

    /*public enum Action
    {
        IsArgsNullOrEmpty,
    }*/