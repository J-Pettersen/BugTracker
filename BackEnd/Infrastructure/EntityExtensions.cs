using System.Linq;

namespace BackEnd.Data
{
    public static class EntityExtensions
    {
        public static DTO.UserResponse MapUserResponse(this User user) =>
            new DTO.UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                EmailAddress = user.EmailAddress,
                Role = user.Role,
                Projects = user.UsersProjects?
                            .Select(up => new DTO.Project
                            {
                                Id = up.ProjectId,
                                Title = up.Project.Title
                            })
                            .ToList()
            };

        public static DTO.ProjectResponse MapProjectResponse(this Project project) =>
            new DTO.ProjectResponse
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ProjectManagerId = project.ProjectManagerId,
                ProjectManager = new DTO.User
                {
                    Id = project?.ProjectManagerId ?? 0,
                    Name = project.ProjectManager?.Name
                },
                Users = project.UsersProjects?
                                .Select(up => new DTO.User
                                {
                                    Id = up.UserId,
                                    Name = up.User.Name
                                })
                                .ToList()
            };

        public static DTO.TicketResponse MapTicketResponse(this Ticket ticket) =>
            new DTO.TicketResponse
            {

                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Status = ticket.Status,
                TicketType = ticket.TicketType,
                CreatedOn = ticket.CreatedOn,
                AssignedDevId = ticket.AssignedDevId,
                AssignedDev = new DTO.User
                {
                    Id = ticket?.AssignedDevId ?? 0,
                    Name = ticket.AssignedDev?.Name
                },
                SubmittedById = ticket.SubmittedById,
                SubmittedBy = new DTO.User
                {
                    Id = ticket?.SubmittedById ?? 0,
                    Name = ticket.SubmittedBy?.Name
                },
                ProjectId = ticket.ProjectId,
                Project = new DTO.Project
                {
                    Id = ticket?.ProjectId ?? 0,
                    Title = ticket.Project?.Title,
                }
            };
    }
}
