//file="ResponseMessages.cs" >

namespace Order.Api.Application.Constants;

/// <summary>
/// Defines the <see cref="ResponseMessages" />.
/// </summary>
public static class ResponseMessages
{
    /// <summary>
    /// The user created successfully
    /// </summary>
    public static readonly Message OrderCreatedSuccessfully = new() { Code = "7000", Title = "Order Created successfully", Text = "User updated successfully.", MessageDisplayType = nameof(MessageDisplayTypes.All), MessageIndicatorType = nameof(MessageIndicatorTypes.Information) };
}
