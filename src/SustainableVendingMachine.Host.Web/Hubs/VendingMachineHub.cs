using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SustainableVendingMachine.Host.Web.Hubs
{
    public class VendingMachineHub : Hub
    {
        public async Task SendDisplayMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveDisplayMessage", message);
        }
    }
}
