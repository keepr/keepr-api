namespace Keeper.API.InputModels
{
    public class ProjectInputModel
    {
        public string Name { get; set; }
        public double? Budget { get; set; }
        public string Currency { get; set; }
        public double? HourlyRate { get; set; }
    }
}
