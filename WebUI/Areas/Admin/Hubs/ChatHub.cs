using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace WebUI.Areas.Admin.Hubs
{
    [Authorize(Roles = "Admin,Member")]
    public class ChatHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public ChatHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SendMessage(string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var username = user.UserName;
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}
