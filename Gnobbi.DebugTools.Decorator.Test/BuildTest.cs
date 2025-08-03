using Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses;
using Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Gnobbi.DebugTools.Decorator.Decorator.Test
{
    public class BuildTest
    {

        private ServiceProvider? _serviceProvider;
        [SetUp]
        public void Setup()
        {
            var serivceCollection = new ServiceCollection();
            serivceCollection.AddSingleton<IUserRepository, UserRepository>();
            serivceCollection.AddSingleton<IProjectRepository, ProjectRepository>();

            serivceCollection.AddSingleton<IDiagnosticEntryHandler, FakeDiagnosticEntryHandler>();
            ServiceDecoratorRegistration.RegisterDecorators(serivceCollection);
            _serviceProvider = serivceCollection.BuildServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
            _serviceProvider?.Dispose();
            _serviceProvider = null;
        }

        [Test]
        public async Task SimpleTest()
        {
            // Arrange
            var projectRepository = _serviceProvider!.GetRequiredService<IProjectRepository>();
            var userRepository = _serviceProvider!.GetRequiredService<IUserRepository>();
            var fakeHandler = _serviceProvider!.GetRequiredService<IDiagnosticEntryHandler>() as FakeDiagnosticEntryHandler;

            // Act 
            var user = userRepository.GetUserByName("Alice");
            var user2 = await userRepository.GetUserByNameAsync("Alice");
            var project = projectRepository.TestMethodWithValueTuple(user, ("test", -1), out string test);
            var project2 = await projectRepository.GetProjectByUserAsync(user);
            var tripple = await projectRepository.ComplexTypeGeneric(new Tripple<User, string, bool> { Item1 = user, Item2 = "halo", Item3 = false }, userRepository);
            await projectRepository.VoidFunctionAsync();
            projectRepository.VoidFunction();

            // Assert
            Assert.That(projectRepository.GetType(), Is.EqualTo(typeof(ProjectRepository_DiagnosticDecorator)));
            Assert.That(userRepository.GetType(), Is.EqualTo(typeof(UserRepository_DiagnosticDecorator)));
            Assert.That(fakeHandler?.Entries.Count, Is.EqualTo(7));
        }
    }
}
