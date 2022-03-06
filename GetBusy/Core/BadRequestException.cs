namespace GetBusy.Core
{
    /// <summary>
    /// Represents errors that are caused by an invalid request.
    /// </summary>
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException()
        {
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
