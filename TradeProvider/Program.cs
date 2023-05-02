using ClosedXML.Excel;

public class TradeProvider
{
    public static void Main()
    {
        var workbook = GetWorkbook();
        var excelStorageInfoProvider = new ExcelStorageInfoProvider(workbook);
        var clientDataProcessor = new StorageInfoProcessor(excelStorageInfoProvider);
        while (true)
        {
            Console.WriteLine("Команды: \n - заказы \n - изменить контактное лицо \n - золотой клиент \n");
            Console.Write("Введите команду: ");
            var command = Console.ReadLine();
            try
            {
                CommandProvider.Execute(command, clientDataProcessor);
            }
            catch (Exception exception) 
                when (exception is TradeProviderExceptionBase || exception is UnknownCommandException)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }

    private static XLWorkbook GetWorkbook()
    {
        var workbook = (XLWorkbook)null;
        while (workbook == null)
        {
            Console.Write("Введите путь до файла: ");
            var path = Console.ReadLine();
            try
            {
                workbook = new XLWorkbook(path);
            }
            catch (Exception)
            {
                Console.WriteLine("Невероный путь до файла, введите еще раз:");
            }
        }

        return workbook;
    }
}
