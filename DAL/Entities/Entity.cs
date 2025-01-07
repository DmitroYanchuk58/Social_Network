using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
