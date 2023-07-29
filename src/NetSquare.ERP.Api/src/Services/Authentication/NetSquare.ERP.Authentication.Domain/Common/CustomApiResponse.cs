//-----------------------------------------------------------------------
// <copyright file="CustomApiResponse.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.Common;

/// <summary>
/// Defines the <see cref="CustomApiResponse" />.
/// </summary>
public class CustomApiResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomApiResponse"/> class.
    /// </summary>
    public CustomApiResponse()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomApiResponse"/> class.
    /// </summary>
    /// <param name="sentDate">The sentDate<see cref="DateTime"/>.</param>
    /// <param name="payload">The payload<see cref="object"/>.</param>
    /// <param name="message">The message<see cref="string"/>.</param>
    /// <param name="statusCode">The statusCode<see cref="HttpStatusCode"/>.</param>
    public CustomApiResponse(DateTime sentDate, object payload, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        this.HttpCode = (int)statusCode;
        this.HttpMessage = message;
        this.Payload = payload;
        this.SentDate = sentDate;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomApiResponse"/> class.
    /// </summary>
    /// <param name="sentDate">The sentDate<see cref="DateTime"/>.</param>
    /// <param name="payload">The payload<see cref="object?"/>.</param>
    public CustomApiResponse(DateTime sentDate, object? payload = null)
    {
        this.HttpCode = (int)System.Net.HttpStatusCode.OK;
        this.HttpMessage = "Success";
        this.Payload = payload;
        this.SentDate = sentDate;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomApiResponse"/> class.
    /// </summary>
    /// <param name="payload">The payload<see cref="object"/>.</param>
    public CustomApiResponse(object payload)
    {
        this.HttpCode = (int)System.Net.HttpStatusCode.OK;
        this.Payload = payload;
    }

    /// <summary>
    /// Gets or sets the HttpCode.
    /// </summary>
    public int HttpCode { get; set; }

    /// <summary>
    /// Gets or sets the HttpMessage.
    /// </summary>
    public string? HttpMessage { get; set; }

    /// <summary>
    /// Gets or sets the Payload.
    /// </summary>
    public object? Payload { get; set; }

    /// <summary>
    /// Gets or sets the SentDate.
    /// </summary>
    public DateTime SentDate { get; set; }
}