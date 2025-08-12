using ItemManagementService.Application.Commands.Items;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Item;

namespace ItemManagementService.Application.Handlers.Items;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Guid>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public CreateItemCommandHandler(IItemUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var item = ItemFactory.Create(request.Name, request.TenantId);

        await _unitOfWork.Items.AddAsync(item.ToEntity(), cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}
