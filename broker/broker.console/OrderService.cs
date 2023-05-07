public class OrderService : IOrderService
{
    public OrderService()
    {
    }

    public void Buy(string code, int quantity, decimal price)
    {
        Console.WriteLine($"Code: {code} bought at a quantity of {quantity} for a price of {price}");
    }

    public void Sell(string code, int quantity, decimal price)
    {
        Console.WriteLine($"Code: {code} sold at a quantity of {quantity} for a price of {price}");
    }
}