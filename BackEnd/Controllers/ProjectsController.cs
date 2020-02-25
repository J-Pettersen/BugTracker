using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using DTO;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private readonly TrackerDBContext _db;

        public ProjectsController(TrackerDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectResponse>>> GetProjects()
        {
            var projects = await _db.Projects.AsNoTracking()
                                .Include(s => s.ProjectManager)
                                .Include(s => s.UsersProjects)
                                    .ThenInclude(ss => ss.User)
                                .Select(s => s.MapProjectResponse())
                                .ToListAsync();

            return projects;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResponse>> GetProject(int id)
        {
            var project = await _db.Projects//.AsNoTracking()
                                            .Include(s => s.UsersProjects)
                                                .ThenInclude(ss => ss.User)
                                            .SingleOrDefaultAsync(s => s.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var result = project.MapProjectResponse();
            return result;
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<List<UserResponse>>> GetUsers(int id)
        {
            var users = await _db.Users//.AsNoTracking()
                                        .Include(up => up.UsersProjects)
                                         .ThenInclude(u => u.User)
                                        .Where(up => up.UsersProjects.Any(p => p.Project.Id == id))
                                        .Select(u => u.MapUserResponse())
                                        .ToListAsync();

            return users;            
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponse>> PostProject(DTO.Project input)
        {
            var project = new Data.Project
            {
                Title = input.Title,
                Description = input.Description,
                ProjectManagerId = input.ProjectManagerId
            };

            _db.Projects.Add(project);
            await _db.SaveChangesAsync();

            var result = project.MapProjectResponse();

            return CreatedAtAction(nameof(GetProject),
                                    new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, DTO.Project input)
        {
            var project = await _db.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            project.Title = input.Title;
            project.Description = input.Description;
            project.ProjectManagerId = input.ProjectManagerId;

            await _db.SaveChangesAsync();

            return NoContent();           

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectResponse>> DeleteProject(int id)
        {
            var project = await _db.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();

            return project.MapProjectResponse();
        }
    }
}