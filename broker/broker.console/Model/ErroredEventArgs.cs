public delegate void ErroredEventHandler(ErroredEventArgs e);

public class ErroredEventArgs : ErrorEventArgs
{
    public string Code { get; }
    public decimal Price { get; }
    
    public ErroredEventArgs(string code, decimal price, Exception ex) : base(ex)
    {
        Code = code;
        Price = price;
    }
}