using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Configuration;

namespace pinit.Helpers
{
    public static class CustomExtention
    {

        public static bool ValidExtention(this string source)
        {
            return (source.EndsWith(".png") || source.EndsWith(".jpg") || source.EndsWith(".jpeg") || source.EndsWith(".gif"));
        }
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

            //VirualLocatoin
            virualLocation  = ConfigurationManager.AppSettings.Get("VirualLocatoin");
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
                    imageBytes = br.ReadBytes(2000000);
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
                virualLocation = virualLocation+ fileName;
                //     /pinuploads/[filename]

                return toreturn;
            }
            catch (Exception)
            {
                

            }
            virualLocation = "";
            return toreturn;
        }



        public static bool DownloadImage(this HttpPostedFileBase file, out string virualLocation)
        {

            //VirualLocatoin
            virualLocation = ConfigurationManager.AppSettings.Get("VirualLocatoin");
            var toreturn = false;
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                //var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                
                
                var temp = HttpContext.Current.Server.MapPath("~/pinuploads/");
                var dropLocation = temp;
                var saveLocation = Path.Combine(dropLocation, fileName);
                file.SaveAs(saveLocation);
                
                virualLocation = virualLocation + fileName;
                //     /pinuploads/[filename]

                return true;
            }
            catch (Exception)
            {


            }
            virualLocation = "";
            return toreturn;
        }
    }
}