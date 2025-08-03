using Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses.Model;

namespace Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses
{
    [Decorate(DecoratingType.Diagnostics)]
    public interface IProjectRepository
    {
        Project TestMethodWithValueTuple(User user, (string s, int i) test0, out string test);
        Task<Project> GetProjectByUserAsync(User user);
        (Project result, string dia) GetProjectByUser(User user, ref string test);
        Task<Tripple<string, User, int>> ComplexTypeGeneric(Tripple<User, string, bool> inputTripple, IUserRepository userRepository);
        Task VoidFunctionAsync();
        void VoidFunction();
    }
}
