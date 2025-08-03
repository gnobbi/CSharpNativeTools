using CSharpNativeTools.Domain;
using CSharpNativeTools.Domain2;
using Gnobbi.DebugTools.Decorator;
using Microsoft.Extensions.DependencyInjection;

namespace SharpNativeTools.App;

internal class Program
{
    static async Task Main(string[] args)
    {
        var serivceCollection = new ServiceCollection();
        serivceCollection.AddTransient<App>();
        var serviceProvider = App.RegisterServices(serivceCollection)
            .BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<App>();
        await app.Run();
    }
}

public class App
{
    private readonly IProjectRepository projectRepo;
    private readonly IUserRepository userRepo;

    public App(IProjectRepository projectRepo, IUserRepository userRepo)
    {
        this.projectRepo = projectRepo;
        this.userRepo = userRepo;
    }

    public async Task Run()
    {
        var user = userRepo.GetUserByName("Alice");
         var projectResult = projectRepo.GetProjectByUser(user.Value, ("DSFA", 2), out var str);
        var stri = $"ref {str}";
         var v = projectRepo.TestMethod(new Tripple<User, string, bool> { Item1 = user.Value, Item2 = "halo", Item3 = false}, userRepo);
        await projectRepo.VoidFunction();
        projectRepo.VoidFunction2();
    }

    public static ServiceCollection RegisterServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IUserRepository, UserRepository>();
        serviceCollection.AddSingleton<IProjectRepository, ProjectRepository>();

        serviceCollection.AddSingleton<IDiagnosticEntryHandler, MongoDbDiagnosticEntryHandler>();
        ServiceDecoratorRegistration.RegisterDecorators(serviceCollection);

        return serviceCollection;
    }
}