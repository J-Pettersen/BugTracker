using System.Threading.Tasks;
using FrontEnd.Services;
using FrontEnd.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace FrontEnd.Pages
{
    public class WelcomeModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public WelcomeModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public User authorise { get; set; }

        public IActionResult OnGet()
        {
            // Redirect to home page if user is anonymous or already registered as attendee
            var isAuthorised = User.IsAuthorised();

            if (!User.Identity.IsAuthenticated || isAuthorised)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var success = await _apiClient.PostUser(authorise);

            if (!success)
            {
                ModelState.AddModelError("", "There was an issue creating the attendee for this user.");
                return Page();
            }

            // Re-issue the auth cookie with the new IsAttendee claim
            User.MakeAuthorised();
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, User);

            return RedirectToPage("/Index");
        }
    }
}