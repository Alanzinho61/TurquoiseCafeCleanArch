using TurqoiseEatary.Domain.Common.Models;

namespace TurqoiseEatary.Domain.MenuReview;

public sealed class MenuReviewId : ValueObject
{
    public Guid Value { get; private set; }
    private MenuReviewId(Guid value)
    {
        Value = value;
    }
    public static MenuReviewId CreateNew()
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}