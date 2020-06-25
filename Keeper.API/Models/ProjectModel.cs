using System;
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

            if (project.Client != null)
            {
                this.Client = new ClientModel(project.Client);
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; }
        public string Currency { get; set; }
        public double HourlyRate { get; set; }
        public bool Archive { get; set; }
        public ClientModel Client { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
    }
}
