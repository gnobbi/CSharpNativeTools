using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace CSharpNativeTools.ResultPattern
{
    public class Result<TSuccess>
    {
        public TSuccess? Value { get; private set; }
        public ErrorResultBase? Error { get; private set; }

        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccessFull()
        {
            return Value is not null;
        }

        [MemberNotNullWhen(true, nameof(Error))]
        [MemberNotNullWhen(false, nameof(Value))]
        public bool IsFailure()
        {
            return Error is not null;
        }

        private Result(TSuccess success, ErrorResultBase error)
        {
            Value = success;
            Error = error;
        }

        public static Result<TSuccess> Ok(TSuccess success) => new Result<TSuccess>(success, default!);
        public static Result<TSuccess> Fail(ErrorResultBase error) => new Result<TSuccess>(default!, error);
        public static implicit operator Result<TSuccess>(ErrorResultBase error) => Fail(error);
        public static implicit operator Result<TSuccess>(TSuccess value) => Ok(value);
    }

    public abstract record ErrorResultBase(string ErrorMessage, HttpStatusCode StatusCode);
    public record NotFound : ErrorResultBase
    {
        public NotFound(string errorMessage = "") : base(errorMessage, HttpStatusCode.NotFound) { }
    }


    public static class  ResultExtesnions
    {

        public static Result<TSuccess2> Bind<TSuccess, TSuccess2>(this Result<TSuccess> value, Func<TSuccess, Result<TSuccess2>> func)
        {
            return value.IsSuccessFull() ? func(value.Value) : value.Error;
        }

        public static async Task<Result<TSuccess2>> Bind<TSuccess, TSuccess2>(this Result<TSuccess> value, Func<TSuccess, Task<Result<TSuccess2>>> func)
        {
            var result = value;
            return result.IsSuccessFull() ? await func(result.Value) : result.Error;
        }

        public static async Task<Result<TSuccess2>> Bind<TSuccess, TSuccess2>(this Task<Result<TSuccess>> value, Func<TSuccess, Task<Result<TSuccess2>>> func)
        {
            var result = await value;
            return result.IsSuccessFull() ? await func(result.Value) : result.Error;
        }

        public static async Task<Result<TSuccess2>> Bind<TSuccess, TSuccess2>(this Task<Result<TSuccess>> value, Func<TSuccess, Result<TSuccess2>> func)
        {
            var result = await value;
            return result.IsSuccessFull() ? func(result.Value) : result.Error;
        }

        public static async Task<Result<TSuccess2>> Bind<TSuccess, TSuccess2>(this TSuccess value, Func<TSuccess, Task<Result<TSuccess2>>> func)
        {
            var result = Result<TSuccess>.Ok(value);
            return await result.Bind(func);
        }

        public static Result<TSuccess2> Bind<TSuccess, TSuccess2>(this TSuccess value, Func<TSuccess, Result<TSuccess2>> func)
        {
            var result = Result<TSuccess>.Ok(value);
            return result.Bind(func);
        }
    }
}
