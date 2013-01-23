using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    /// <summary>
    /// Contains extension methods for the String class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Alias for String.Format
        /// </summary>
        /// <param name="format">The format string</param>
        /// <param name="args">The arguments to be inserted into the placeholders</param>
        /// <returns>The formatted string</returns>
        public static String FormatWith(this String format, params object[] args)
        {
            return String.Format(format, args);
        }
    }
}
