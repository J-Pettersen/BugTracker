using System.Threading.Tasks;
using DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd
{
    public class UserModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public UserModel(IApiClient apiClient)
        {

            _apiClient = apiClient;

        }

        public UserResponse User { get; set; }

        public async Task<IActionResult> OnGet(string EmailAddress)
        {
            User = await _apiClient.GetUser(EmailAddress);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}