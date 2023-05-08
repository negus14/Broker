namespace broker.tests;
public class BrokerTests
{
    [Fact]
    public void OrderService_Greater_Or_Equal_To_Threshold()
    {
        Mock<IOrderService> moqOrderService = new Mock<IOrderService>();
        decimal threshold = 2;
        var order = new Order(moqOrderService.Object, threshold);
        moqOrderService.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()));

        order.RespondToTick("AED-123456", 3);

        moqOrderService.Verify(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()), Times.Never);
    }

    [Fact]
    public void OrderService_Less_Than_Threshold()
    {
        Mock<IOrderService> moqOrderService = new Mock<IOrderService>();
        decimal threshold = 2;
        var order = new Order(moqOrderService.Object, threshold);
        moqOrderService.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()));

        order.RespondToTick("AED-123456", 1);

        moqOrderService.Verify(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()), Times.Once); 
    }

    [Fact]
    public void OrderService_Raises_Placed_Event()
    {
        Mock<IOrderService> moqOrderService = new Mock<IOrderService>();
        decimal threshold = 2;
        var order = new Order(moqOrderService.Object, threshold);
        moqOrderService.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()));

        order.RespondToTick("AED-123456", 1);

        order.Placed += (PlacedEventArgs p) =>
        {
            Assert.Equal("AED-123456", p.Code);
            Assert.Equal(1, p.Price);
        };   
    }

    [Fact]
    public void OrderService_Throws_Exception_Raise_Errored_Event()
    {
        Mock<IOrderService> moqOrderService = new Mock<IOrderService>();
        decimal threshold = 2;
        var order = new Order(moqOrderService.Object, threshold);
        
        try
        {
            moqOrderService.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>())).Throws(It.IsAny<Exception>());

            order.RespondToTick("AED-123456", 1);
        }
        catch (System.Exception)
        {   
            order.Errored += (ErroredEventArgs e) =>
            {
                Assert.Equal("AED-123456", e.Code);
                Assert.Equal(1, e.Price);
            };          
        } 
    }

    [Fact]
    public void OrderService_Is_ThreadSafe()
    {
        Mock<IOrderService> moqOrderService1 = new Mock<IOrderService>();
        Mock<IOrderService> moqOrderService2 = new Mock<IOrderService>();
        Mock<IOrderService> moqOrderService3 = new Mock<IOrderService>();

        moqOrderService1.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()));
        moqOrderService2.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()));
        moqOrderService3.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()));

        decimal threshold = 2;
        var order1 = new Order(moqOrderService1.Object, threshold);
        var order2 = new Order(moqOrderService2.Object, threshold);
        var order3 = new Order(moqOrderService2.Object, threshold);

        //RunTaskInParallel(() => order.RespondToTick("AED", 2), numberOfParallelExecutions);  
        Parallel.Invoke(() =>
                            {
                                Console.WriteLine("Begin first task...");
        order1.RespondToTick("AED-123456", 1);
                            },  // close first Action

                            () =>
                            {
                                Console.WriteLine("Begin second task...");
        order2.RespondToTick("AED-123456", 2);
                            }, //close second Action

                            () =>
                            {
                                Console.WriteLine("Begin third task...");
        order3.RespondToTick("AED-123456", 3);
                            } //close third Action
                        ) ;
        
        order1.Placed += (PlacedEventArgs p) =>
        {
            Assert.Equal("AED-123456", p.Code);
            Assert.Equal(1, p.Price);
        }; 

                order2.Placed += (PlacedEventArgs p) =>
        {
            Assert.Equal("AED-123456", p.Code);
            Assert.Equal(1, p.Price);
        };  

                order3.Placed += (PlacedEventArgs p) =>
        {
            Assert.Equal("AED-123456", p.Code);
            Assert.Equal(3, p.Price);
        };   
    }
}