namespace BAL.DTOs
{
    public class User : Entity
    {
        private string? nickname;

        private string? password;

        private string? email;

        public string Nickname
        {
            get
            {
                return nickname;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                nickname = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                password = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                email = value;
            }
        }


    }
}
