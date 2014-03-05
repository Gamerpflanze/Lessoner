using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lessoner
{
    public class MultipleUserException : Exception
    {
        public MultipleUserException() : base("Es Existieren mehrere Benutzer mit diesem Login")
        {
        }
    }
}