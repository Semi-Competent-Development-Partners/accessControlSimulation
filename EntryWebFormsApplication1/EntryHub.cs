using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNet.SignalR;

namespace EntryWebFormsApplication1 {
    public class EntryHub : Hub {
        public void NotifyDatabaseChange() {
            Clients.All.updateTable();
        }

        public async Task NewEntry(int employeeId, string actionType, string timestamp) {
            Console.WriteLine("Triggering NewEntry event... (log from Hub class)");

            // Call the 'newEntry' method on all connected clients
            //await Clients.All.newEntry(employeeId, actionType, timestamp.ToString("yyyy-MM-dd HH:mm:ss"));

            await Clients.All.SendAsync("NewEntry", employeeId, actionType, timestamp);
        }
    }
}