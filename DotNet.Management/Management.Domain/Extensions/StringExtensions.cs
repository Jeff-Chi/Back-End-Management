using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string? value)
        {
            return value == null || value.Length == 0;
        }

        public static bool IsNullOrWhiteSpace(this string? value)
        {
            if (value == null) return true;

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i])) return false;
            }

            return true;
        }
    }
}
