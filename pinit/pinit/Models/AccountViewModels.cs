using System.ComponentModel.DataAnnotations;
using pinit.Data;
using System.Linq;
using pinit.Helpers;


namespace pinit.Models
{
    
    /// <summary>
    /// this is to manage change password feature
    /// 
    /// </summary>
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




    /// <summary>
    /// this is to manage login 
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

  
        /// <summary>
        /// provide form authentication from mvc 3 custom to fit simple tables
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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


    /// <summary>
    /// takes in username password first last name and email 
    /// register account
    /// </summary>
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
            if (!Email.ValidateEmail())
            {
                msg = "Email address not correct";
                return false;
            }
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
                else 
                {
                    if (!string.IsNullOrWhiteSpace(res.msg)) 
                    {
                        msg = res.msg;
                    }
                }


            }
            return toreturn;

        }
    }


    /// <summary>
    /// update profile model
    /// for firstname lastname and email
    /// </summary>
    public class UpdateUserinfoViewModel
    {
        
        public string UserName { get; set; }
       
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


        public bool FillInfo() 
        {
           
            using (var db = new PinitEntities())
            {
                var userinfo = db.UserInfoes.FirstOrDefault(x => x.UserName == UserName);
                if (userinfo == null)
                {
                    return false;
                }
                FirstName = userinfo.FirstName;
                LastName = userinfo.LastName;
                Email = userinfo.Email;
                
                return true;
            }
        }

        public bool UpdateUserInfo(out string msg)
        {
            msg = "account was not update";
            var toreturn = false;
            if (!Email.ValidateEmail()) {
                msg = "Email address not correct";
                return false;
            }
            using (var db = new PinitEntities())
            {
                var result = db.FI_UpdateProfile(UserName, FirstName, LastName, Email);
                var res = result.FirstOrDefault();
                if (res == null)
                {
                    return toreturn;
                }
                if (res.Success ?? false)
                {
                    msg = "Profile was updated";
                    toreturn = true;
                }
            }
            return toreturn;

        }
    }

}
