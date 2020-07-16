using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        protected readonly IApiClient _apiClient;
        public bool IsAdmin { get; set; }

        public IndexModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IEnumerable<ProjectResponse> Projects { get; set; }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);



        public async Task OnGet()
        {
            IsAdmin = User.IsAdmin();

            var projects = await _apiClient.GetProjects();

            Projects = projects.OrderBy(p => p.Title);
        }
    }
}
