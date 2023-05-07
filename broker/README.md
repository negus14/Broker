# Broker
  Implement the IOrder interface
  1.    The class should take IOrderService & a decimal threshold value as parameters in the constructor
  2.    In RespondToTick if the incoming price is less than the threshold use the IOrderService to buy, and also raise the "Placed" event
  3.    If anything goes wrong you should raise the "Errored" event
  4.    Inhibit further buys once one has been placed, or if there is an error
  5.    The code should be thread safe and you should assume it can be called from multiple threads

  You should upload all your code (as Visual Studio or Rider solution) to GitHub and send the URL to your submission
  You should also implement any tests you would write for this, you may use whatever framework you like for this