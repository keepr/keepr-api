namespace Keeper.API.InputModels
{
    public class UserUpdateInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Currency { get; set; }
        public double? HourlyRate { get; set; }
    }
}
