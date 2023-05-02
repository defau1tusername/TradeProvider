using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IStorageInfoProvider
{
    public Product GetProductByProductName(string productName);

    public List<Order> GetOrdersByProductCode(double productCode, string productName);

    public Client GetClientByCode(double clientCode);

    public Client GetClientByCompany(string company);

    public void ChangeCustomerInfo(string company, string contact);

    public List<Order> FindOrdersByDate(DateTime ordersDate);

    public List<Client> GetAllClients();
}

