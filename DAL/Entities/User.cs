using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;


namespace DAL.Entities
{
    public class User : Entity
    {
        #region Properties

        [Required]
        public string Nickname {  get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        #endregion 
    }
}
