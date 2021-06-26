using System;
using System.Threading.Tasks;

namespace ValenteMesmo.Railway
{

    /// <summary>
    /// Async Railway extension methods
    /// </summary>
    public static class RailwayExtensions
    {
        /// <summary>
        /// Connects from async Railway.
        /// </summary>
        /// <returns>New Railway without changing Success type</returns>
        public static async Task<Railway<Target>> Join<Success, Target>(
            this Task<Railway<Success>> result
            , Func<Success, Railway<Target>> success)
              => (await result).Join(success);

        /// <summary>
        /// Connects two async Railways.
        /// </summary>
        /// <returns>New Railway without changing Success type</returns>
        public static async Task<Railway<Target>> Join<Success, Target>(
            this Task<Railway<Success>> result
            , Func<Success, Task<Railway<Target>>> success)
              => await (await result).Join(success);

        /// <summary>
        /// This method ends an async Railway.       
        /// </summary>
        /// <typeparam name="Success"></typeparam>
        /// <typeparam name="Target">Type that both handlers need to return</typeparam>
        /// <param name="either"></param>
        /// <param name="success">Handler for the happy path</param>
        /// <param name="failure">Handler for the sad path</param>
        /// <returns>Both handlers need to return the same type</returns>
        public async static Task<Target> Handle<Success, Target>(
            this Task<Railway<Success>> either
            , Func<Success, Target> success
            , Func<Exception, Target> failure)
                => (await either).Handle(success, failure);

        /// <summary>
        /// This method ends an async Railway with async Success handler.
        /// </summary>
        /// <typeparam name="Success"></typeparam>
        /// <typeparam name="Target"></typeparam>
        /// <param name="either"></param>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <returns></returns>
        public async static Task<Target> Handle<Success, Target>(
            this Task<Railway<Success>> either
            , Func<Success, Task<Target>> success
            , Func<Exception, Target> failure)
                => await (await either).Handle(success, failure);

        /// <summary>
        /// This method ends an async Railway with async Failure handler.
        /// </summary>
        /// <typeparam name="Success"></typeparam>
        /// <typeparam name="Target"></typeparam>
        /// <param name="either"></param>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <returns></returns>
        public async static Task<Target> Handle<Success, Target>(
            this Task<Railway<Success>> either
            , Func<Success, Target> success
            , Func<Exception, Task<Target>> failure)
                => await (await either).Handle(success, failure);

        /// <summary>
        /// This method ends an async Railway with async handlers.
        /// </summary>
        /// <typeparam name="Success"></typeparam>
        /// <typeparam name="Target"></typeparam>
        /// <param name="either"></param>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <returns></returns>
        public async static Task<Target> Handle<Success, Target>(
            this Task<Railway<Success>> either
            , Func<Success, Task<Target>> success
            , Func<Exception, Task<Target>> failure)
                => await (await either).Handle(success, failure);
    }
}