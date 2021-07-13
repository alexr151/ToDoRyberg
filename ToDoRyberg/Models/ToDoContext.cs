using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoRyberg.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "work", Name = "Work"},
                new Category { CategoryId = "home", Name = "Home" },
                new Category { CategoryId = "ex", Name = "Exercise" },
                new Category { CategoryId = "shop", Name = "Shopping" },
                new Category { CategoryId = "call", Name = "Contact" }
                );

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "open", Name = "Open"},
                new Status { StatusId = "closed", Name = "Completed"},
                new Status { StatusId = "inprogress", Name = "In Progress"},
                new Status { StatusId = "qa", Name = "Quality Assurance"}
                );

            modelBuilder.Entity<Priority>().HasData(
                new Priority { PriorityId = "urgent", Name = "Urgent"},
                new Priority { PriorityId = "high", Name = "High"},
                new Priority { PriorityId = "moderate", Name = "Moderate"},
                new Priority { PriorityId = "low", Name = "Low"}
                );
        }


    }
}
