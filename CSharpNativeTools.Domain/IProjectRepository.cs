using CSharpNativeTools.ResultPattern;
using GeneratedDI;

namespace CSharpNativeTools.Domain
{
    [Decorate(DecoratingType.Diagnostics)]
    public interface IProjectRepository
    {
        Result<Project> GetProjectByUser(User user, (string s, int i) test0, out string test);
        (Project result, string dia) GetProjectByUser(User user, ref string test);
        Task<Result<Project>> GetProjectByUserAsync(User user);
        Task<Result<Tripple<string, User, int>>> TestMethod(Tripple<User, string, bool> inputTripple, IUserRepository userRepository);
    }
}

public class Tripple<T1, T2, T3>
{
    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }
    public T3 Item3 { get; set; }
}