﻿using CustomerAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Service.v1.Command
{
    public class CreateCustomerCommand: IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}
