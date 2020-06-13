using System;
using System.Collections.Generic;

namespace Keeper.Data.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; }
        public string Currency { get; set; }
        public double HourlyRate { get; set; }
        public bool Archive { get; set; }
        public IEnumerable<ProjectTask> Tasks {get;set;}
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }

        // FK
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
