using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UnknownCommandException : Exception
{
    public UnknownCommandException()
        : base("Неизвестная команда")
    { }
}

