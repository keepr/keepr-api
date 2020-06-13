using System;

namespace Keeper.API.InputModels
{
    public class ProjectTaskInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Hours { get; set; }
        public DateTimeOffset? Date { get; set; }
    }
}
