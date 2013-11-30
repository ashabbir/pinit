using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pinit.Helpers
{
    public static class CustomExtention
    {
        public static bool ValidateEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}