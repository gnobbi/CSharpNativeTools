using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
[Generator(LanguageNames.CSharp)]
public class StopwatchGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new InterfaceReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (!(context.SyntaxReceiver is InterfaceReceiver receiver))
            return;

        var compilation = context.Compilation;

        foreach (var interfaceDecl in receiver.Interfaces)
        {
            var model = compilation.GetSemanticModel(interfaceDecl.SyntaxTree);
            if (!(model.GetDeclaredSymbol(interfaceDecl) is INamedTypeSymbol symbol))
                continue;

            string ns = symbol.ContainingNamespace.ToDisplayString();
            string ifaceName = symbol.Name;
            string className = $"Stopwatch{ifaceName}";

            var methods = symbol.GetMembers().OfType<IMethodSymbol>().Where(m => m.MethodKind == MethodKind.Ordinary);

            var sb = new StringBuilder($@"
namespace {ns}
{{
    public class {className} : {ifaceName}
    {{
        private readonly {ifaceName} _inner;

        public {className}({ifaceName} inner)
        {{
            _inner = inner;
        }}
");

            foreach (var method in methods)
            {
                string returnType = method.ReturnType.ToDisplayString();
                string methodName = method.Name;
                var parameters = string.Join(", ", method.Parameters.Select(p => $"{p.Type.ToDisplayString()} {p.Name}"));
                var args = string.Join(", ", method.Parameters.Select(p => p.Name));

                sb.AppendLine($@"
        public {returnType} {methodName}({parameters})
        {{
           var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            {(returnType == "void" ? "" : "var result = ")}_inner.{methodName}({args});
            stopwatch.Stop();
            Console.WriteLine($""{methodName} Took {{stopwatch.ElapsedMilliseconds}} ms"");
            {(returnType == "void" ? "" : "return result;")}

        }}
");
            }

            sb.AppendLine("    }\n}");

            context.AddSource($"{className}.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }

        
    }

    public static string ResultCode()
    {

        return @"
            if (result.IsSuccessFull())
            {
                Console.WriteLine($""Entity found: {result.Value}"");
            }
            else
            {
                Console.WriteLine($""Error: {result.Error.ErrorMessage}"");
            }
";
    }
    class InterfaceReceiver : ISyntaxReceiver
    {
        public List<InterfaceDeclarationSyntax> Interfaces { get; } = new List<InterfaceDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is InterfaceDeclarationSyntax id)
            {
                Interfaces.Add(id);
            }
        }
    }
}