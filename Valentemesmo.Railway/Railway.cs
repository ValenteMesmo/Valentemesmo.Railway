using System;
using System.Threading.Tasks;

namespace ValenteMesmo.Railway
{

    /// <summary>
    /// Either a Success or a <see cref="Exception"/>.<br/>
    /// An implementation of <see href="https://www.google.com/search?q=railway+error+handling">Railway error handling</see>
    /// </summary>
    /// <typeparam name="Success">Type of expected value when nothing goes wrong</typeparam>
    public struct Railway<Success>
    {
        private readonly Success success;
        private readonly Exception failure;
        private readonly bool isSuccess;

        /// <summary>
        /// Constructor for Success.<br/>
        /// You probably won't use it...<br/>
        /// The implicit operator will do the trick.<br/>
        /// </summary>
        /// <param name="success">Type of expected value when nothing goes wrong</param>
        public Railway(Success success)
        {
            this.success = success;
            isSuccess = true;
            failure = null;
        }

        /// <summary>
        /// Constructor for Failure.<br/>
        /// You probably won't use it. The implicit operator will do the trick.<br/>
        /// </summary>
        /// <param name="failure">I recomend using custom Exception types</param>
        public Railway(Exception failure)
        {
            this.failure = failure;
            isSuccess = false;
            success = default;
        }

        /// <summary>
        /// This method ends the Railway.
        /// </summary>
        /// <typeparam name="Target">Type that both handlers need to return</typeparam>
        /// <param name="success">Handler for the happy path</param>
        /// <param name="failure">Handler for the sad path</param>
        /// <returns>Both handlers need to return the same type</returns>
        public Target Handle<Target>(
            Func<Success, Target> success
            , Func<Exception, Target> failure)
                => isSuccess
                    ? success(this.success)
                    : failure(this.failure);

        /// <summary>
        /// This method ends the Railway with async Success
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <returns></returns>
        public async Task<Target> Handle<Target>(
            Func<Success, Task<Target>> success
            , Func<Exception, Target> failure)
                => isSuccess
                    ? await success(this.success)
                    : failure(this.failure);

        /// <summary>
        /// This method ends the Railway with async Failure
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <returns></returns>
        public async Task<Target> Handle<Target>(
            Func<Success, Target> success
            , Func<Exception, Task<Target>> failure)
                => isSuccess
                    ? success(this.success)
                    : await failure(this.failure);

        /// <summary>
        /// Connects two Railways<br/>
        /// </summary>
        /// <returns>New Railway without changing Success type</returns>
        public Railway<Success> Join(
            Func<Success, Railway<Success>> success)
                => isSuccess
                    ? success(this.success)
                    : this;

        /// <summary>
        /// Connects two Railways<br/>
        /// </summary>
        /// <typeparam name="Target">Type of resulting Railway</typeparam>
        /// <returns>New Railway, but changing Success type to Target</returns>
        public Railway<Target> Join<Target>(
            Func<Success, Railway<Target>> success)
                => isSuccess
                    ? success(this.success)
                    : new Railway<Target>(failure);

        /// <summary>
        /// Connects two Railways using async handler<br/>
        /// </summary>
        /// <typeparam name="Target">Type of resulting Railway</typeparam>
        /// <returns>New Railway, but changing Success type to Target</returns>
        public async Task<Railway<Target>> Join<Target>(
            Func<Success, Task<Railway<Target>>> success)
                => isSuccess
                    ? await success(this.success)
                    : new Railway<Target>(failure);

        /// <summary>
        /// Implicit convert Success to Railway
        /// </summary>
        public static implicit operator Railway<Success>(Success success)
            => new Railway<Success>(success);

        /// <summary>
        /// Implicit convert Exception to Railway
        /// </summary>
        public static implicit operator Railway<Success>(Exception failure)
            => new Railway<Success>(failure);

        /// <summary>
        /// Explicit convert Success to Railway
        /// </summary>
        public static explicit operator Success(Railway<Success> either)
            => either.success;

        /// <summary>
        /// Explicit convert Exception to Railway
        /// </summary>
        public static explicit operator Exception(Railway<Success> either)
            => either.failure;

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return isSuccess
                ? $"Success: {success}"
                : $"Failure: {failure}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return isSuccess
                ? success.Equals(obj)
                : failure.Equals(obj);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override int GetHashCode()
        {
            return isSuccess
               ? success.GetHashCode()
               : failure.GetHashCode();
        }
    }
}