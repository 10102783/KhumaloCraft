using KhumaloCraft.Models;
using KhumaloCraft.NotificationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhumaloCraft.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class ProductUpdatesController : ControllerBase
        {
            private readonly NotificationService _notificationService;

            public ProductUpdatesController(NotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            [HttpPost("update-product")]
            public async Task<IActionResult> UpdateProduct(ProductUpdateModel model)
            {
                // Update product logic here...

                // Fetch all subscriptions from the database
                var subscriptions = GetAllSubscriptions(); // Implement this to get subscriptions from your DB

                foreach (var subscription in subscriptions)
                {
                    await _notificationService.SendPushNotification(
                        subscription,
                        "Product Update",
                        $"Product {model.ProductName} has been updated.",
                        "https://yourwebsite.com/products",
                        "https://yourwebsite.com/product-image.jpg"
                    );
                }

                return Ok();
            }

            private List<PushSubscription> GetAllSubscriptions()
            {
                // Implement this to retrieve all subscriptions from your database
                return new List<PushSubscription>();
            }
        }

        public class ProductUpdateModel
        {
            public string ProductName { get; set; }
        }
    }

