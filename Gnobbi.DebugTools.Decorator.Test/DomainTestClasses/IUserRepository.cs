using Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses.Model;

namespace Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses;

[Decorate(DecoratingType.Diagnostics)]
public interface IUserRepository
{
    User GetUserByName(string name);
    Task<User> GetUserByNameAsync(string name);
}