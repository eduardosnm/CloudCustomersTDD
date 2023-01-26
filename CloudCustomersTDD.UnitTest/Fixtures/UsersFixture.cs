using CloudCustomersTDD.API.Models;

namespace CloudCustomersTDD.UnitTest.Fixtures;

public static class UsersFixture
{
    public static List<User> GetTestUsers() => new()
    {
        new User
        {
            Id = 1,
            Name = "Jane",
            Address = new Address()
            {
                Street = "123 Main st",
                City = "Madison",
                ZipCode = "12312"
            },
            Email = "jane@example.com"
        },
        new User
        {
            Id = 2,
            Name = "Jane2",
            Address = new Address()
            {
                Street = "123 Main st",
                City = "Madison",
                ZipCode = "12312"
            },
            Email = "jane2@example.com"
        },
        new User
        {
            Id = 3,
            Name = "Jane3",
            Address = new Address()
            {
                Street = "123 Main st",
                City = "Madison",
                ZipCode = "12312"
            },
            Email = "jane3@example.com"
        }
    };
}