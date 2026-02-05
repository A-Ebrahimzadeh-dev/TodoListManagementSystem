using System.ComponentModel;

namespace TodoListManagementSystem.Shared.Exceptions
{
    /// <summary>
    /// Standard HTTP error types for handling exceptions in the system.
    /// </summary>
    public enum ErrorType
    {
        // ================== Client Errors (4xx) ==================

        /// <summary>
        /// The server cannot or will not process the request due to something that is perceived to be a client error.
        /// </summary>
        [Description("Bad Request")]
        BadRequest = 400,

        /// <summary>
        /// The client must authenticate itself to get the requested response.
        /// </summary>
        [Description("Unauthorized")]
        Unauthorized = 401,

        /// <summary>
        /// The client does not have access rights to the content.
        /// </summary>
        [Description("Forbidden")]
        Forbidden = 403,

        /// <summary>
        /// The server can not find the requested resource.
        /// </summary>
        [Description("Not Found")]
        NotFound = 404,

        /// <summary>
        /// The request method is known by the server but is not supported by the target resource.
        /// </summary>
        [Description("Method Not Allowed")]
        MethodNotAllowed = 405,

        /// <summary>
        /// The request conflicts with the current state of the target resource (e.g., duplicate entry).
        /// </summary>
        [Description("Conflict")]
        Conflict = 409,

        /// <summary>
        /// The server understands the content type of the request entity, but was unable to process the contained instructions.
        /// </summary>
        [Description("Unprocessable Entity")]
        UnprocessableEntity = 422,

        /// <summary>
        /// The user has sent too many requests in a given amount of time.
        /// </summary>
        [Description("Too Many Requests")]
        TooManyRequests = 429,

        // ================== Server Errors (5xx) ==================

        /// <summary>
        /// The server has encountered a situation it doesn't know how to handle.
        /// </summary>
        [Description("Internal Server Error")]
        InternalServerError = 500,

        /// <summary>
        /// The request method is not supported by the server and cannot be handled.
        /// </summary>
        [Description("Not Implemented")]
        NotImplemented = 501,

        /// <summary>
        /// The server is not ready to handle the request (e.g., due to maintenance or overload).
        /// </summary>
        [Description("Service Unavailable")]
        ServiceUnavailable = 503,
    }
}
