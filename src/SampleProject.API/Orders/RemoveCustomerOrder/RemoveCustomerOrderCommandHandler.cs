﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.API.Orders.RemoveCustomerOrder
{
    public class RemoveCustomerOrderCommandHandler : IRequestHandler<RemoveCustomerOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public RemoveCustomerOrderCommandHandler(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(RemoveCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(request.CustomerId);

            customer.RemoveOrder(request.OrderId);

            await this._customerRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}