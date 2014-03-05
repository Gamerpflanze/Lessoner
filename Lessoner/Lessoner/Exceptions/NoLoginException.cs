using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lessoner
{
    public class NoLoginException : Exception
    {
        public NoLoginException() : base("Ex existiert kein Benutzer mit diesem Login.")
        {
        }
    }
}