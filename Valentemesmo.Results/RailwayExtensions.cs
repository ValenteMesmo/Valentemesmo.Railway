using System;
using System.Threading.Tasks;

namespace ValenteMesmo.Results
{
    /// <summary>
    /// Async Result extension methods
    /// </summary>
    public static class ResultExtensions
    {
        public async static Task<Result<TResult>> Pipe<T, TResult>(
            this Task<Result<T>> asyncResult
            , Func<T, TResult> onSuccess
        )
        {
            return (await asyncResult).Pipe(onSuccess);
        }

        public async static Task<Result<TResult>> Pipe<T, TResult>(
            this Task<Result<T>> asyncResult
            , Func<T, Result<TResult>> onSuccess
        )
        {
            return (await asyncResult).Pipe(onSuccess);
        }

        public async static Task<Result<TResult>> Pipe<T, TResult>(
            this Task<Result<T>> asyncResult
            , Func<T, Task<TResult>> onSuccess
        )
        {
            var result = await asyncResult;

            if (result.isSuccessful)
                return new Result<TResult>(await onSuccess(result.value));
            else
                return new Result<TResult>(result.ex);
        }

        public async static Task<Result<TResult>> Pipe<T, TResult>(
            this Task<Result<T>> asyncResult
            , Func<T, Task<Result<TResult>>> onSuccess
        )
        {
            var result = await asyncResult;
            return await result.Pipe(onSuccess);
        }

        public async static Task<TResult> Match<T, TResult>(
            this Task<Result<T>> asyncResult
            , Func<T, TResult> onSuccess
            , Func<Exception, TResult> onFailure
        )
        {
            var result = await asyncResult;
            return result.Match(onSuccess, onFailure);
        }

        public async static Task<TResult> Match<T, TResult>(
            this Task<Result<T>> asyncResult
            , Func<T, Task<TResult>> onSuccess
            , Func<Exception, TResult> onFailure
        )
        {
            var result = await asyncResult;
            if (result.isSuccessful)
                return await onSuccess(result.value);
            else
                return onFailure(result.ex);
        }

        public async static Task<TResult> Match<T, TResult>(
            this Task<Result<T>> asyncResult
            , Func<T, TResult> onSuccess
            , Func<Exception, Task<TResult>> onFailure
        )
        {
            var result = await asyncResult;
            if (result.isSuccessful)
                return onSuccess(result.value);
            else
                return await onFailure(result.ex);
        }

        public async static Task<TResult> Match<T, TResult>(
           this Task<Result<T>> asyncResult
           , Func<T, Task<TResult>> onSuccess
           , Func<Exception, Task<TResult>> onFailure
        )
        {
            var result = await asyncResult;
            if (result.isSuccessful)
                return await onSuccess(result.value);
            else
                return await onFailure(result.ex);
        }
    }
}