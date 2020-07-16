using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using System.Security.Claims;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd
{
    public class ProjectModel : PageModel
    {
        private readonly IApiClient _apiClient;
        public bool IsAdmin { get; set; }

        public ProjectModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public ProjectResponse Project { get; set; }

        public List<TicketResponse> ProjectTickets { get; set; }

        public List<UserResponse> AssignedUsers { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Project = await _apiClient.GetProject(id);
            IsAdmin = User.IsAdmin();

            if (Project == null)
            {
                return RedirectToPage("/Index");
            }

            AssignedUsers = await _apiClient.GetUsersByProject(id);

            ProjectTickets = await _apiClient.GetTickets();

            return Page();
        }
    }
}