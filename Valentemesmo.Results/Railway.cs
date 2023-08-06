using System;
using System.Threading.Tasks;

namespace ValenteMesmo.Results
{
    /// <summary>
    /// Either a <see cref="T"/> or a <see cref="Exception"/>.<br/>
    /// An implementation of <see href="https://www.google.com/search?q=railway+error+handling">Railway error handling</see>
    /// </summary>
    /// <typeparam name="TSuccess">Type of expected value when nothing goes wrong</typeparam>
    public class Result<T>
    {
        internal readonly T value;
        internal readonly Exception ex;
        internal readonly bool isSuccessful;

        public Result(T value)
        {
            this.value = value;
            isSuccessful = true;
            ex = null;
        }

        public Result(Exception ex)
        {
            value = default;
            this.ex = ex;
            isSuccessful = false;
        }

        public static implicit operator Result<T>(T value) =>
            new Result<T>(value);

        public static implicit operator Result<T>(Exception ex) =>
            new Result<T>(ex);


        public static explicit operator T(Result<T> either)
            => either.value;

        public static explicit operator Exception(Result<T> either)
            => either.ex;

        public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Exception, TResult> onFailure) =>
            isSuccessful
                ? onSuccess(value)
                : onFailure(ex);

        public Result<TResult> Pipe<TResult>(Func<T, TResult> onSuccess) =>
            isSuccessful
                ? onSuccess(value)
                : new Result<TResult>(ex);

        public Result<TResult> Pipe<TResult>(Func<T, Result<TResult>> onSuccess) =>
          isSuccessful
              ? onSuccess(value)
              : new Result<TResult>(ex);


        public async Task<Result<TResult>> Pipe<TResult>(Func<T, Task<Result<TResult>>> onSuccess) =>
            isSuccessful
                ? await onSuccess(value)
                : new Result<TResult>(ex);
    }
}