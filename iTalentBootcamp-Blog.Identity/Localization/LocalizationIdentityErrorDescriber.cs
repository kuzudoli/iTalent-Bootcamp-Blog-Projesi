using Microsoft.AspNetCore.Identity;

namespace iTalentBootcamp_Blog.Identity.Localization
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "DuplicateEmail",
                Description = $"'{email}' email adresi başka bir kullanıcı tarafından kullanılmaktadır."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $"'{userName}' kullanıcı adı başka bir kullanıcı tarafından alınmıştır."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Şifre en az 6 karakter olmalıdır."
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresDigit",
                Description = $"Şifre en az 1 adet rakam içermelidir."
            };
        }
    }
}
