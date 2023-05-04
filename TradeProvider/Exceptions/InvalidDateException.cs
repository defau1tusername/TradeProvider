using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvalidDateException : TradeProviderExceptionBase
{
    public InvalidDateException()
        : base("Формат даты не соответствует MM.yyyy")
    { }
}

