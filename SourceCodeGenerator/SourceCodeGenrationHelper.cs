using Microsoft.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Gnobbi.DebugTools.Decorator.SourceCodeGenerator
{
    public static class SourceCodeGenrationHelper
    {
        public static DiagnosticConfig GetDiagnosticConfig(this GeneratorExecutionContext context)
        {
            try
            {
                var file = context.AdditionalFiles.FirstOrDefault(f => f.Path.EndsWith("Gnobbi.DebugTools.Decorator.Config.json"));
                var json = file?.GetText(context.CancellationToken)?.ToString();
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }

                json = json.Replace("\"All\"", "0").Replace("\"Namespaces\"", "1").Replace("\"Attributes\"", "2");
                var serializer = new DataContractJsonSerializer(typeof(DiagnosticConfig));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                return (DiagnosticConfig)serializer.ReadObject(ms);
            }catch
            {
                return null;
            }

        }
    }
}
