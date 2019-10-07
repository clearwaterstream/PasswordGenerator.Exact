using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace clearwaterstream.Security.Test
{
    public class PasswordGeneratorTest
    {
        [Fact]
        public void LettersOnly()
        {
            var pwd = PasswordGenerator.GeneratePassword(10, 0, 0, null);

            Assert.True(pwd.Length == 10);

            var anyDigits = pwd.Any(l => char.IsDigit(l));
            var numLetters = pwd.Count(l => char.IsLetter(l));

            Assert.False(anyDigits);
            Assert.True(numLetters == 10);
        }

        [Fact]
        public void TwoDigits()
        {
            var pwd = PasswordGenerator.GeneratePassword(10, 2, 0, null);

            Assert.True(pwd.Length == 10);

            var numDigits = pwd.Count(l => char.IsDigit(l));
            var numLetters = pwd.Count(l => char.IsLetter(l));

            Assert.True(numDigits == 2);
            Assert.True(numLetters == 8);
        }

        [Fact]
        public void TwoDigitsTwoSpecial()
        {
            var pwd = PasswordGenerator.GeneratePassword(10, 2, 2, null);

            Assert.True(pwd.Length == 10);

            var numDigits = pwd.Count(l => char.IsDigit(l));
            var numLetters = pwd.Count(l => char.IsLetter(l));
            var numSpecial = pwd.Length - numLetters - numDigits;

            Assert.True(numDigits == 2);
            Assert.True(numSpecial == 2);
            Assert.True(numLetters == 6);
        }

        [Fact]
        public void DupeCheck()
        {
            ConcurrentBag<string> passwords = new ConcurrentBag<string>();

            Parallel.For(0, 1000, i =>
            {
                var pwd = PasswordGenerator.GeneratePassword(10, 2, 2, null);

                passwords.Add(pwd);
            });

            Assert.True(passwords.Count == 1000);

            var distinctCount = passwords.Distinct().Count();

            Assert.True(distinctCount == 1000);
        }
    }
}
