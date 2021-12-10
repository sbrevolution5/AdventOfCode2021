using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Services
{
    internal static class PasswordChecker
    {
        public static int HowManyValidPasswords(string input)
        {
            List<Password> passwords = ParsePasswords(input);
            var count = 0;
            foreach (var password in passwords)
            {
                if (ValidatePart2(password))
                {
                    count++;
                }
            }
            return count;
        }

        private static bool ValidatePart2(Password password)
        {
            return password.PasswordString[password.Min-1]== password.KeyChar ^ password.PasswordString[password.Max-1] == password.KeyChar;
        }

        private static bool Validate(Password password)
        {
            var characters = password.PasswordString.OrderBy(c=>c).ToList();
            var charCount = 0;
            foreach (var c in characters)
            {
                if (c == password.KeyChar)
                {
                    charCount++;
                }
            }
            return charCount >= password.Min && charCount<= password.Max;
        }

        private static List<Password> ParsePasswords(string input)
        {
            var lines = input.Split('\n');
            var passwords = new List<Password>();
            foreach (var line in lines)
            {
                var pass = new Password();
                var parts = line.Split(' ');
                var minMax = parts[0].Split('-');
                pass.Min = int.Parse(minMax[0]);
                pass.Max = int.Parse(minMax[1]);
                pass.KeyChar = parts[1][0];
                pass.PasswordString = parts[2];
                passwords.Add(pass);
            }
            return passwords;
        }
    }
    internal class Password
    {
        public string PasswordString { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public char KeyChar { get; set; }
        public bool IsValid { get; set; }
    }
}
