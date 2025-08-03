using Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses.Model;
namespace Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses;

public class UserRepository : IUserRepository
{
    public User GetUserByName(string name)
    {
        return new User("Alice");
    }

    public async Task<User> GetUserByNameAsync(string name)
    {
        return new User("Alice");
    }
}
