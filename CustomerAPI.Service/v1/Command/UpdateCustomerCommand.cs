using CustomerAPI.Domain.Entities;
using MediatR;

namespace CustomerAPI.Service.v1.Command
{
    public class UpdateCustomerCommand :IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}
