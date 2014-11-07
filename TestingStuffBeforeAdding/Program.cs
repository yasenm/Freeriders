namespace TestingStuffBeforeAdding
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Program
    {

        static void Main()
        {
        }

        private static string ClientId = "253ceccb0eb6e36";

        private static void UploadImage(byte[] image)
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

                Console.WriteLine(url);
            }
            catch (Exception s)
            {
                Console.WriteLine("Something went wrong. " + s.Message);
            }
        }
    }
}
