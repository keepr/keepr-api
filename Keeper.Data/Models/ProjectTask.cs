using System;

namespace Keeper.Data.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        // FK
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
