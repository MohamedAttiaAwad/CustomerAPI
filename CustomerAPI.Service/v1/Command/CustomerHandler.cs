using CustomerAPI.Data.Repository.v1;
using CustomerAPI.Domain.Entities;
using CustomerAPI.Messaging.Send.Sender.v1;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAPI.Service.v1.Command
{
    public class CustomerHandler
        : IRequestHandler<CreateCustomerCommand, Customer>
        , IRequestHandler<UpdateCustomerCommand,Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerUpdateSender _customerUpdateSender;

        public CustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _customerRepository.AddAsync(request.Customer);
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer =  await _customerRepository.AddAsync(request.Customer);
            _customerUpdateSender.SendCustomer(customer);
            return customer;
        }
    }
}
