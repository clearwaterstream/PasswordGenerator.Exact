// https://dotnetcodr.com/2016/10/05/generate-truly-random-cryptographic-keys-using-a-random-number-generator-in-net/
// https://en.wikipedia.org/wiki/Universally_unique_identifier#Version_4_(random)
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace clearwaterstream.Security
{
    public class CryptoRandomGenerator : IDisposable
    {
        public static readonly CryptoRandomGenerator Instance = new CryptoRandomGenerator();

        readonly RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();

        private CryptoRandomGenerator() { }

        public string GenerateKey()
        {
            // guids/uuids are 16 bytes.
            return GenerateKey(16);
        }

        public string GenerateKey(int keyLength)
        {
            var rndBytes = new byte[keyLength];

            rngCryptoServiceProvider.GetBytes(rndBytes);

            return WebEncoders.Base64UrlEncode(rndBytes);
        }

        public byte[] GenerateBytes(int numOfBytes)
        {
            var rndBytes = new byte[numOfBytes];

            rngCryptoServiceProvider.GetBytes(rndBytes);

            return rndBytes;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                rngCryptoServiceProvider?.Dispose();
            }
        }
    }
}
