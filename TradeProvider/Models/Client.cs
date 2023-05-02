public class Client
{
    public double Code { get; set; }
    public string Company { get; set; }
    public string Address { get; set; }
    public string Contact { get; set; }

    public Client(double code, string company, string address, string contact)
    {
        Code = code;
        Company = company;
        Address = address;
        Contact = contact;
    }
}