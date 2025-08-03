using CSharpNativeTools.ResultPattern;

namespace CSharpNativeTools.Domain
{
    public class UserRepository : IUserRepository
    {
        public Result<User> GetUserByName(string name)
        {
            return name switch
            {
                "Alice" => new User("Alice"),
                _ => new NotFound($"User with name {name} not found.")
            };
        }

        public async Task<Result<User>> GetUserByNameAsync(string name)
        {
            return name switch
            {
                "Alice" => new User("Alice"),
                _ => new NotFound($"User with name {name} not found.")
            };
        }
    }
}
