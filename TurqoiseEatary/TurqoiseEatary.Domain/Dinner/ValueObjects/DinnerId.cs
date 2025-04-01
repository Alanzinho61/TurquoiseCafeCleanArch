using TurqoiseEatary.Domain.Common.Models;

namespace TurqoiseEatary.Domain.Dinner;

public sealed class DinnerId : ValueObject
{
    public Guid Value { get; private set; }
    private DinnerId(Guid value)
    {
        Value = value;
    }
    public static DinnerId CreateNew()
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}