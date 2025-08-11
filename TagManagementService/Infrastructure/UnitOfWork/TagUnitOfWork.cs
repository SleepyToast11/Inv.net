using Shared.Domain.Tags.Repositories;
using Shared.Persistence;
using Shared.Persistence.Repositories.Common;

namespace TagManagementService.Infrastructure.UnitOfWork;

public class TagUnitOfWork(
    AppDbContext context, 
    ITagRepository tagRepository, 
    ISuperTagRepository superTagRepository) : BaseUnitOfWork(context), ITagUnitOfWork
{
    public ITagRepository Tags { get; } = tagRepository;
    public ISuperTagRepository SuperTags { get; } = superTagRepository;
}