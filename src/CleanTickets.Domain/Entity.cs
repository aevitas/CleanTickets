using System.ComponentModel.DataAnnotations;

namespace CleanTickets.Domain;

public abstract class Entity
{
    [Key]
    public long Id { get; private protected set; }
}
