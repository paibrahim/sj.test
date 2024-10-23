using System.Buffers.Text;
using System.Text.RegularExpressions;

namespace Domain.Aggregate
{
    public class User
    {
        public Guid? Id { get; set; }
        public string? Company {  get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }

        public static User Create(string name, string email, string password, string salt, string company)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2)
            { 
                throw new ArgumentException(nameof(name));
            }

            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException(nameof(email));
            }

            if (string.IsNullOrEmpty(password) || !Base64.IsValid(password))
            {
                throw new ArgumentException(nameof(password));
            }

            if (string.IsNullOrEmpty(salt) || !Base64.IsValid(salt))
            {
                throw new ArgumentException(nameof(salt));
            }

            if (string.IsNullOrEmpty(company))
            {
                throw new ArgumentException(nameof(company));
            }

            return new()
            {
                Name = name,
                Email = email.ToLower(),
                Password = password,
                Company = company.ToLower(),
                Salt = salt
            };
        }

        public void Update(string name, string email)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2)
            {
                throw new ArgumentException(nameof(name));
            }

            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException(nameof(email));
            }

            Email = email;
            Name = name;
        }
    }
}
