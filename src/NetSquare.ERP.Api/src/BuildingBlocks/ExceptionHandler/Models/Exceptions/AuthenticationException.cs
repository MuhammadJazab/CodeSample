//-----------------------------------------------------------------------
// <copyright file="AuthenticationException.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Models.Exceptions;

/// <summary>
/// Defines the <see cref="AuthenticationException" />.
/// </summary>
[Serializable]
public class AuthenticationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationException"/> class.
    /// </summary>
    public AuthenticationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationException"/> class.
    /// </summary>
    /// <param name="message">The message<see cref="string" />.</param>
    public AuthenticationException(string message)
    : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationException"/> class.
    /// </summary>
    /// <param name="serializationInfo">The serializationInfo<see cref="SerializationInfo" />.</param>
    /// <param name="streamingContext">The streamingContext<see cref="StreamingContext" />.</param>
    protected AuthenticationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
    {
    }

    /// <summary>
    /// The GetObjectData.
    /// </summary>
    /// <param name="info">The info<see cref="SerializationInfo" />.</param>
    /// <param name="context">The context<see cref="StreamingContext" />.</param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}