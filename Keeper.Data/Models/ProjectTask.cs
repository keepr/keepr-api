using System;

namespace Keeper.Data.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }

        // FK
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
