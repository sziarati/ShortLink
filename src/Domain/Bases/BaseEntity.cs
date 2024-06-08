
namespace Domain.Bases;

public abstract class BaseEntity
{
    public uint Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? EditDate { get; set; }
}