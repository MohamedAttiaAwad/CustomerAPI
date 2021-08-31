using CustomerAPI.Domain.Entities;

namespace CustomerAPI.Messaging.Send.Sender.v1
{
    public interface ICustomerUpdateSender
    {
        void SendCustomer(Customer customer);
    }
}
