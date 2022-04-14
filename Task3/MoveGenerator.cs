using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Task3
{
    public class MoveGenerator
    {
        public string ComputerMove { get; private set; }
        public string HMAC { get; private set; }
        public string Key { get; private set; }

        private readonly string[] args;

        public MoveGenerator(string[] args)
        {
            this.args = args;
        }

        public void NextMove()
        {
            int computerMove = RandomNumberGenerator.GetInt32(args.Length);
            byte[] key = new byte[32];
            RandomNumberGenerator.Fill(key);
            ComputerMove = args[computerMove];
            Key = ByteArrayToHexString(key);
            HMAC = ByteArrayToHexString(HashMAC(Key, ComputerMove));
            
        }

        private static byte[] HashMAC(String key, String message)
        {
            var hash = new HMACSHA256(Encoding.ASCII.GetBytes(key));
            return hash.ComputeHash(Encoding.ASCII.GetBytes(message));
        }

        private static string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
