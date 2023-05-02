public class ClientOrdersInfo
{
    public double Count { get; set; }
    public double Price { get; set; }
    public List<DateTime> Dates { get; set; }

    public ClientOrdersInfo(double count, double price, List<DateTime> dates)
    {
        Count = count;
        Price = price;
        Dates = dates;
    }
}