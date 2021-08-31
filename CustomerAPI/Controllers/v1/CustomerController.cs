using AutoMapper;
using CustomerAPI.Domain.Entities;
using CustomerAPI.Models.v1;
using CustomerAPI.Service.v1.Command;
using CustomerAPI.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        /// <summary>
        /// Action to create a new customer in the database.  
        /// </summary>
        /// <param name="createCustomerModel">Model to create a new customer</param>
        /// <returns>Returns the created customer</returns>
        /// <response code="200">Returned if the customer was created</response>  
        /// <response code="400">Returned if the model couldn&#8217;t be parsed or the customer couldn&#8217;t be saved</response>  
        /// <response code="422">Returned when the validation failed</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Customer>> Customer(CreateCustomerModel createCustomerModel)
        {
            try
            {
                return await _mediator.Send(new CreateCustomerCommand
                {
                    Customer = _mapper.Map<Customer>(createCustomerModel)
                }) ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult<Customer>> Customer(UpdateCustomerModel updateCustomerModel)
        {
            try
            {
                var customer = await _mediator.Send(new GetCustomerByIdQuery
                {
                    Id = updateCustomerModel.Id
                });

                if (customer == null)
                {
                    return BadRequest($"No customer found with the id {updateCustomerModel.Id}");
                }

                return await _mediator.Send(new UpdateCustomerCommand
                {
                    Customer = _mapper.Map(updateCustomerModel, customer)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
