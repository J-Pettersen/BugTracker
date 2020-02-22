using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd
{
    public class ProjectModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public ProjectModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public ProjectResponse Project { get; set; }

        public List<TicketResponse> ProjectTickets { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Project = await _apiClient.GetProject(id);

            if (Project == null)
            {
                return RedirectToPage("/Index");
            }
            
            ProjectTickets = await _apiClient.GetTickets();

            return Page();
        }
    }
}