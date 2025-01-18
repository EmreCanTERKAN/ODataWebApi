using Ardalis.SmartEnum;
using Bogus.DataSets;

namespace ODataWebApi.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName => string.Join("", FirstName, LastName);
        public UserType UserType { get; set; } = UserType.User;
        public Address Adress { get; set; } = default!;
    }
}

public sealed class UserType : SmartEnum<UserType>
{
    public static UserType User = new("User",0);
    public static UserType Admin = new("Admin", 1);

    public UserType(string name, int value) : base(name, value)
    {
    }
}

public sealed record Address(
    string City,
    string Town,
    string FullAdress);
