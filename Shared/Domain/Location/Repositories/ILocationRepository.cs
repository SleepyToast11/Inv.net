using Shared.Domain.Common.Interfaces;
using Shared.Persistence.Entities.Location;

namespace Shared.Domain.Location.Repositories;

public interface ILocationRepository : IGenericRepository<LocationEntity>
{
}