using CSharpNativeTools.ResultPattern;
using System.ComponentModel;

namespace CSharpNativeTools.Domain
{
    [Description("Project")]
    public interface IProjectRepository
    {
        Result<Project> GetProjectByUser(User user);
        Task<Result<Project>> GetProjectByUserAsync(User user);
    }
}