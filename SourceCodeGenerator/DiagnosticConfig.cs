using System;
using System.Runtime.Serialization;

namespace Gnobbi.DebugTools.Decorator.SourceCodeGenerator
{
    [DataContract]
    public class DiagnosticConfig
    {
        [DataMember]
        public DiagnosticConfigType Type { get; set; } = DiagnosticConfigType.Attributes;

        [DataMember]
        public string[] ExcludeInterfaces { get; set; } = Array.Empty<string>();

        [DataMember]
        public string[] IncludeNamespaces { get; set; } = Array.Empty<string>();
    }
}
