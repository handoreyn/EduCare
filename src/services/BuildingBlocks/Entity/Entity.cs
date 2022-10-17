using BuildingBlocks.Enums;

namespace BuildingBlocks.Entity;

public abstract class Entity
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public StatusEnumType Status { get; set; }
}