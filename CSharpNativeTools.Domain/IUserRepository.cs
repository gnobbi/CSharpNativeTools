using CSharpNativeTools.ResultPattern;
using System.ComponentModel;

namespace CSharpNativeTools.Domain
{
    [Description("User Repository: ")]
    public interface IUserRepository
    {
        Result<User> GetUserByName(string name);
        Task<Result<User>> GetUserByNameAsync(string name);
    }
}