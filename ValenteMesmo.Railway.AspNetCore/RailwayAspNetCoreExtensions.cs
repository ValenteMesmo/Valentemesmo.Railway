using Microsoft.AspNetCore.Mvc;
using System;

namespace ValenteMesmo.Railway
{
    public static class RailwayAspNetCoreExtensions
    {
        public static IActionResult AsOkResult<T>(this Railway<T> value) =>
            HandleWithSuccessCode(200, value);

        public static IActionResult AsCreatedResult<T>(this Railway<T> value) =>
            HandleWithSuccessCode(201, value);

        public static IActionResult AsNoContent<T>(this Railway<T> value) =>
            HandleWithSuccessCode(204, value);

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

            return new ObjectResult("Unexpected error")
            {
                StatusCode = 500
            };
        }
    }
}

