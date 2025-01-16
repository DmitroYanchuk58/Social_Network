using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Helpers.Interfaces
{
    public interface IEncryption
    {
        public string Encrypt(string plaintext);

        public string Decrypt(string ciphertext);
    }
}
