using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DTO;

namespace FrontEnd.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }





        //project methods
        public async Task<List<ProjectResponse>> GetProjects()
        {
            var response = await _httpClient.GetAsync("/api/projects");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<ProjectResponse>>();
        }

        public async Task<ProjectResponse> GetProject(int id)
        {
            var response = await _httpClient.GetAsync($"/api/projects/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ProjectResponse>();
        }

        public async Task<bool> PostProject(Project project)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/projects", project);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task PutProject(Project project)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/projects/{project.Id}", project);

            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteProject(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/projects/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }
            
            response.EnsureSuccessStatusCode();
        }





        //User methdos

        public async Task<List<UserResponse>> GetUsers()
        {
            var response = await _httpClient.GetAsync("/api/users");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<UserResponse>>();
        }

        public async Task<UserResponse> GetUser(string email)
        {
            var response = await _httpClient.GetAsync($"/api/users/{email}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            
           return await response.Content.ReadAsAsync<UserResponse>();
        }

        public async Task<bool> PostUser(User user )
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/users", user);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task PutUser(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/users/{user.EmailAddress}", user);

            response.EnsureSuccessStatusCode();
        }







        //ticket methods

        public async Task<List<TicketResponse>> GetTickets()
        {
            var response = await _httpClient.GetAsync("/api/tickets");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<TicketResponse>>();
        }

        public async Task<TicketResponse> GetTicket(int id)
        {
            var response = await _httpClient.GetAsync($"/api/tickets/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TicketResponse>();
        }

        public async Task<bool> PostTicket(Ticket ticket)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/tickets", ticket);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task PutTicket(Ticket ticket)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/tickets/{ticket.Id}", ticket);

            response.EnsureSuccessStatusCode();
        }






        ////cross class methods

        public async Task<List<ProjectResponse>> GetProjectsByUser(string email)
        {
            var response = await _httpClient.GetAsync($"/api/users/{email}/projects");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<ProjectResponse>>();
        }

        public async Task<List<UserResponse>> GetUsersByProject(int id)
        {
            var response = await _httpClient.GetAsync($"/api/projects/{id}/users");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<UserResponse>>();
        }

        public async Task AddUserToProject(string email, int projectId)
        {
            var response = await _httpClient.PostAsync($"/api/users/{email}/project/{projectId}", null);

            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveUserFromProject(string email, int projectId)
        {
            var response = await _httpClient.DeleteAsync($"/api/users/{email}/project/{projectId}");

            response.EnsureSuccessStatusCode();
        }

    }
}
