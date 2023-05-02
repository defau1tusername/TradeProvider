public class Order
{
    public double Code { get; set; }
    public double ProductCode { get; set; }
    public double ClientCode { get; set; }
    public double Count { get; set; }
    public DateTime Date { get; set; }

    public Order(double code, double productCode, double clientCode, double count, DateTime date)
    {
        Code = code;
        ProductCode = productCode;
        ClientCode = clientCode;
        Count = count;
        Date = date;
    }
}