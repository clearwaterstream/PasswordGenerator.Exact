using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace clearwaterstream.Security
{
    /// <summary>
    /// Crypto-secure random password generator
    /// </summary>
    public static class PasswordGenerator
    {
        private static readonly char[] default_specialChars = "!@#$%^&*-_+=".ToCharArray();
        private static readonly char[] letters = new char[52];

        static PasswordGenerator()
        {
            for (int i = 0; i < 26; i++)
            {
                letters[i] = (char)('a' + i);
                letters[i + 26] = (char)('A' + i);
            }
        }

        public static string GeneratePassword(int length, int numberOfDigits, int numberOfSpecialChars)
        {
            return GeneratePassword(length, numberOfDigits, numberOfSpecialChars, default_specialChars);
        }

        public static string GeneratePassword(int length, int numberOfDigits, int numberOfSpecialChars, char[] specialChars)
        {            
            var passwordMinLength = 8;
            var passwordMaxLength = 128;

            if (length < passwordMinLength || length > passwordMaxLength)
                throw new ArgumentOutOfRangeException(nameof(length), $"must be between {passwordMinLength} and {passwordMaxLength}");

            var numOfNonLetters = numberOfDigits + numberOfSpecialChars;
            var numOfLetters = length - numOfNonLetters;

            if (numOfNonLetters > length - 1 || numOfNonLetters < 0)
                throw new ArgumentOutOfRangeException($"{nameof(numberOfDigits)} and/or {nameof(numberOfSpecialChars)}", $"sum must not be less then zero and cannot exceed {nameof(length)} - 1");

            if(numberOfSpecialChars > 0 && specialChars.Length == 0)
                throw new ArgumentException($"Please supply an array of special characters to use", nameof(specialChars));

            var rndBytes = CryptoRandomGenerator.Instance.GenerateBytes(length);
            var passwordBuffer = new char[length];

            var iterator = 0;

            for (int i = 0; i < numOfLetters; i++)
            {
                var randNum = rndBytes[iterator] % letters.Length;
                char rndLetter = letters[randNum];

                passwordBuffer[iterator] = rndLetter;

                iterator++;
            }

            for (int i = 0; i < numberOfDigits; i++)
            {
                var randNum = rndBytes[iterator] % 10;
                var rndDigit = (char)('0' + randNum); // 0 to 9

                passwordBuffer[iterator] = rndDigit;

                iterator++;
            }

            for (int i = 0; i < numberOfSpecialChars; i++)
            {
                var randNum = rndBytes[iterator] % specialChars.Length;
                var rndSpecialChar = specialChars[randNum];

                passwordBuffer[iterator] = rndSpecialChar;

                iterator++;
            }

            // System.Random is not thead safe so have to create a new one and seed it with something other then Ticks
            var rndBytesForSeed = CryptoRandomGenerator.Instance.GenerateBytes(4);
            var rndKey = BitConverter.ToInt32(rndBytesForSeed, 0);

            passwordBuffer.Shuffle(rndKey);

            return new string(passwordBuffer);
        }
    }
}
