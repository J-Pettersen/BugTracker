﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ProjectResponse : Project
    {
        public User ProjectManager { get; set; }
        public ICollection<User> Users { get; set; }
    }
}