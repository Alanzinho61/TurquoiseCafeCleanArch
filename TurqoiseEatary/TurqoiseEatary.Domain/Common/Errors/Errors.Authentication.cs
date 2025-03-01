using ErrorOr;

namespace TurqoiseEatary.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvlidCredentials",
            description: "Invalid Credentials.");

    }
}