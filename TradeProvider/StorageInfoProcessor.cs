using System.Globalization;

public class StorageInfoProcessor
{
    private readonly IStorageInfoProvider storageInfoProvider;

    public StorageInfoProcessor(IStorageInfoProvider storageInfoProvider) =>
        this.storageInfoProvider = storageInfoProvider;

    public Dictionary<Client, ClientOrdersInfo> GetClientsAndOrders(string productName)
    {
        var clientsAndOrders = new Dictionary<Client, ClientOrdersInfo>();
        var product = storageInfoProvider.GetProductByProductName(productName);
        var orders = storageInfoProvider.GetOrdersByProductCode(product.Code, productName);

        foreach (var order in orders)
        {
            var client = storageInfoProvider.GetClientByCode(order.ClientCode);

            if (!clientsAndOrders.ContainsKey(client))
                clientsAndOrders[client] = 
                    new ClientOrdersInfo(order.Count, product.Price, new List<DateTime> { order.Date });
            else
                UpdateOrdersInfo(clientsAndOrders[client], order);
        }

        return clientsAndOrders;

        void UpdateOrdersInfo(ClientOrdersInfo previousOrdersInfo, Order order)
        {
            previousOrdersInfo.Count += order.Count;
            previousOrdersInfo.Dates.Add(order.Date);
        }
    }

    public Client ChangeClientInfo(string company, string contact)
    {
        storageInfoProvider.ChangeCustomerInfo(company, contact);
        return storageInfoProvider.GetClientByCompany(company);
    }

    public List<Client> GetGoldenClients(string date)
    {
        var ordersDate = GetOrdersDate(date);
        var foundOrders = storageInfoProvider.FindOrdersByDate(ordersDate);

        var clientsAndOrders = new Dictionary<double, int>();
        var maxOrdersCount = 0;

        foreach (var order in foundOrders)
        {
            if (!clientsAndOrders.ContainsKey(order.ClientCode))
                clientsAndOrders[order.ClientCode] = 0;

            clientsAndOrders[order.ClientCode]++;
            if (clientsAndOrders[order.ClientCode] > maxOrdersCount)
                maxOrdersCount = clientsAndOrders[order.ClientCode];
        }

        var goldenClientsCodes = clientsAndOrders
            .Where(x => x.Value == maxOrdersCount)
            .Select(x => x.Key)
            .ToHashSet<double>();

        var clients = storageInfoProvider.GetAllClients();
        var goldenClients = clients.Where(client => goldenClientsCodes.Contains(client.Code)).ToList();

        return goldenClients;
    }
    private DateTime GetOrdersDate(string date)
    {
        try
        {
            return DateTime.ParseExact(date, "MM.yyyy", CultureInfo.InvariantCulture);
        }
        catch
        {
            throw new InvalidDateException();
        }
    }
}

