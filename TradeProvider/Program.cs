using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net;

public class TradeProvider
{
    public static void Main()
    {
        Console.Write("Введите путь до файла: ");
        var path = Console.ReadLine();
        var workbook = new XLWorkbook(path);
        var clientDataProcessor = new ClientDataProcessor(new ExcelStorageInfoProvider(workbook));
        
        Console.WriteLine("Команды: \n - заказы \n - изменить контактное лицо \n - золотой клиент \n");
        while (true)
        {
            try
            {
                Console.Write("Введите команду: ");
                var command = Console.ReadLine();
                ProcessCommand(command, clientDataProcessor);
            }
            catch (Exception exception) when (exception is CommandBaseException || exception is UnknownCommandException)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }

    private static void ProcessCommand(string command, ClientDataProcessor clientDataProcessor)
    {
        switch (command)
        {
            case "заказы":
            {
                ProcessOrderCommand(clientDataProcessor);
                break;
            }
            case "изменить контактное лицо":
            {
                ProccessCustomerInfoCommand(clientDataProcessor);
                break;
            }
            case "золотой клиент":
            {
                ProcessGoldenClientCommand(clientDataProcessor);
                break;
            }
            default:
                throw new UnknownCommandException();
        }
    }

    private static void ProcessOrderCommand(ClientDataProcessor clientDataProcessor)
    {
        Console.Write("Введите наименование товара по которому провести поиск: ");
        var product = Console.ReadLine();
        var clientsAndOrders = clientDataProcessor.GetClientsAndOrders(product);
        OutputClientsAndOrdersToConsole(clientsAndOrders);
    }
    private static void ProccessCustomerInfoCommand(ClientDataProcessor clientDataProcessor)
    {
        Console.Write("Введите наименование организации у которой нужно изменить контактное лицо: ");
        var company = Console.ReadLine();
        Console.Write("Введите новое контактное лицо: ");
        var contact = Console.ReadLine();
        var clientUpdatedInfo = clientDataProcessor.ChangeClientInfo(company, contact);
        OutputClientUpdatedInfo(clientUpdatedInfo);
    }

    private static void ProcessGoldenClientCommand(ClientDataProcessor clientDataProcessor)
    {
        Console.Write("Введите за какой период найти золотого клиента в формате mm/yyyy: ");
        var date = Console.ReadLine();
        var goldenClients = clientDataProcessor.GetGoldenClients(date);
        OutputGoldenClientToConsole(goldenClients, date);
    }

    private static void OutputClientsAndOrdersToConsole(Dictionary<Client, ClientOrdersInfo> clientsAndOrders)
    {
        foreach (var clientAndOrders in clientsAndOrders)
        {
            Console.WriteLine("\n======================================= Информация о клиенте =======================================");
            OutputClient(clientAndOrders.Key);
            Console.WriteLine("======================================= Информация о заказах =======================================");
            Console.WriteLine("Требуемое количество: " + clientAndOrders.Value.Count);
            Console.WriteLine("Цена: " + clientAndOrders.Value.Price);
            Console.WriteLine("Даты заказов: ");
            foreach (var date in clientAndOrders.Value.Dates)
                Console.WriteLine(" - " + date.ToString("dd/MM/yyyy"));
            Console.WriteLine("=====================================================================================================\n");

        }
    }
   
    private static void OutputClientUpdatedInfo(Client clientUpdatedInfo)
    {
        Console.WriteLine("\n======================================= Обновленная информация: ======================================");
        OutputClient(clientUpdatedInfo);
        Console.WriteLine("======================================================================================================\n");
    } 

    private static void OutputGoldenClientToConsole(List<Client> goldenClients, string date)
    {
        Console.WriteLine($"\n================================= Золотые клиенты за период: {date} ================================");
        foreach (var goldenClient in goldenClients)
            OutputClient(goldenClient);
        Console.WriteLine("=====================================================================================================\n");
    }

    private static void OutputClient(Client client) =>
        Console.WriteLine($"{client.Code} | {client.Company} | {client.Address} | {client.Contact}");
}
