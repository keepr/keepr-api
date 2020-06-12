using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keeper.Data.Models;

namespace Keeper.API.Models
{
    public class ProjectTaskModel
    {
        public ProjectTaskModel() { }

        public ProjectTaskModel(ProjectTask task)
        {
            this.Id = task.Id;
            this.Name = task.Name;
            this.Description = task.Description;
            this.Hours = task.Hours;
            this.Date = task.Date;
            this.Created = task.Created;
            this.Modified = task.Modified;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
