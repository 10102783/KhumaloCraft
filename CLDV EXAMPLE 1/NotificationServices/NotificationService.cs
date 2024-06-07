using KhumaloCraft.Models;

using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KhumaloCraft.NotificationServices
{
    public class NotificationService
    {
        private readonly string _publicKey = "<Your VAPID Public Key>";
        private readonly string _privateKey = "<Your VAPID Private Key>";

        public async Task SendPushNotification(PushSubscription subscription, string title, string message, string url, string imageUrl)
        {
            var webPushClient = new Product();
           

            var payload = new
            {
                notification = new
                {
                    title,
                    body = message,
                    icon = imageUrl,
                    vibrate = new int[] { 100, 50, 100 },
                    data = new { url },
                    actions = new[]
                    {
                        new { action = "explore", title = "Go to the site" }
                    }
                }
            };

            string jsonPayload = JsonConvert.SerializeObject(payload);
            
        }
    }
}

