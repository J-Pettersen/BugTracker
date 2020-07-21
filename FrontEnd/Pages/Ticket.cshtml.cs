using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd
{
    public class TicketModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public TicketModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public TicketResponse Ticket { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Ticket = await _apiClient.GetTicket(id);

            if (Ticket == null)
            {
                return RedirectToPage("/MyTickets");
            }

            return Page();
        }
    }
}