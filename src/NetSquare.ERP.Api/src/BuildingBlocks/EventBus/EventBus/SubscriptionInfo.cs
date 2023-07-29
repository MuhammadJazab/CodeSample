//-----------------------------------------------------------------------
// <copyright file="SubscriptionInfo.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBus;

/// <summary>
/// Defines the <see cref="InMemoryEventBusSubscriptionsManager" />.
/// </summary>
public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
{
    /// <summary>
    /// Defines the <see cref="SubscriptionInfo" />.
    /// </summary>
    public class SubscriptionInfo
    {
        /// <summary>
        /// Gets a value indicating whether IsDynamic.
        /// </summary>
        public bool IsDynamic { get; }

        /// <summary>
        /// Gets the HandlerType.
        /// </summary>
        public Type HandlerType { get; }

        /// <summary>
        /// Prevents a default instance of the <see cref="SubscriptionInfo"/> class from being created.
        /// </summary>
        /// <param name="isDynamic">The isDynamic<see cref="bool"/>.</param>
        /// <param name="handlerType">The handlerType<see cref="Type"/>.</param>
        private SubscriptionInfo(bool isDynamic, Type handlerType)
        {
            IsDynamic = isDynamic;
            HandlerType = handlerType;
        }

        /// <summary>
        /// The Dynamic.
        /// </summary>
        /// <param name="handlerType">The handlerType<see cref="Type"/>.</param>
        /// <returns>The <see cref="SubscriptionInfo"/>.</returns>
        public static SubscriptionInfo Dynamic(Type handlerType)
        {
            return new SubscriptionInfo(true, handlerType);
        }

        /// <summary>
        /// The Typed.
        /// </summary>
        /// <param name="handlerType">The handlerType<see cref="Type"/>.</param>
        /// <returns>The <see cref="SubscriptionInfo"/>.</returns>
        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(false, handlerType);
        }
    }
}