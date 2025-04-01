using System.Linq.Expressions;
using Microsoft.VisualBasic;
using TurqoiseEatary.Domain.Common.Menu.ValueObjects;
using TurqoiseEatary.Domain.Common.Models;

namespace TurqoiseEatary.Domain.Host;

public sealed class HostId : ValueObject
{
    public Guid Value { get; }
    private HostId(Guid value)
    {
        Value = value;
    }

    public static HostId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static HostId Create(Guid value)
    {
        return new HostId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
