using System.Collections.Immutable;
using TurqoiseEatary.Domain.Common.Models;

namespace TurqoiseEatary.Domain.Common.ValueObjects;

public sealed class Rating : ValueObject
{
    public Rating(double value)
    {
        if (value < 0 || value > 5)
            throw new ArgumentException("Rating must be between 0 and 5");
        Value = value;
    }
    public double Value { get; private set; }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}