using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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


        public static bool DownloadImage(this string imageUrl, out string virualLocation) 
        {
            var toreturn = false;
            try
            {

                string fileName = Path.GetFileName(imageUrl);
                var temp = HttpContext.Current.Server.MapPath("~/pinuploads/");
                var dropLocation = temp;
                var saveLocation = Path.Combine(dropLocation, fileName);

                byte[] imageBytes;
                HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
                WebResponse imageResponse = imageRequest.GetResponse();
                Stream responseStream = imageResponse.GetResponseStream();

                using (BinaryReader br = new BinaryReader(responseStream))
                {
                    imageBytes = br.ReadBytes(500000);
                    br.Close();
                }
                responseStream.Close();
                imageResponse.Close();

                FileStream fs = new FileStream(saveLocation, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                try
                {
                    bw.Write(imageBytes);
                    toreturn = true;
                }
                finally
                {
                    fs.Close();
                    bw.Close();
                }
                virualLocation = @"/pinuploads/" + fileName;
                //     /pinuploads/[filename]

                return toreturn;
            }
            catch (Exception)
            {
                

            }
            virualLocation = "";
            return toreturn;
        }
    }
}