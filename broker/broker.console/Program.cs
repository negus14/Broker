//var builder = .CreateBuilder(args);
//var app = builder.Build();

IOrderService orderService = new OrderService();
var order = new Order(orderService, 5);
//order.RespondToTick("AED", 5);
//order.RespondToTick("AED", 1);
order.RespondToTick("AED", 2);
// order.RespondToTick("AED", -1);
// Parallel.Invoke(() =>
//                     {
//                         Console.WriteLine("Begin first task...");
// order.RespondToTick("AED-123456", 1);
//                     },  // close first Action

//                     () =>
//                     {
//                         Console.WriteLine("Begin second task...");
// order.RespondToTick("AED-123456", 2);
//                     }, //close second Action

//                     () =>
//                     {
//                         Console.WriteLine("Begin third task...");
// order.RespondToTick("AED-123456", 3);
//                     } //close third Action
//                 ) ;
// For testing
// Console.Read();

