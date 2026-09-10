using scms.Shared.Dtos;

namespace scms.Application.Dtos.Tenant;

public class MasterValuesDto : BaseDto
{
    public string Type { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Description { get; set; }
}
