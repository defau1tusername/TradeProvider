using ClosedXML.Excel;

public class ExcelStorageInfoProvider : IStorageInfoProvider
{
    private readonly XLWorkbook workbook;

    public ExcelStorageInfoProvider(XLWorkbook workbook) => this.workbook = workbook;

    public Product GetProductByProductName(string productName)
    {
        var productsRows = GetRowsFromWorksheet("Товары");
        var productRow = productsRows.FirstOrDefault(row => row.Cell(2).Value.GetText() == productName);
        if (productRow == null)
            throw new ProductNotFoundException(productName);

        return ConvertProductRow(productRow);
    }

    public List<Order> GetOrdersByProductCode(double productCode, string productName)
    {
        var ordersRows = workbook.Worksheet("Заявки").RangeUsed().RowsUsed().Skip(1);
        var foundOrdersRows = ordersRows.Where(order => order.Cell(2).Value.GetNumber() == productCode);
        if (!foundOrdersRows.Any())
            throw new OrdersNotFoundException($"Заявок на {productName} не найдено");

        return foundOrdersRows.Select(order => ConvertOrderRow(order)).ToList();
    }
    public Client GetClientByCode(double clientCode)
    {
        var clients = GetRowsFromWorksheet("Клиенты");
        var foundClient = clients.FirstOrDefault(customer => customer.Cell(1).Value.GetNumber() == clientCode);
        if (foundClient == null)
            throw new ClientNotFoundException(
                $"Некорректные данные в файле, клиент не найдень. Код клиента: {clientCode}");

        return ConvertClientRow(foundClient);
    }

    public Client GetClientByCompany(string company)
    {
        var clients = GetRowsFromWorksheet("Клиенты");
        var foundClient = clients.FirstOrDefault(client => client.Cell(2).Value.GetText() == company);
        if (foundClient == null)
            throw new ClientNotFoundException($"Компания с названием \"{company}\" не найдена");

        return ConvertClientRow(foundClient);
    }

    public void ChangeCustomerInfo(string company, string contact)
    {
        var clients = GetRowsFromWorksheet("Клиенты");
        var foundClient = clients.FirstOrDefault(client => client.Cell(2).Value.GetText() == company);
        if (foundClient == null)
            throw new ClientNotFoundException($"Компания с названием \"{company}\" не найдена");
        try
        {
            workbook.Worksheet("Клиенты")
                .Cell(foundClient.Cell(4).Address).Value = contact;
            workbook.Save();
        }
        catch (Exception)
        {
            throw new SaveFailedException();
        }
    }

    public List<Order> FindOrdersByDate(DateTime ordersDate)
    {
        var orders = GetRowsFromWorksheet("Заявки");
        var foundOrders = orders.Where(order =>
                order.Cell(6).Value.GetDateTime().Month == ordersDate.Month
                && order.Cell(6).Value.GetDateTime().Year == ordersDate.Year)
            .Select(order => ConvertOrderRow(order))
            .ToList();
        if (foundOrders.Count == 0)
            throw new OrdersNotFoundException($"Заявок за {ordersDate} не найдено");

        return foundOrders;
    }

    public List<Client> GetAllClients() =>
        GetRowsFromWorksheet("Клиенты").Select(client => ConvertClientRow(client)).ToList();

    private IEnumerable<IXLRangeRow> GetRowsFromWorksheet(string worksheetName) =>
        workbook.Worksheet(worksheetName).RangeUsed().RowsUsed().Skip(1);

    private Client ConvertClientRow(IXLRangeRow row) =>
        new Client(
            row.Cell(1).Value.GetNumber(),
            row.Cell(2).Value.GetText(),
            row.Cell(3).Value.GetText(),
            row.Cell(4).Value.GetText());

    private Order ConvertOrderRow(IXLRangeRow row) =>
        new Order(
            row.Cell(1).Value.GetNumber(),
            row.Cell(2).Value.GetNumber(),
            row.Cell(3).Value.GetNumber(),
            row.Cell(5).Value.GetNumber(),
            row.Cell(6).Value.GetDateTime());

    private Product ConvertProductRow(IXLRangeRow row) =>
    new Product(
            row.Cell(1).Value.GetNumber(),
            row.Cell(2).Value.GetText(),
            row.Cell(3).Value.GetText(),
            row.Cell(4).Value.GetNumber());
}

