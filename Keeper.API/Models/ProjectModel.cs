using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Keeper.Data.Models;

namespace Keeper.API.Models
{
    public class ProjectModel
    {
        public ProjectModel() { }

        public ProjectModel(Project project)
        {
            this.Id = project.Id;
            this.Name = project.Name;
            this.Budget = project.Budget;
            this.Currency = project.Currency;
            this.HourlyRate = project.HourlyRate;
            this.Archive = project.Archive;
            this.Created = project.Created;
            this.Modified = project.Modified;

            if (project.Tasks != null)
            {
                this.Tasks = project.Tasks.Select(x => new ProjectTaskModel(x));
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; }
        public string Currency { get; set; }
        public double HourlyRate { get; set; }
        public bool Archive { get; set; }
        public IEnumerable<ProjectTaskModel> Tasks { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
    }
}
