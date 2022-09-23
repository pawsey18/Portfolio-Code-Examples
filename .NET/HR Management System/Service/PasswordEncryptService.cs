using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// Service for encrypting a password using SHA256
    /// </summary>
    public class PasswordEncryptService
    {
        
        /// <summary>
        /// Encrypt user's password when logging in or creating a new employee
        /// </summary>
        /// <param name="value"></param>
        /// <returns>a string of the hashed password</returns>
       public  string Encrypt(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }


        /// <summary>
        /// Compares two values to check if password matches.
        /// </summary>
        /// <param name="formPassword"></param>
        /// <param name="dbHash"></param>
        /// <returns>a boolean value based on password matching of the db password and form password</returns>
        public bool CompareLoginWithHash(string formPassword, string dbHash)
        {
            var hashFormPassword = Encrypt(formPassword);

            if (hashFormPassword != dbHash)
            {
                return false;
            }
            return true;
        }
    }
}
