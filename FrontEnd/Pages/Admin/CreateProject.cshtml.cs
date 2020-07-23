using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.Admin
{
    public class CreateProjectModel : PageModel
    {

        private readonly IApiClient _apiClient;

        public CreateProjectModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        [BindProperty]
        public DTO.Project Project { get; set; }
        public ProjectResponse TheProject { get; set; }

        public List<SelectListItem> Users { get; set; }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        public async Task OnGet()
        {
            //Gets list of all users and puts them in selectList to be used in drop down list
            Users = new List<SelectListItem>();
            var users = await _apiClient.GetUsers();
            foreach (var user in users)
            {
                var userId = user.Id;
                var userName = user.Name;
                SelectListItem item = new SelectListItem() { Value = userId.ToString(), Text = userName };
                Users.Add(item);
            };
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Message = "Project successfully created!";

            await _apiClient.PostProject(Project);

            return Page();
        }
    }
}