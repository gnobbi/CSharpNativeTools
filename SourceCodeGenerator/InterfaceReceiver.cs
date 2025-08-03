using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gnobbi.DebugTools.Decorator.SourceCodeGenerator
{
    public class InterfaceReceiver : ISyntaxReceiver
    {
        private GeneratorExecutionContext _context;
        private DiagnosticConfig _config;
        private bool _initialized = false;

        public List<InterfaceDeclarationSyntax> CandidateInterfaces { get; } = new List<InterfaceDeclarationSyntax>();

        public bool IsAcceped(INamedTypeSymbol ifaceSymbol)
        {
            if(!_initialized)
            {
                throw new Exception("Not initialized call SetContext");
            }

            var interfaceName = ifaceSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
            var namespaceName = ifaceSymbol.ContainingNamespace?.ToDisplayString() ?? string.Empty;
            if (_config == null || _config.Type == DiagnosticConfigType.Attributes)
            {
                return HasAttributeSet(ifaceSymbol);
            }

            if (_config != null && _config.Type == DiagnosticConfigType.All)
            {
                if (_config.ExcludeInterfaces != null && _config.ExcludeInterfaces.Contains(interfaceName))
                {
                    return HasAttributeSet(ifaceSymbol);
                }
                else
                {
                    return true;
                }
            }

            if (_config != null
                && _config.Type == DiagnosticConfigType.Namespaces
                && _config.IncludeNamespaces != null
                && _config.IncludeNamespaces.Length > 0
                && _config.IncludeNamespaces.Contains(namespaceName))
            {
                if (_config.ExcludeInterfaces != null && _config.ExcludeInterfaces.Contains(interfaceName))
                {
                    return HasAttributeSet(ifaceSymbol);
                }
                else
                {
                    return true;
                }
            }

            return HasAttributeSet(ifaceSymbol);
        }
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is InterfaceDeclarationSyntax ifaceDecl)
            {
                CandidateInterfaces.Add(ifaceDecl);
            }
        }

        private bool HasAttributeSet(INamedTypeSymbol ifaceSymbol)
        {
            var decorateAttr = ifaceSymbol.GetAttributes().FirstOrDefault(a => a.AttributeClass?.Name == "DecorateAttribute");

            if (decorateAttr == null)
            {
                return false;
            }

            var flags = (int)decorateAttr.ConstructorArguments[0].Value;
            if ((flags & 1) == 0) // 1 = Diagnostics is set
            {
                return false;
            }

            return true;
        }

        public void SetContext(GeneratorExecutionContext context)
        {
            _initialized = true;
            _context = context;
            _config = context.GetDiagnosticConfig();
        }
    }
}
