using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace FrontEnd.Services
{
    public interface IApiClient
    {
        Task<List<ProjectResponse>> GetProjects();
        Task<ProjectResponse> GetProject(int id);
        Task<List<ProjectResponse>> GetProjectsByUser(String email);
        Task<bool> PostProject(Project project);
        Task PutProject(Project project);
        Task DeleteProject(int id);

        Task<List<UserResponse>> GetUsers();
        Task<UserResponse> GetUser(string email);
        Task<bool> PostUser(User User);
        Task PutUser(User User);

        Task<List<TicketResponse>> GetTickets();
        Task<TicketResponse> GetTicket(int id);
        Task<bool> PostTicket(Ticket ticket);
        Task PutTicket(Ticket ticket);




    }
}
