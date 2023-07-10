using Application.Common.Mappings;
using Domain.Entities;

namespace Application.FeeManagement.Dto;

public class FeeDto : IMapFrom<FeeParameter>
{
    public string Id { get; set; }
    public long Minimum { get; set; }
    public long Maximum { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
