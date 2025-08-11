namespace TagManagementService.Infrastructure.Dto;

public record TagDto(Guid Id, string Name, Guid SuperTagId);