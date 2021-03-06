﻿using DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
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
        public bool IsAdmin { get; set; }
        public bool IsAuthorised { get; set; }
        public async Task<IActionResult> OnGet()
        {
            
            IsAuthorised = User.IsAuthorised();
            IsAdmin = User.IsAdmin();
            if (IsAuthorised)
            {
                var email = User.FindFirstValue(ClaimTypes.Name).ToString();
                MyProjects = await _apiClient.GetProjectsByUser(email);
            }            
            return Page();
        }
    }
}