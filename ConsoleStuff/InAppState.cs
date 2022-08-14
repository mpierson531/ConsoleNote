using ConsoleStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNote
{
    public class InAppState : StatesClass
    {
        public InAppState() : base()
        {
            Introduction();
        }

        void Introduction()
        {
            Console.WriteLine("Please enter a command. You may either 'Create' or 'Write'.");
            Command = Console.ReadLine();

            if (Command.Equals("Create", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Please enter the name of the file to create.");
                FileName = Console.ReadLine();
            }
            else if (Command.Equals("Write", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Please enter the name of the file to write to.");
                FileName = Console.ReadLine();
            }

            FileNameCommandNullOrEmpty();
        }

        void FileNameCommandNullOrEmpty()
        {
            if (String.IsNullOrEmpty(Command))
            {
                Console.WriteLine("Please enter a command. You may either 'Create' or 'Write'.");
                FileName = Console.ReadLine();
            }
            else if (String.IsNullOrEmpty(FileName))
            {
                Console.WriteLine("Please enter a file name");
                FileName = Console.ReadLine();
            }
            else
            {
                TransitionToContent();
            }

            void TransitionToContent()
            {
                if (!String.IsNullOrEmpty(FileName) && Command.Equals("Create", StringComparison.CurrentCultureIgnoreCase))
                {

                    Console.WriteLine($"{FileName} created.");
                }
                else if (!String.IsNullOrEmpty(FileName) && Command.Equals("Write", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine($"Please enter the content to write to {FileName}.");

                }
            }
        }
    }
}
