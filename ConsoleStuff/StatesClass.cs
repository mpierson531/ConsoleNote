using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNote
{
    public class StatesClass
    {
        //string? FileContent;
        public string? Command;
        public string CurrentDirectory;
        public bool IsArgsBig = false;
        public bool FileNameHasTXT;
        public List<string> content;
        private GlobalState globalState;
        //public GlobalModifier globalModifier;
        public GlobalState CurrentState { get { return globalState; } }

        //static bool ArgsNull;

        public StatesClass([Optional] string[] args)
        {
            globalState = GlobalState.DetectingState;
            CurrentDirectory = Environment.CurrentDirectory;

            DetectingState(args);
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
                if (args == null)
                {
                    return true;
                    //return ArgsNull = true;
                }
                else
                {
                    return false;
                    //return ArgsNull = false;
                }
            }

            if (IsArgsNull(args))
            {
                ModifierStatements(args, GlobalModifier.ArgsNull);
            }
            else
            {
                ModifierStatements(args, GlobalModifier.ArgsNotNull);
            }

        }

        public void ModifierStatements(string[] args, GlobalModifier globalModifier)
        {
            globalState = (globalState, globalModifier) switch
            {
                (GlobalState.DetectingState, GlobalModifier.ArgsNull) => GlobalState.InAppState,
                (GlobalState.DetectingState, GlobalModifier.ArgsNotNull) => GlobalState.ArgumentState,
                (GlobalState.ArgumentState, GlobalModifier.ArgsNull) => GlobalState.InAppState,
                (GlobalState.InAppState, GlobalModifier.ArgsNotNull) => GlobalState.ArgumentState,
            };

            GlobalStateTransition(args, globalModifier);

            /*if (globalState.Equals(GlobalState.DetectingState) && globalModifier == GlobalModifier.ArgsNull)
            {
                globalState = GlobalState.InAppState;
            }
            else if (globalState.Equals(GlobalState.DetectingState) && globalModifier == GlobalModifier.ArgsNotNull)
            {
                globalState = GlobalState.ArgumentState;
            }
            else if (globalState.Equals(GlobalState.ArgumentState) && globalModifier.Equals(GlobalModifier.ArgsNull))
            {
                globalState = GlobalState.InAppState;
            }
            else if (globalState.Equals(GlobalState.InAppState) && globalModifier.Equals(GlobalModifier.ArgsNotNull))
            {
                globalState = GlobalState.ArgumentState;
            }*/
        }

        public void GlobalStateTransition(string[] args, GlobalModifier globalModifier)
        {
            if (globalState.Equals(GlobalState.DetectingState) && globalModifier == GlobalModifier.ArgsNull)
            {
                InAppState inApp = new InAppState();
            }
            else if (globalState.Equals(GlobalState.DetectingState) && globalModifier == GlobalModifier.ArgsNotNull)
            {
                ArgumentState argState = new ArgumentState(args);
            }
            else if (globalState.Equals(GlobalState.ArgumentState) && globalModifier.Equals(GlobalModifier.ArgsNull))
            {
                InAppState inApp = new InAppState();
            }
            else if (globalState.Equals(GlobalState.InAppState) && globalModifier.Equals(GlobalModifier.ArgsNotNull))
            {
                ArgumentState argState = new ArgumentState(args);
            }


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
            ArgsNull,
        }*/