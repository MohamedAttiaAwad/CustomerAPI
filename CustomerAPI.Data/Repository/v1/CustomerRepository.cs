using CustomerAPI.Data.Database;
using CustomerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAPI.Data.Repository.v1
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext customerContext): base(customerContext)
        {

        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _customerContext.Customer.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
