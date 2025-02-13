namespace BAL.DTOs
{
    public class User : IEntity
    {
        private string? nickname;

        private string? password;

        private string? email;

        private Guid id;

        public string Nickname
        {
            get
            {
                return nickname ?? string.Empty;
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
                return password ?? string.Empty;
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
                return email ?? string.Empty;
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

        public Guid Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
    }
}
