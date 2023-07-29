//-----------------------------------------------------------------------
// <copyright file="AppException.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Models.Exceptions;

/// <summary>
/// Defines the <see cref="AppException" />.
/// </summary>
[Serializable]
public class AppException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class.
    /// </summary>
    public AppException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class.
    /// </summary>
    /// <param name="messagesSummary">The messagesSummary<see cref="MessagesSummary" />.</param>
    public AppException(MessagesSummary messagesSummary)
    : base(
    JsonConvert.SerializeObject(messagesSummary, Formatting.Indented))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class.
    /// </summary>
    /// <param name="message">The message<see cref="string" />.</param>
    public AppException(string message)
    : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class.
    /// </summary>
    /// <param name="serializationInfo">The serializationInfo<see cref="SerializationInfo" />.</param>
    /// <param name="streamingContext">The streamingContext<see cref="StreamingContext" />.</param>
    protected AppException(SerializationInfo serializationInfo, StreamingContext streamingContext)
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