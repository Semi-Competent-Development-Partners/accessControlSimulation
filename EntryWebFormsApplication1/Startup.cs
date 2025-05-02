using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(EntryWebFormsApplication1.Startup))]

namespace EntryWebFormsApplication1 {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            // Configure SignalR
            app.MapSignalR();
        }
    }
}
