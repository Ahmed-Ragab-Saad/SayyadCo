namespace SayyadCo.Application.Common.Results
{
    public class BaseResult
    {
        public bool IsSuccess { get; protected set; }
        public string Error { get; protected set; }
        public ResultStatus Status { get; protected set; }

        protected BaseResult(bool isSuccess, string error, ResultStatus status)
        {
            IsSuccess = isSuccess;
            Error = error;
            Status = status;
        }

        public static BaseResult Success() =>
            new BaseResult(true, null, ResultStatus.Success);

        public static BaseResult Failure(string error) =>
            new BaseResult(false, error, ResultStatus.Failure);

        public static BaseResult NotFound(string error = "Not Found") =>
            new BaseResult(false, error, ResultStatus.NotFound);
    }
}
