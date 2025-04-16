using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MassTransit;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IBus _bus;

        public CancelSaleHandler(ISaleRepository saleRepository, IBus bus)
        {
            _saleRepository = saleRepository;
            _bus = bus;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var saleCanceled = await _saleRepository.CancelAsync(request.Id, cancellationToken);

            if(!saleCanceled)
                throw new KeyNotFoundException($"Sale with Id {request.Id} not found or already canceled.");

            await _bus.Publish(new SaleCancelledEvent { Id = request.Id }, cancellationToken);

            return new CancelSaleResult { Success = true };
        }
    }
}
