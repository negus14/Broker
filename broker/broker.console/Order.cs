public class Order : IOrder
{
    public event PlacedEventHandler? Placed;
    public event ErroredEventHandler? Errored;

    private readonly IOrderService _orderService;
    private decimal _threshold;
    
    // The class should take IOrderService & a decimal threshold value as parameters in the constructor
    public Order(IOrderService orderService, decimal threshold)
    {
        _orderService = orderService;
        _threshold = threshold;
    }

    public void RespondToTick(string code, decimal price)
    {
        // Inhibit further buys once one has been placed, or if there is an error
        if(Placed != null || Errored != null)
        {
            Console.WriteLine("Cannot buy as order has been placed");
            return;
        }
        
        try
        {
            // In RespondToTick if the incoming price is less than the threshold use the IOrderService to buy, 
            if(price < _threshold)
            {
                var placedEventArgs = new PlacedEventArgs(code, price);

                // and also raise the "Placed" event
                Placed += new PlacedEventHandler(Buy);
                Placed(placedEventArgs);
            }
        }
        catch (System.Exception ex)
        {   
            // If anything goes wrong you should raise the "Errored" event     
            var erroredEventArgs = new ErroredEventArgs(code, price, ex);
            Errored += new ErroredEventHandler(Buy); 
            Errored(erroredEventArgs);
        } 
    }

    public void Buy(PlacedEventArgs placedEventArgs)
    {
        _orderService.Buy(placedEventArgs.Code, 1, placedEventArgs.Price);
    }

    public void Buy(ErroredEventArgs erroredEventArgs)
    {
        Console.WriteLine($"Exception: {erroredEventArgs.GetException().ToString()}");
    }
}