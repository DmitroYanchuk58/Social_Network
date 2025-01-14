using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;


namespace DAL.Entities
{
    public class User : Entity <User>
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

        public override void CopyTo(User entityForChange)
        {
            if (entityForChange.Nickname != null)
            {
                Nickname = entityForChange.Nickname;
            }
            if (entityForChange.Password != null)
            {
                Password = entityForChange.Password;
            }
            if (entityForChange.Email != null)
            {
                Email = entityForChange.Email;
            }
        }

        public override bool IsEmpty()
        {
            if(Nickname.IsNullOrEmpty() && Password.IsNullOrEmpty() && Email.IsNullOrEmpty())
            {
                return true;
            }
            return false;
        }
    }
}
