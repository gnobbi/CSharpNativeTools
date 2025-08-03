using System.Runtime.Serialization;

namespace Gnobbi.DebugTools.Decorator.SourceCodeGenerator
{
    [DataContract]
    public enum DiagnosticConfigType
    {
        [EnumMember]
        All = 0,
        [EnumMember]
        Namespaces = 1,
        [EnumMember]
        Attributes = 2
    }
}
