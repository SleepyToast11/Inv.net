using Shared.Domain.Tags.Repositories;
using Shared.Persistence.Repositories.Common.Interfaces;

namespace TagManagementService.Infrastructure.UnitOfWork;

public interface ITagUnitOfWork: IUnitOfWork
{
    public ITagRepository Tags { get; }
    public ISuperTagRepository SuperTags { get; } 
}