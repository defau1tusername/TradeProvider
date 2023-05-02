using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class CommandBaseException : Exception
{
    public CommandBaseException(string message)
        : base(message)
    { }
}
