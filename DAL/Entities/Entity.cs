using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public abstract class Entity<T> where T : class
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public abstract void CopyTo(T entityForChange);

        public abstract bool IsEmpty();
    }
}
