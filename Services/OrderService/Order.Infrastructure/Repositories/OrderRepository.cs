//file="OrderRepository.cs" >

namespace Order.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
{
    /// <summary>
    /// Initializes Current Context
    /// </summary>
    /// <param name="session"></param>
    public OrderRepository(IOrderDbContext session)
        : base(session)
    {

    }
}
