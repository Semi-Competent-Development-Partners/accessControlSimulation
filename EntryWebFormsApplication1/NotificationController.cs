using System.Web.Http;
using Microsoft.AspNet.SignalR;
using EntryWebFormsApplication1;
using System;

namespace EntryWebFormsApplication1.Controllers {
    public class NotificationController : ApiController {
        [HttpPost]
        [HttpGet]
        [Route("api/notify")]
        public IHttpActionResult NotifyClients(string message) {
            // Get the SignalR Hub context
            var context = GlobalHost.ConnectionManager.GetHubContext<EntryHub>();


            string[] msgParts = message.Split(' ');

            // Broadcast the message to all connected clients
            context.Clients.All.NewEntry(msgParts[0], msgParts[1], msgParts[2] + " " + msgParts[3]); // date is two parts, so we need to concatenate them



            Console.WriteLine($"api endpoint reached: {message}");

            return Ok("Notification sent to all clients.");

            
        }
    }
}
