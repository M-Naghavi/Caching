﻿using InMemory_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemory_Project.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext
            (DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
