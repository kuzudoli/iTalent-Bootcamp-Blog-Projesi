using iTalentBootcamp_Blog.Identity.Entity;
using Microsoft.AspNetCore.Identity;

namespace iTalentBootcamp_Blog.Identity.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            var errors = new List<IdentityError>();

            CheckUsername(user, password, errors);
            CheckSequentialNumbers(password, errors);

            if (errors.Any())
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));

            return Task.FromResult(IdentityResult.Success);
        }

        private void CheckUsername(AppUser user, string password, List<IdentityError> errors)
        {
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError()
                {
                    Code = "PasswordNotContainUsername",
                    Description = "Şifre kullanıcı adınızı içeremez!"
                });
            }
        }

        private void CheckSequentialNumbers(string password, List<IdentityError> errors)
        {
            var numbers = new List<int>();
            var maxSequential = 2;

            foreach (char item in password)
                if (char.IsNumber(item))
                    numbers.Add(item);

            if (numbers.Any())
            {
                for (int i = 0; i < numbers.Count - 1; i++)//123
                {
                    if (numbers[i] + 1 == numbers[i + 1])
                        maxSequential--;
                    if (maxSequential == 0)
                    {
                        errors.Add(new IdentityError()
                        {
                            Code = "PasswordNotContainSequentialNumber",
                            Description = "Şifre ardışık rakamlar içeremez!"
                        });
                        break;
                    }
                }
            }
        }

    }
}
