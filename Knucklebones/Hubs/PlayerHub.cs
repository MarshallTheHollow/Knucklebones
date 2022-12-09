using Knucklebones.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


namespace Knucklebones.Hubs
{
    public class PlayerHub : Hub
    {

        static List<string> Users = new List<string>();

        public async Task SendInvite(string opp)
        {
            string InviteSender = Context.User.Identity.Name;
            await Clients.User(opp).SendAsync("InviteToGame", InviteSender);
        }

        public async Task InviteAction(string InviteSender, string message)
        {
            string me = Context.User.Identity.Name;
            if (message == "accept")
            {
                await Clients.Users(InviteSender, me).SendAsync("AcceptInvite", me, InviteSender);
            }
            else
            {
                await Clients.Users(InviteSender, me).SendAsync("DeclineInvite");
            }          
        }

        public async Task GetUsersList()
        {
            await Clients.All.SendAsync("Userslist", Users);
        }

        public async Task Connect(string userName)
        {
            if (!Users.Contains(userName))
            {
                Users.Add(userName);
            }           
            await GetUsersList();
        }

        public async Task Disconnect(string userName)
        {
            Users.Remove(userName);

            await Clients.All.SendAsync("DeleteUser", userName);

            await GetUsersList();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            string name = Context.User.Identity.Name;

            await Disconnect(name);

            await base.OnDisconnectedAsync(ex);
        }

    }

}