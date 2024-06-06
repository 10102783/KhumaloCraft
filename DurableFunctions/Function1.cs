using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DurableFunctions
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Call activity functions for KhumaloCraft
            outputs.Add(await context.CallActivityAsync<string>(nameof(AddProduct), new Product { Name = "Sample Product", Price = 29.99M, Category = "Crafts", Availability = true }));
            outputs.Add(await context.CallActivityAsync<string>(nameof(OrderProduct), new Order { ProductName = "Sample Product", Quantity = 2 }));
            outputs.Add(await context.CallActivityAsync<string>(nameof(ContactUs), new Inquiry { CustomerName = "John Doe", Message = "I have a question about your products." }));

            // returns responses from activities
            return outputs;
        }

        [FunctionName(nameof(AddProduct))]
        public static string AddProduct([ActivityTrigger] Product product, ILogger log)
        {
            log.LogInformation("Adding product {Name}.", product.Name);
            // Simulate adding product to a database
            return $"Product added: {product.Name}";
        }

        [FunctionName(nameof(OrderProduct))]
        public static string OrderProduct([ActivityTrigger] Order order, ILogger log)
        {
            log.LogInformation("Processing order for {ProductName}.", order.ProductName);
            // Simulate order processing
            return $"Order placed for {order.Quantity} of {order.ProductName}";
        }

        [FunctionName(nameof(ContactUs))]
        public static string ContactUs([ActivityTrigger] Inquiry inquiry, ILogger log)
        {
            log.LogInformation("Handling inquiry from {CustomerName}.", inquiry.CustomerName);
            // Simulate handling customer inquiry
            return $"Inquiry received from {inquiry.CustomerName}: {inquiry.Message}";
        }

        [FunctionName("Function1_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Function1", null);

            log.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool Availability { get; set; }
    }

    public class Order
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }

    public class Inquiry
    {
        public string CustomerName { get; set; }
        public string Message { get; set; }
    }
}
