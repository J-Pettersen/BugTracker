using DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd
{
    public class MyProjectsModel : PageModel
    {

        private readonly IApiClient _apiClient;

        public MyProjectsModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public List<ProjectResponse> MyProjects { get; set; }

        public async Task<IActionResult> OnGet(String emailAddress)
        {
            MyProjects = await _apiClient.GetProjectsByUser(emailAddress);

            return Page();
        }
    }
}