using Fiorella.Domain.Entities.Common;

namespace Fiorella.Domain.Entities;

public class Category : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
