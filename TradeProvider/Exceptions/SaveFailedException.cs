using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SaveFailedException : TradeProviderExceptionBase
{
    public SaveFailedException()
        : base($"Ошибка при сохранении/изменении файла")
    { }
}

