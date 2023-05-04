public class Product
{
    public double Code { get; set; }
    public string Name { get; set; }
    public string UnitOfMeasurement { get; set; }
    public double Price { get; set; }

    public Product(double code, string name, string unitOfMeasurement, double price)
    {
        Code = code;
        Name = name;
        UnitOfMeasurement = unitOfMeasurement;
        Price = price;
    }
}