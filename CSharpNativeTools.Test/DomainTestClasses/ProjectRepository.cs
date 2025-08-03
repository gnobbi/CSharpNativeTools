using Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses.Model;

namespace Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses;
public class ProjectRepository : IProjectRepository
{
    public Project TestMethodWithValueTuple(User user, (string s, int i) test0, out string test)
    {
        test = "testValue"; // Example value for the out parameter
        return user switch
        {
            { Name: var name } when name.StartsWith("A") => new Project("ProjectA"),
            _ => new Project("DefaultProject")
        };
    }

    public (Project result, string dia) GetProjectByUser(User user, ref string test)
    {
        return (new Project("ProjectA"), "sdfsd");
    }

    public async Task<Project> GetProjectByUserAsync(User user)
    {
        return user switch
        {
            { Name: var name } when name.StartsWith("A") => new Project("ProjectA"),
            _ => new Project("DefaultProject")
        };
    }

    public Task<Tripple<string, User, int>> ComplexTypeGeneric(Tripple<User, string, bool> inputTripple, IUserRepository userRepository)
    {
        return Task.FromResult(
            new Tripple<string, User, int>
            {
                Item1 = "TestString",
                Item2 = inputTripple.Item1,
                Item3 = 42
            }
        );
    }

    public async Task VoidFunctionAsync()
    {
        await Task.Delay(1000); // Simulate some asynchronous operation
    }

    public void VoidFunction()
    {
    }
}
