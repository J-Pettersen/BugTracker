using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        protected readonly IApiClient _apiClient;

        public IndexModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IEnumerable<ProjectResponse> Projects { get; set; }
        


        public async Task OnGet()
        {

            var projects = await _apiClient.GetProjects();

            Projects = projects.OrderBy(p => p.Title);
        }
    }
}
