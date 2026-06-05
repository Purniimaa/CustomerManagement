using System.Security.Cryptography;
using System.Text;

namespace CustomerManagement.Options
{
    public class ShaAlgo
    {

        public string HashPassword(string password )
        {
            using(SHA256  sha256 = SHA256.Create())
            {
                byte[] passwordhasher = Encoding.UTF8.GetBytes( password );
                byte[] hash = sha256.ComputeHash(passwordhasher);

                StringBuilder sb = new StringBuilder();
                foreach( byte b in hash )
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public bool verify(string inputpassword,string storehash)
        {
            string hashpassd = HashPassword(inputpassword);
            return hashpassd == storehash;
        }
    }
}
