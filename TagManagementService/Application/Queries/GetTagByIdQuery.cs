using MediatR;
using TagManagementService.Infrastructure.Dto;

namespace TagManagementService.Application.Queries;

public record GetTagByIdQuery(Guid id): IRequest<TagDto>;