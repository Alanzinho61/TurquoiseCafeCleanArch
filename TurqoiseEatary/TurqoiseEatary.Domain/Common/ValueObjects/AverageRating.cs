using System.ComponentModel.DataAnnotations;
using TurqoiseEatary.Domain.Common.Models;

namespace TurqoiseEatary.Domain.Common.ValueObjects;

// public sealed class AverageRating : ValueObject
// {
//     public double Value { get; private set; }
//     public int TotalRatings { get; private set; }
//     public int TotalScore { get; private set; }

//     // private AverageRating(){}
//     private AverageRating(double value, int totalRatings, int totalScore)
//     {
//         Value = value;
//         TotalRatings = totalRatings;
//         TotalScore = totalScore;
//     }

//     public AverageRating AddRating(int score)
//     {
//         int newTotalRatings = TotalRatings + 1;
//         int newTotalScore = TotalScore + score;
//         double newAverage = newTotalScore / newTotalRatings; // Burada tur donusumu gerekebilir
//         return new(newAverage, newTotalRatings, newTotalScore);

//     }
//     public override IEnumerable<object> GetEqualityComponents()
//     {
//         yield return Value;
//         yield return TotalRatings;
//         yield return TotalScore;
//     }
// }

public sealed class AverageRating : ValueObject
{
    private AverageRating(double value, int numRatings)
    {
        Value = value;

        NumRatings = numRatings;
    }

    public double Value { get; private set; }
    public int NumRatings { get; private set; }

    public static AverageRating CreateNew(double rating = 0, int numRatings = 0)
    {
        return new AverageRating(rating, numRatings);
    }

    public void AddNewRating(Rating rating)
    {
        Value = ((Value * NumRatings) + rating.Value) / ++NumRatings;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
