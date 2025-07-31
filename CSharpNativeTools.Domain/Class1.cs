using CSharpNativeTools.ResultPattern;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace CSharpNativeTools.Domain
{
    public record User(string Name);
    public record Project(string Name);

    public class UserRepositoryLoggingDecorator : IUserRepository
    {
       
        private readonly IUserRepository _inner;
        public UserRepositoryLoggingDecorator(IUserRepository inner)
        {
            _inner = inner;
        }
        public Result<User> GetUserByName(string name)
        {
            var result = _inner.GetUserByName(name);
            if (result.IsSuccessFull())
            {
                Console.WriteLine($"User found: {result.Value.Name}");
            }
            else
            {
                Console.WriteLine($"Error: {result.Error?.ErrorMessage}");
            }
            return result;
        }
        public async Task<Result<User>> GetUserByNameAsync(string name)
        {
            var result = await _inner.GetUserByNameAsync(name);
            if (result.IsSuccessFull())
            {
                Console.WriteLine($"User found: {result.Value.Name}");
            }
            else
            {
                Console.WriteLine($"Error: {result.Error?.ErrorMessage}");
            }
            return result;
        }
    }
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

    public class ProjectRepositoryLoggingDecorator : IProjectRepository
    {
        private readonly IProjectRepository _inner;
        public ProjectRepositoryLoggingDecorator(IProjectRepository inner)
        {
            _inner = inner;
        }
        public Result<Project> GetProjectByUser(User name)
        {
            var result = _inner.GetProjectByUser(name);
            if (result.IsSuccessFull())
            {
                Console.WriteLine($"Project found: {result.Value.Name}");
            }
            else
            {
                Console.WriteLine($"Error: {result.Error?.ErrorMessage}");
            }
            return result;
        }
        public async Task<Result<Project>> GetProjectByUserAsync(User name)
        {
            var result = await _inner.GetProjectByUserAsync(name);
            if (result.IsSuccessFull())
            {
                Console.WriteLine($"Project found: {result.Value.Name}");
            }
            else
            {
                Console.WriteLine($"Error: {result.Error?.ErrorMessage}");
            }
            return result;
        }
    }

    public class ProjectRepository : IProjectRepository
    {
        public Result<Project> GetProjectByUser(User user)
        {
            return user switch
            {
                { Name: var name } when name.StartsWith("A") => new Project("ProjectA"),
                _ => new NotFound($"User has no project")
            };
        }

        public async Task<Result<Project>> GetProjectByUserAsync(User user)
        {
            return user switch
            {
                { Name: var name } when name.StartsWith("A") => new Project("ProjectA"),
                _ => new NotFound($"User has no project")
            };
        }
    }
}
