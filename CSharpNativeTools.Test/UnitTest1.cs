using CSharpNativeTools.Domain;
using CSharpNativeTools.ResultPattern;
using System.Threading.Tasks;

namespace CSharpNativeTools.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var repo = new UserRepositoryLoggingDecorator(new UserRepository());
            var repo2 = new ProjectRepository();
            var result = "input"
                .Bind(repo.GetUserByName)
                .Bind(repo2.GetProjectByUser);

            var resul = await "input"
             .Bind(repo.GetUserByNameAsync)
             .Bind(repo2.GetProjectByUser);

            if ( result.IsSuccessFull())
            {
                Assert.That(result.Value.Name, Is.EqualTo("ProjectA"));
            }
            else
            {
                string s = result.Error.ErrorMessage;
                Assert.Fail($"Expected success but got error: {result.Error.ErrorMessage}");
            }
        }
    }
}
