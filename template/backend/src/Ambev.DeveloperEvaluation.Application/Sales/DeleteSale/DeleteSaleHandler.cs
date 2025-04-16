using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MassTransit;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IBus _bus;

        public DeleteSaleHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _bus = bus;
        }

        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var saleDeleted = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
            if (!saleDeleted)
                throw new KeyNotFoundException($"Sale with id {request.Id} not found.");

            await _bus.Publish(new SaleDeletedEvent { Id = request.Id }, cancellationToken);

            return new DeleteSaleResult { Success = true };
        }


    }
}
