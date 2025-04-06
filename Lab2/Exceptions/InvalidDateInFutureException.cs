using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Exceptions
{
    class InvalidDateInFutureException : Exception
    {
        public InvalidDateInFutureException(DateTime date)
            :base($"Людина не може бути народжена у майбутньому, дата {date:dd-MM-yyyy} не коректна!") { }

        public InvalidDateInFutureException(String message, DateTime date)
           : base($"{message}  {date:dd-MM-yyyy}") {}
    }
}
