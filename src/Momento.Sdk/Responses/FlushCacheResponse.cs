﻿namespace Momento.Sdk.Responses;

using Momento.Sdk.Exceptions;

/// <summary>
/// Parent response type for a flush cache request. The
/// response object is resolved to a type-safe object of one of
/// the following subtypes:
/// <list type="bullet">
/// <item><description>FlushCacheResponse.Success</description></item>
/// <item><description>FlushCacheResponse.Error</description></item>
/// </list>
/// Pattern matching can be used to operate on the appropriate subtype.
/// For example:
/// <code>
/// if (response is FlushCacheResponse.Error errorResponse)
/// {
///     // handle error as appropriate
/// }
/// </code>
/// </summary>
public abstract class FlushCacheResponse
{
    /// <include file="../docs.xml" path='docs/class[@name="Success"]/description/*' />
    public class Success : FlushCacheResponse { }

    /// <include file="../docs.xml" path='docs/class[@name="Error"]/description/*' />
    public class Error : FlushCacheResponse
    {
        private readonly SdkException _error;

        /// <include file="../docs.xml" path='docs/class[@name="Error"]/constructor/*' />
        public Error(SdkException error)
        {
            _error = error;
        }

        /// <include file="../docs.xml" path='docs/class[@name="Error"]/prop[@name="InnerException"]/*' />
        public SdkException InnerException
        {
            get => _error;
        }

        /// <include file="../docs.xml" path='docs/class[@name="Error"]/prop[@name="ErrorCode"]/*' />
        public MomentoErrorCode ErrorCode
        {
            get => _error.ErrorCode;
        }

        /// <include file="../docs.xml" path='docs/class[@name="Error"]/prop[@name="Message"]/*' />
        public string Message
        {
            get => $"{_error.MessageWrapper}: {_error.Message}";
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{base.ToString()}: {this.Message}";
        }

    }
}
