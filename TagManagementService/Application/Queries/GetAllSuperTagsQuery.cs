using MediatR;
using Shared.Domain.Tags;
using TagManagementService.Infrastructure.Dto;

namespace TagManagementService.Application.Queries;

public record GetAllSuperTagsQuery():  IRequest<IReadOnlyList<SuperTagDto>>;