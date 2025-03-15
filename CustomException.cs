using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    /// <summary>
    /// Exception for pre-specified user friendly message
    /// </summary>
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
