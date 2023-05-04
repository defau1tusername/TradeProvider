using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OrdersNotFoundException : TradeProviderExceptionBase
{
    public OrdersNotFoundException(string meassgage)
        : base(meassgage)
    { }
}

