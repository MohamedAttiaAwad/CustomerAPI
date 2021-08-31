﻿using CustomerAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Service.v1.Query
{
    public class GetCustomerByIdQuery :IRequest<Customer>
    {
        public Guid Id { get; set; }
    }
}
