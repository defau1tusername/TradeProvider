using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ProductNotFoundException : TradeProviderExceptionBase
{
    public ProductNotFoundException(string product) 
        : base($"Продукт с названием \"{product}\" не найден")
    { }
}
