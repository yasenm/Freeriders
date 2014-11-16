namespace FreeRiders.Data
{
    using System.Linq;
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    using FreeRiders.Models;

    public static class ImgurUploader
    {
        private const string ClientId = "253ceccb0eb6e36";

        public static string UploadImageToImgur(byte[] image)
        {
            WebClient w = new WebClient();
            w.Headers.Add("Authorization", "Client-ID " + ClientId);

            NameValueCollection Keys = new NameValueCollection();
            try
            {
                Keys.Add("image", Convert.ToBase64String(image));

                byte[] responseArray = w.UploadValues("https://api.imgur.com/3/image", Keys);
                dynamic result = Encoding.ASCII.GetString(responseArray);

                Regex reg = new Regex("link\":\"(.*?)\"");
                Match match = reg.Match(result);

                string url = match.ToString().Replace("link\":\"", "").Replace("\"", "").Replace("\\/", "/");

                return url;
            }
            catch (Exception s)
            {
                return "error " + s.Message;
            }
        }

        public static Picture SavePictureInDb(HttpPostedFileBase inputPicture, IFreeRidersDbContext Data)
        {
            if (null != inputPicture)
            {
                HttpPostedFileBase file = inputPicture;
                int fileLen = file.ContentLength;
                if (fileLen > 0)
                {
                    using (var memory = new MemoryStream())
                    {
                        file.InputStream.CopyTo(memory);
                        var image = memory.GetBuffer();
                        var pictureUrl = ImgurUploader.UploadImageToImgur(image);

                        if (pictureUrl != "error")
                        {
                            var uploadedPicture = new Picture
                            {
                                ImageUrl = pictureUrl,
                            };

                            Data.Pictures.Add(uploadedPicture);
                            Data.SaveChanges();

                            return uploadedPicture;
                        }
                        else
                        {
                            return Data.Pictures.FirstOrDefault();
                        }
                    }
                }
                else
                {
                    return Data.Pictures.FirstOrDefault();
                }
            }
            else
            {
                return Data.Pictures.FirstOrDefault();
            }
        }
    }
}
