using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.Services;
using DTO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.Admin
{
    public class EditTicketModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public EditTicketModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public Ticket Ticket { get; set; }
        public List<SelectListItem> Users { get; set; }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        public async Task OnGet(int id)
        {
            Users = new List<SelectListItem>();
            var users = await _apiClient.GetUsers();
            foreach (UserResponse user in users)
            {
                var userId = user.Id;
                var userName = user.Name;
                SelectListItem item = new SelectListItem() { Value = userId.ToString(), Text = userName };
                Users.Add(item);
            };

            var ticket = await _apiClient.GetTicket(id);
            Ticket = new Ticket
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Status = ticket.Status,
                TicketType = ticket.TicketType,
                CreatedOn = ticket.CreatedOn,
                AssignedDevId = ticket.AssignedDevId,
                SubmittedById = ticket.SubmittedById,
                ProjectId = ticket.ProjectId
            };
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Message = "Ticket updated successfully!";

            await _apiClient.PutTicket(Ticket);

            return Page();
        }   
    }
}