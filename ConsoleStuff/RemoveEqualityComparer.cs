using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNote
{
    internal class RemoveEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            if (!string.IsNullOrEmpty(x) || !string.IsNullOrEmpty(y))
            {
                return false;
            }

            string trimmedX = x.Trim();
            string trimmedY = y.Trim();

            if (trimmedX == trimmedY)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public int GetHashCode([DisallowNull] string obj)
        {
            string trimmed = obj.Trim();
            return trimmed.GetHashCode();
        }
    }
}