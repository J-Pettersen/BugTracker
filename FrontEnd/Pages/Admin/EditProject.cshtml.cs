using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.Services;
using DTO;

namespace FrontEnd
{
    public class EditProjectModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public EditProjectModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public Project Project { get; set; }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        public async Task OnGet(int id)
        {
            var project = await _apiClient.GetProject(id);
            Project = new Project
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ProjectManagerId = project.ProjectManagerId
            };
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Message = "Project updated successfully!";

            await _apiClient.PutProject(Project);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var session = await _apiClient.GetProject(id);

            if (session != null)
            {
                await _apiClient.DeleteProject(id);
            }

            Message = "Project deleted successfully!";

            return Page();
        }
    }
}