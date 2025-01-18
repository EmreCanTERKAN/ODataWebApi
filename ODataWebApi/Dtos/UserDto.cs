namespace ODataWebApi.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public UserType UserType { get; set; } = UserType.User;
        public string UserTypeName { get; set; } 
        public int UserTypeValue { get; set; } 
        public Address Adress { get; set; } = default!;
    }
}
