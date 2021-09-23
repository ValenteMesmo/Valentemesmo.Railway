using System;
using System.Collections.Generic;
using System.Text;

namespace ValenteMesmo
{
    /// <summary>
    /// Represents errors that occur during application execution
    /// </summary>
    public class RailwayException : Exception
    {
        /// <summary>
        /// Error detail
        /// </summary>
        public readonly object Content;
        /// <summary>
        /// Http Error Code
        /// </summary>
        public readonly int Code;

        /// <summary>
        /// </summary>
        public RailwayException(int Code, object Content) : base("Check Content attribute for more info")
        {
            this.Content = Content;
            this.Code = Code;
        }
    }

    /// <summary>
    /// HTTP status 400
    /// </summary>
    public class BadRequestException : RailwayException
    {
        /// <summary>        
        /// </summary>
        public BadRequestException() : this(string.Empty) { }

        /// <summary>        
        /// </summary>
        public BadRequestException(object Content) : base(400, Content) { }
    }

    /// <summary>
    /// HTTP status 401
    /// </summary>
    public class UnauthorizedException : RailwayException
    {
        /// <summary>
        /// </summary>
        public UnauthorizedException() : this(string.Empty) { }

        /// <summary>
        /// </summary>
        public UnauthorizedException(object Content) : base(401, Content) { }
    }

    /// <summary>
    /// HTTP status 403
    /// </summary>
    public class ForbiddenException : RailwayException
    {
        /// <summary>
        /// </summary>
        public ForbiddenException() : this(string.Empty) { }

        /// <summary>
        /// </summary>
        public ForbiddenException(object Content) : base(403, Content) { }
    }

    /// <summary>
    /// HTTP status 404
    /// </summary>
    public class NotFoundException : RailwayException
    {
        /// <summary>
        /// </summary>
        public NotFoundException() : this(string.Empty) { }

        /// <summary>
        /// </summary>
        public NotFoundException(object Content) : base(404, Content) { }
    }

    /// <summary>
    /// HTTP status 405
    /// </summary>
    public class MethodNotAllowedException : RailwayException
    {
        /// <summary>
        /// </summary>
        public MethodNotAllowedException() : this(string.Empty) { }

        /// <summary>
        /// </summary>
        public MethodNotAllowedException(object Content) : base(405, Content) { }
    }

    /// <summary>
    /// HTTP status 409
    /// </summary>
    public class ConflictException : RailwayException
    {
        /// <summary>
        /// </summary>
        public ConflictException() : this(string.Empty) { }

        /// <summary>
        /// </summary>
        public ConflictException(object Content) : base(409, Content) { }
    }

    /// <summary>
    /// HTTP status 500
    /// </summary>
    public class InternalServerErrorException : RailwayException
    {
        /// <summary>
        /// </summary>
        public InternalServerErrorException() : this(string.Empty) { }

        /// <summary>
        /// </summary>
        public InternalServerErrorException(object Content) : base(500, Content) { }
    }

    /// <summary>
    /// HTTP status 503
    /// </summary>
    public class ServiceUnavailableException : RailwayException
    {
        /// <summary>
        /// </summary>
        public ServiceUnavailableException() : this(string.Empty) { }

        /// <summary>
        /// </summary>
        public ServiceUnavailableException(object Content) : base(503, Content) { }
    }
}
