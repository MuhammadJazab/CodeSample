//file="OrderRepository.cs" >

namespace Order.Infrastructure.Repositories;

/// <summary>
/// Initializes Current Context
/// </summary>
/// <param name="session"></param>
public class OrderRepository(IOrderDbContext session) : GenericRepository<OrderEntity>(session), IOrderRepository
{
}
