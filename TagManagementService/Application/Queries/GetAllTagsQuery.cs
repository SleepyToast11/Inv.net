using MediatR;
using TagManagementService.Infrastructure.Dto;

namespace TagManagementService.Application.Queries;

public record GetAllTagsQuery(): IRequest<IReadOnlyList<TagDto>>;