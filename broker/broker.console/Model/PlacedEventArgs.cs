public delegate void PlacedEventHandler(PlacedEventArgs e);

public class PlacedEventArgs
{
    public string Code { get; }
    public decimal Price { get; }

    public PlacedEventArgs(string code, decimal price)
    {
        Code = code;
        Price = price;
    }
}