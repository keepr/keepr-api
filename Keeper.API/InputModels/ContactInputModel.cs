namespace Keeper.API.InputModels
{
    public class ContactInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? Primary { get; set; }
    }
}
