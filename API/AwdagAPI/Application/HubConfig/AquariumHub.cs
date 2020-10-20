using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Hub;
using Application.Dtos.NewFolder.Response;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace Application.HubConfig
{
    public class AquariumHub : Hub<IAquariumHubInterface>
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task ExitGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {   
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendAquariumDataToDirectedGroups(HubTransferData data)
        {
            await Clients.All.TransferData(data);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendMessage(message);
        }
    }
}
