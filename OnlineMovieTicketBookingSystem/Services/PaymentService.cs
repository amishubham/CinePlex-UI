using System;

namespace OnlineMovieTicketBookingSystem.Services
{
    public class PaymentService
    {
        public string CreateOrder(decimal amount)
        {
            // Simulate the creation of a payment order
            // In a real-world scenario, this would involve calling an external payment gateway API
            // For simplicity, we'll just return a dummy order ID
            return Guid.NewGuid().ToString();
        }

        public bool ProcessPayment(string orderId, decimal amount, string paymentMethod)
        {
            // Simulate the processing of a payment
            // In a real-world scenario, this would involve calling an external payment gateway API
            // For simplicity, we'll assume the payment is always successful
            Console.WriteLine($"Processing payment for Order ID: {orderId}, Amount: {amount}, Payment Method: {paymentMethod}");
            return true;
        }
    }
}