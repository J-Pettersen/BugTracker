using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using BackEnd.Data;
using DTO;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TrackerDBContext _db;

        public UsersController(TrackerDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            var users = await _db.Users.AsNoTracking()
                                .Include(p => p.UsersProjects)
                                    .ThenInclude(pp => pp.Project)
                                .Select(p=>p.MapUserResponse())
                                .ToListAsync();

            return users;
        }

        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserResponse>> GetUser(string email)
        {
            var user = await _db.Users.Include(p => p.UsersProjects)
                                    .ThenInclude(pp => pp.Project)
                                .SingleOrDefaultAsync(p => p.EmailAddress == email);

            if (user == null)
            {
                //user with given email does not exist
                return NotFound();
            }

            var result = user.MapUserResponse();

            return result ;
        }

        [HttpGet("{email}/projects")]
        public async Task<ActionResult<List<ProjectResponse>>> GetProjects(string emailAddress)
        {
            var projects = await _db.Projects.AsNoTracking()
                                             .Include(p => p.ProjectManager)
                                             .Include(u => u.UsersProjects)
                                                 .ThenInclude(p => p.Project)
                                             .Where(up => up.UsersProjects.Any(u => u.User.EmailAddress == emailAddress))
                                             .Select(p => p.MapProjectResponse())
                                             .ToListAsync();

            return projects;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserResponse>> Post(DTO.User input)
        {
            // Check if the user already exists
            var userExists = await _db.Users
                .Where(u => u.EmailAddress == input.EmailAddress)
                .FirstOrDefaultAsync();

            if (userExists != null)
            {
                //Email Address already in use
                return Conflict(input);
            }

            var user = new Data.User
            {
                Name = input.Name,
                EmailAddress = input.EmailAddress,
                Role = input.Role,
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var result = user.MapUserResponse();

            return CreatedAtAction(nameof(GetUser), new { emailAddress = result.EmailAddress }, result);
        }

    }
}