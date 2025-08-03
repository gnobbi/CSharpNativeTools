using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratedDI;

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

