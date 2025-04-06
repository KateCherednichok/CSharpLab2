using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Exceptions
{
    class InvalidEmailException : Exception
    {
        public InvalidEmailException(String email)
        : base($"Некоректна електронна пошта: {email}") { }
        public InvalidEmailException(String message, String email)
        : base($"{message}: {email}") { }

    }
}
