using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleNote
{
    internal class Parser
    {
        public string Input { get; set; }
        public string StringRegex { get; set; }
        public Regex Regex { get; set; }
        public List<Match> Matches { get; private set; }
        private TimeSpan Timeout = Regex.InfiniteMatchTimeout;

        public Parser()
        {

        }

        public Parser(string input, string pattern)
        {
            Input = input;
            StringRegex = pattern;
            Regex = new Regex(pattern);
        }

        public List<Match> Parse()
        {
            if (!string.IsNullOrEmpty(Input) && Regex != null)
            {
                Matches = Regex.Matches(Input).ToList();
            }

            return Matches;
        }

        public string Replace(string replacement, TimeSpan timeout, RegexOptions options = RegexOptions.Compiled)
        {
            return Regex.Replace(Input, StringRegex, replacement, options, timeout);
        }

        public string Replace(string replacement, RegexOptions options = RegexOptions.Compiled)
        {
            return Regex.Replace(Input, StringRegex, replacement, options, Timeout);
        }

        public static List<Match> Parse(string input, string pattern, RegexOptions options = RegexOptions.Compiled)
        {
            return Regex.Matches(input, pattern, options, Regex.InfiniteMatchTimeout).ToList();
        }

        public static List<Match> Parse(string input, string pattern, TimeSpan timeout, RegexOptions options = RegexOptions.Compiled)
        {
            return Regex.Matches(input, pattern, options, timeout).ToList();
        }

        public static string Replace(string input, string pattern, string replacement, TimeSpan timeout, RegexOptions options = RegexOptions.Compiled)
        {
            return Regex.Replace(input, pattern, replacement, options, timeout);
        }

        public static string Replace(string input, string pattern, string replacement, RegexOptions options = RegexOptions.Compiled)
        {
            return Regex.Replace(input, pattern, replacement, options, Regex.InfiniteMatchTimeout);
        }

        public static string Replace(string input, IEnumerable<string> thingsToReplace, string replacement, RegexOptions options = RegexOptions.Compiled)
        {
            Parser parser = new Parser();
            parser.Input = input;
            string modifiedInput = input;

            for (int i = 0; i < thingsToReplace.LongCount(); i++)
            {
                parser.StringRegex = thingsToReplace.ElementAt(i);
                modifiedInput = parser.Replace(replacement);
                parser.Input = modifiedInput;
            }

            return modifiedInput;
        }

        public static string Replace(string input, IEnumerable<Match> thingsToReplace, string replacement, RegexOptions options = RegexOptions.Compiled)
        {
            var list = new List<string>();

            for (int i = 0; i < thingsToReplace.LongCount(); i++)
            {
                list.Add(thingsToReplace.ElementAt(i).Value);
            }

            return Replace(input, list, replacement);
        }
    }
}