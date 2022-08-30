using System.ComponentModel.DataAnnotations;

namespace CleanTickets.Domain;

public abstract class Entity
{
    [Key]
    public long Id { get; internal set; }
}
