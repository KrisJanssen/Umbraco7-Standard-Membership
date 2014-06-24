using System.Security.Cryptography;
using System.Text;

namespace U7StandardMembership.Code
{
    public class Helpers
    {
        public static string GravatarUrl(string emailAddress)
        {
            //Get email to lower
            var emailToHash = emailAddress.ToLower();

            // Create a new instance of the MD5CryptoServiceProvider object.  
            var md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(emailToHash));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            foreach (byte b in data)
            {
                sBuilder.Append(b.ToString("x2"));
            }

            var hashedEmail = sBuilder.ToString();  // Return the hexadecimal string.

            //Return the gravatar URL
            return "http://www.gravatar.com/avatar/" + hashedEmail;
        }
    }
}