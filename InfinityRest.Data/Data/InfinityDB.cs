using System;
using System.Collections.Generic;
using System.Linq;
using InfinityRest.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using InfinityRest.Shared.Enums;

namespace InfinityRest.Data.Data
{
    public class InfinityDB : DbContext
    {
        public InfinityDB(DbContextOptions<InfinityDB> options)
            : base(options)
        { }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Run> Runs { get; set; }
        public DbSet<ProcessState> ProcessStates { get; set; }
        public DbSet<TaskSettings> TaskSettings { get; set; }
        public DbSet<TaskRunType> TaskRunTypes { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<TaskType>().Property(c => c.Id).ValueGeneratedNever(); // leave it top
            modelBuilder.Entity<ProcessState>().Property(c => c.Id).ValueGeneratedNever(); // leave it top
            modelBuilder.Entity<TaskRunType>().Property(c => c.Id).ValueGeneratedNever(); // leave it top this disables Auto Increment for ID
            

            modelBuilder.AddEnum<ProcessState, ProcessStateEnum>();
            modelBuilder.AddEnum<TaskType, TaskTypeEnum>();
            modelBuilder.AddEnum<TaskRunType, TaskRunTypeEnum>();

            var mockRun = new Run()
            {
                    Id = 1,
                    Date = DateTime.Today,
                    Priority = 100,

            };
            modelBuilder.Entity<Run>().HasData(mockRun);
            var mockTask = new Task()
            {
                    Id = 1,
                    Name = "mockTest",
                    ProcessStateId = 10,
                    RunId = 1,
                    TaskTypeId = 1,
            };
            modelBuilder.Entity<Task>().HasData(mockTask);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
