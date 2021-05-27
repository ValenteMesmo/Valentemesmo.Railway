using System;
using System.Threading.Tasks;

namespace Valentemesmo
{
    /// <summary>
    /// Async Railway extension methods
    /// </summary>
    public static class RailwayExtensions
    {
        /// <summary>
        /// Connects from async Railway<br/>
        /// </summary>
        /// <returns>New Railway without changing Success type</returns>
        public static async Task<Railway<Target>> Join<Success, Target>(
            this Task<Railway<Success>> result
            , Func<Success, Railway<Target>> success)
              => (await result).Join(success);

        /// <summary>
        /// Connects two async Railways<br/>
        /// </summary>
        /// <returns>New Railway without changing Success type</returns>
        public static async Task<Railway<Target>> Join<Success, Target>(
            this Task<Railway<Success>> result
            , Func<Success, Task<Railway<Target>>> success)
              => await (await result).Join(success);

        /// <summary>
        /// This method ends an async Railway.<br/>        
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
    }
}
