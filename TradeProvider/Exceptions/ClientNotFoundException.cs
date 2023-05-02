using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ClientNotFoundException : CommandBaseException
{
    public ClientNotFoundException(string message)
        : base(message)
    { }
}

