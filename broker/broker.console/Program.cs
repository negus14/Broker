//var builder = .CreateBuilder(args);
//var app = builder.Build();

IOrderService orderService = new OrderService();
var order = new Order(orderService, 5);
//order.RespondToTick("AED", 5);
//order.RespondToTick("AED", 1);
//order.RespondToTick("AED", 2);
order.RespondToTick("AED", -1);

int numberOfParallelExecutions = 2;
//RunTaskInParallel(() => order.RespondToTick("AED", 2), numberOfParallelExecutions);

