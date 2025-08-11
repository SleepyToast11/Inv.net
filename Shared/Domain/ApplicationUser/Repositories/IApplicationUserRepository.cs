using Shared.Domain.Common.Interfaces;
using Shared.Persistence.Entities.ApplicationUser;

namespace Shared.Domain.ApplicationUser.Repositories;

public interface IApplicationUserRepository : IGenericRepository<ApplicationUserEntity>;