using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleNote
{

    internal class Logger
    {
        private static TextReader In = Console.In;
        private static TextWriter Out = Console.Out;

        public static void WriteLine(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Out.WriteLine(text);
        }

        public static void WriteLine(float value)
        {
            WriteLine(value.ToString());
        }

        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Out.WriteLine(text);
        }

        public static void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Out.Write(text);
        }

        public static void Write(float value)
        {
            Write(value.ToString());
        }

        public static void Write(string? value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Out.Write(value);
        }

        public static string ReadLine()
        {
            GC.KeepAlive(In);
            Console.ForegroundColor = ConsoleColor.White;
            string? input = In.ReadLine();

            if (!string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                return "\0";
            }
        }

        public static char Read()
        {
            Console.ForegroundColor = ConsoleColor.White;
            char input = (char)In.Read();

            if (!char.IsWhiteSpace(input))
            {
                return input;
            }
            else
            {
                return '\0';
            }
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Out.WriteLine($"Error: {message}");
        }

        public static void Error(object message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Out.WriteLine(message.ToString());
        }
    }
}
