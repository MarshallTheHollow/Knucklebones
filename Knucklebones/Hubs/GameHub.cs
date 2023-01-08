using Knucklebones.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Knucklebones.Hubs
{
    public class GameHub : Hub
    {

        public async Task CheckArrays(string user1, int[][] user1score, string user2, int[][] user2score)
        {
            await Clients.User(user1).SendAsync("ArrayUpdate", user1score, user2score);
            await Clients.User(user2).SendAsync("ArrayUpdate", user2score, user1score);
        }

        public async Task StartTurn(string user1, string user2)
        {
            await SendGameInfo(user1, user2);     
        }

        public async Task SendGameInfo(string nowplaying, string nowwait)
        {
            int DiceRoll = GameClass.RollTheDice();
            await Clients.User(nowplaying).SendAsync("YourTurn", DiceRoll);
            await Clients.User(nowwait).SendAsync("Wait", DiceRoll);
        }

    }
}
