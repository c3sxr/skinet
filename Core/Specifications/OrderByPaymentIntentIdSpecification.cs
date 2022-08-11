using Core.Entities.OrderAggregate;

namespace Core.Specifications;

public class OrderByPaymentIntentIdSpecification : BaseSpecifications<Order>
{
    public OrderByPaymentIntentIdSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
    {
    }
}