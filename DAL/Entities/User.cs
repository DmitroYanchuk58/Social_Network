using System.ComponentModel.DataAnnotations;


namespace DAL.Entities
{
    public class User : Entity
    {
        #region Properties

        [Required]
        public required string Nickname {  get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        #endregion 
    }
}
