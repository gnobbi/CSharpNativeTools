using CSharpNativeTools.Domain;
using CSharpNativeTools.ResultPattern;

namespace SharpNativeTools.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var repo2 = new StopwatchIProjectRepository(new LoggingIProjectRepository(new ProjectRepository()));

            var logger =  new StopwatchIUserRepository(new LoggingIUserRepository(new UserRepository()));
            "input"
             .Bind(logger.GetUserByName)
             .Bind(repo2.GetProjectByUser);

            "Alice"
             .Bind(logger.GetUserByName)
             .Bind(repo2.GetProjectByUser);
        }
    }
}
