using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages
{
    public class MyTicketsModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public MyTicketsModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public List<TicketResponse> Tickets { get; set; }
        public UserResponse TicketUser { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAuthorised { get; set; }

        public async Task<IActionResult> OnGet()
        {
            IsAuthorised = User.IsAuthorised();
            IsAdmin = User.IsAdmin();
            if (IsAuthorised)
            {
                Tickets = await _apiClient.GetTickets();
                var email = User.FindFirstValue(ClaimTypes.Name).ToString();
                TicketUser = await _apiClient.GetUser(email);
            }            
            return Page();
        }
    }
}