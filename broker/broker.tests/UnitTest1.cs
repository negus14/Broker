namespace broker.tests;
public class UnitTest1
{
    [Fact]
    public void OrderService_Greater_Or_Equal_To_Threshold()
    {
        Mock<IOrderService> moqOrderService = new Mock<IOrderService>();
        moqOrderService.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()));
        decimal threshold = 2;
        var order = new Order(moqOrderService?.Object, threshold);

        order.RespondToTick("AED-123456", 3);

        moqOrderService.Verify(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>(), Times.Zero));
    }

    [Fact]
    public void OrderService_Less_Than_Threshold()
    {
        Mock<IOrderService> moqOrderService = new Mock<IOrderService>();
        moqOrderService.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()));
        decimal threshold = 2;
        var order = new Order(moqOrderService?.Object, threshold);

        order.RespondToTick("AED-123456", 1);

        moqOrderService.Verify(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>(), Times.Once)); 
    }

    [Fact]
    public void OrderService_Raises_Placed_Event()
    {
        Mock<IOrderService> moqOrderService = new Mock<IOrderService>();
        moqOrderService.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()));
        decimal threshold = 2;
        var order = new Order(moqOrderService?.Object, threshold);

        order.RespondToTick("AED-123456", 1);

        order.Placed += (object sender, PlacedEventArgs p) =>
        {
            Assert.Equals(p.Code, "AED-123456");
            Assert.Equals(p.Price, 1);
        };   
    }

    [Fact]
    public void OrderService_Throws_Exception_Raise_Errored_Event()
    {
        Mock<IOrderService> moqOrderService = new Mock<IOrderService>();
        moqOrderService.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>())).Throws(new Exception());
        decimal threshold = 2;
        var order = new Order(moqOrderService?.Object, threshold);

        order.RespondToTick("AED-123456", 1);

        order.Errored += (object sender, ErroredEventArgs e) =>
        {
            Assert.Equals(e.Code, "AED-123456");
            Assert.Equals(e, 1);
            Assert.Equals(e, 1);
        };        
    }

    [Fact]
    public void OrderService_Is_ThreadSafe()
    {
        Mock<IOrderService> moqOrderService1 = new Mock<IOrderService>();
        Mock<IOrderService> moqOrderService2 = new Mock<IOrderService>();

        moqOrderService.Setup(x => x.Buy(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<decimal>()));
        decimal threshold = 2;
        var order1 = new Order(moqOrderService1?.Object, threshold);
        var order2 = new Order(moqOrderService2?.Object, threshold);

        order1.RespondToTick("AED-123456", 1);
        order2.RespondToTick("AED-123456", 1);

        int numberOfParallelExecutions = 2;
        //RunTaskInParallel(() => order.RespondToTick("AED", 2), numberOfParallelExecutions);   
    }
}