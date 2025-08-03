namespace Gnobbi.DebugTools.Decorator;

[Flags]
public enum DecoratingType
{
    Diagnostics = 1
}

[AttributeUsage(AttributeTargets.Interface)]
public sealed class DecorateAttribute : Attribute
{
    public DecoratingType Type { get; }
    public DecorateAttribute(DecoratingType type) => Type = type;
}

