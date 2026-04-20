namespace SayyadCo.Application.Common.Results
{
    public class Result<T> : BaseResult
    {
        public T Data { get; private set; }
        public string? VerificationToken { get; private set; }


        private Result(T data, bool isSuccess, string error, ResultStatus status, string? verificationToken = null)
            : base(isSuccess, error, status)
        {
            Data = data;
            VerificationToken = verificationToken;
        }

        public static Result<T> Success(T data)
            => new Result<T>(data, true, null, ResultStatus.Success);

        public static Result<T> Failure(string error)
            => new Result<T>(default, false, error, ResultStatus.Failure);

        public static Result<T> NotFound(string error = "Not Found")
            => new Result<T>(default, false, error, ResultStatus.NotFound);

        public static Result<T> UnverifiedEmail(string verificationToken)
            => new(default, false, "Email is not verified", ResultStatus.UnverifiedEmail, verificationToken);

        public static Result<T> Unauthorized(string error = "Unauthorized")
            => new(default, false, error, ResultStatus.Unauthorized);

        public static Result<T> Forbidden(string error)
            => new(default, false, error, ResultStatus.Forbidden);
    }
}
