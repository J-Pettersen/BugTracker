using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace FrontEnd.Pages
{
    public class CreateTicketModel : PageModel
    {
        private readonly IApiClient _apiClient;
        public int pId;
        public CreateTicketModel(IApiClient apiClient)
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
            //Gets list of all users and puts them in selectList to be used in drop down list
            Users = new List<SelectListItem>();
            var users = await _apiClient.GetUsers();
            foreach (UserResponse user in users)
            {
                var userId = user.Id;
                var userName = user.Name;
                SelectListItem item = new SelectListItem() { Value = userId.ToString(), Text = userName };
                Users.Add(item);
            };
            pId = id;
        }
        public async Task<IActionResult> OnPost(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Name).ToString();
            var SubmitUser = await _apiClient.GetUser(email);

            Ticket.Status = "Open";
            Ticket.SubmittedById = SubmitUser.Id;
            Ticket.ProjectId = id;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Message = "Ticket successfully created!";

            await _apiClient.PostTicket(Ticket);

            return Page();
        }
    }
}