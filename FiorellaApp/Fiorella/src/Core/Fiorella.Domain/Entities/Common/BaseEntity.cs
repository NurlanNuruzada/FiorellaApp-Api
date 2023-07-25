using System.ComponentModel.DataAnnotations;

namespace Fiorella.Domain.Entities.Common;

public abstract class BaseEntity
{
    [Key]
    public Guid GuId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public virtual bool IsDeleted { get; set; }
}
