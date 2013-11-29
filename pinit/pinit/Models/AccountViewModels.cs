using System.ComponentModel.DataAnnotations;
using pinit.Data;
using System.Linq;

namespace pinit.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public bool CustomChangePassword(string username, string password , string oldPassword)
        {

            using (var db = new PinitEntities())
            {
                var userinfo = db.UserInfoes.FirstOrDefault(x => x.UserName == username && x.Password == oldPassword);
                if (userinfo == null)
                {
                    return false;
                }
                userinfo.Password = password;
                db.SaveChanges();
                return true;
            }


        }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public ApplicationUser CustomLogin(string username, string password)
        {
            var user = new ApplicationUser();
            using (var db = new PinitEntities())
            {
                var userinfo = db.UserInfoes.FirstOrDefault(x => x.UserName == username && x.Password == password);
                if (userinfo == null)
                {
                    return null;
                }
                user = new ApplicationUser
                {
                    UserName = userinfo.UserName,
                    Id = userinfo.UserName
                };
            }


            return user;
        }


        public bool FormAuthentication(string username, string password)
        {

            using (var db = new PinitEntities())
            {
                var userinfo = db.UserInfoes.FirstOrDefault(x => x.UserName == username && x.Password == password);
                if (userinfo == null)
                {
                    return false;
                }
                return true;
            }


        }

    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public bool CreateAccount(out string msg)
        {
            msg = "account was not created";
            var toreturn = false;
            using (var db = new PinitEntities())
            {
                var result = db.FI_SignUp(UserName, Password, FirstName, LastName, Email);
                var res = result.FirstOrDefault();
                if (res == null)
                {
                    return toreturn;
                }
                if (res.Success ?? false)
                {
                    msg = "account was created";
                    toreturn = true;
                }


            }
            return toreturn;

        }
    }
}
