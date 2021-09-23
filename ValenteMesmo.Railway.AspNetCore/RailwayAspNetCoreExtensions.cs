using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ValenteMesmo
{
    public static class RailwayAspNetCoreExtensions
    {
        public static IActionResult Ok<T>(this Railway<T> value) =>
            HandleWithSuccessCode(200, value);

        public static async Task<IActionResult> Ok<T>(this Task<Railway<T>> value) =>
            HandleWithSuccessCode(200, await value);

        public static IActionResult Created<T>(this Railway<T> value) =>
            HandleWithSuccessCode(201, value);

        public static async Task<IActionResult> Created<T>(this Task<Railway<T>> value) =>
            HandleWithSuccessCode(201, await value);

        public static IActionResult NoContent<T>(this Railway<T> value) =>
            HandleWithSuccessCode(204, value);

        public static async Task<IActionResult> NoContent<T>(this Task<Railway<T>> value) =>
            HandleWithSuccessCode(204, await value);

        private static IActionResult HandleWithSuccessCode<T>(int code, Railway<T> value) =>
            value.Handle(
                success => new ObjectResult(success) { StatusCode = code }
                , ex => HandleError(ex)
            );

        private static IActionResult HandleError(Exception ex)
        {
            if (ex is RailwayException data)
                return new ObjectResult(data.Content)
                {
                    StatusCode = data.Code
                };

            return new ObjectResult(ex.ToString())
            {
                StatusCode = 500
            };
        }
    }
}

