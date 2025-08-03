using CSharpNativeTools.ResultPattern;
using GeneratedDI;
using System.ComponentModel;

namespace CSharpNativeTools.Domain
{
    [Decorate(DecoratingType.Diagnostics)]
    public interface IUserRepository
    {
        Result<User> GetUserByName(string name);
        Task<Result<User>> GetUserByNameAsync(string name);
    }
}