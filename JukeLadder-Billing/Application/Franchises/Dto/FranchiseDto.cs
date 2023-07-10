using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Franchises.Dto;

public class FranchiseDto : IMapFrom<Franchise>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string Theme { get; set; }
}
