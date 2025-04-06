using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Exceptions
{
    class InvalidDateInPastException : Exception
    {
        public InvalidDateInPastException(DateTime date)
            :base($"Людина не може бути народжена настільки давно, дата {date:dd-MM-yyyy} не коректна!"){ }

        public InvalidDateInPastException(String message, DateTime date)
           : base($"{message}  {date:dd-MM-yyyy}") { }
    }
}
