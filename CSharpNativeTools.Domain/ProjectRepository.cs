using CSharpNativeTools.ResultPattern;

namespace CSharpNativeTools.Domain
{
    public class ProjectRepository : IProjectRepository
    {
        public Result<Project> GetProjectByUser(User user, (string s, int i) test0, out string test)
        {
            test = "testValue"; // Example value for the out parameter
            return user switch
            {
                { Name: var name } when name.StartsWith("A") => new Project("ProjectA"),
                _ => new NotFound($"User has no project")
            };
        }

        public (Project result, string dia) GetProjectByUser(User user, ref string test)
        {
            return (new Project("ProjectA"), "sdfsd");
        }

        public async Task<Result<Project>> GetProjectByUserAsync(User user)
        {
            return user switch
            {
                { Name: var name } when name.StartsWith("A") => new Project("ProjectA"),
                _ => new NotFound($"User has no project")
            };
        }

        public Task<Result<Tripple<string, User, int>>> TestMethod(Tripple<User, string, bool> inputTripple, IUserRepository userRepository)
        {
            return Task.FromResult<Result<Tripple<string, User, int>>>(
                new Tripple<string, User, int>
                {
                    Item1 = "TestString",
                    Item2 = inputTripple.Item1,
                    Item3 = 42
                }
            );
        }
    }
}
