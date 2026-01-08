using System.Numerics;
using System.Text;

namespace RSATwo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int choice = 1;
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    EasyDemoOne();
                    break;
                case 2:
                    HardDemoOne();
                    break;
                case 3:
                    HardDemoTwo();
                    break;
                case 4:
                    HardDemoThree();
                    break;
                case 5:
                    RunAllDemos();
                    break;
            }
        }


        /// <summary>
        /// Two six digit primes. Weak.  Encrypt and Decrypt a string.
        /// </summary>
        public static void EasyDemoOne()
        {
            Console.WriteLine("Easy Demo");
            string message = "Hello World";
            BigInteger p = BigInteger.Parse("54401");
            BigInteger q = BigInteger.Parse("67943");

            // 1. Calculate n (modulus) and phi (totient)
            BigInteger n = p * q;
            BigInteger phi = (p - 1) * (q - 1);

            // 2. Choose e (Public Exponent) - 65537 is the industry standard
            BigInteger e = 65537;

            // 3. Calculate d (Private Exponent) 
            // d is the modular multiplicative inverse of e mod phi
            BigInteger d = Util.ModInverse(e, phi);

            Console.WriteLine($"Modulus (n): {n}");
            Console.WriteLine($"Public Exponent (e): {e}");
            Console.WriteLine($"Private Exponent (d): {d}\n");

            // 4. Encrypt
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            List<BigInteger> encrypted = new List<BigInteger>();

            foreach (byte b in bytes)
            {
                // RSA Formula: C = m^e mod n
                encrypted.Add(BigInteger.ModPow(b, e, n));
            }

            Console.WriteLine("ENCRYPTED:");
            Console.WriteLine(string.Join(" ", encrypted));

            // 5. Decrypt
            List<byte> decryptedBytes = new List<byte>();
            foreach (BigInteger item in encrypted)
            {
                // RSA Formula: M = c^d mod n
                decryptedBytes.Add((byte)BigInteger.ModPow(item, d, n));
            }

            string result = Encoding.UTF8.GetString(decryptedBytes.ToArray());
            Console.WriteLine($"\nDECRYPTED:\n{result}");
            Console.WriteLine("\n");
        }



        /// <summary>
        /// Two 512-digit primes. Strong. Get the private key.
        /// </summary>
        public static void HardDemoOne()
        {
            Console.WriteLine("Hard Demo One");

            // 512-bit Prime P
            BigInteger p = BigInteger.Parse("9886311149454848507850893527230191244458319690184285189708709149021543166567119095692695272102143093952701199341644754593175110515152528148972580796850401");

            // 512-bit Prime Q
            BigInteger q = BigInteger.Parse("11796068213606622359416556108139591967280961811566870591244109489568910834316712398553257766099511119777926174416972037985444390666013627581216508821949101");

            BigInteger n = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger e = 65537;

            // GCD Check: Crucial step
            if (BigInteger.GreatestCommonDivisor(e, phi) != 1)
            {
                Console.WriteLine("e and phi are not coprime. RSA will not work.");
            }

            // ModInverse using EEA is instant even for these massive numbers
            BigInteger d = Util.ModInverse(e, phi);

            Console.WriteLine($"Modulus N is {n.ToString().Length} digits long.");
            Console.WriteLine($"Calculated Private Key D: {d.ToString().Substring(0, 20)}...");
            Console.WriteLine("\n");
        }


        /// <summary>
        /// Generate two 512-digit primes. Strong. Get the private key.
        /// </summary>
        private static void HardDemoTwo()
        {
            Console.WriteLine("Hard Demo Two");
            Console.WriteLine("Generating 512-bit primes...");

            BigInteger p = Util.GeneratePrime(512);
            BigInteger q = Util.GeneratePrime(512);

            BigInteger n = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger e = 65537;

            // Ensure e is valid for this specific p and q
            while (BigInteger.GreatestCommonDivisor(e, phi) != 1)
            {
                p = Util.GeneratePrime(512);
                phi = (p - 1) * (q - 1);
            }

            // The fast "ModInverse"
            BigInteger d = Util.ModInverse(e, phi);

            Console.WriteLine($"\nGenerated P: {p.ToString().Substring(0, 15)}...");
            Console.WriteLine($"Generated Q: {q.ToString().Substring(0, 15)}...");
            Console.WriteLine($"Modulus Size: {n.ToByteArray().Length * 8} bits");
            Console.WriteLine($"Calculated D instantly using EEA.");
            Console.WriteLine("\n");
        }


        /// <summary>
        /// Generate two 512-digit primes. Strong. Encrypt and Decrypt a string.
        /// </summary>
        public static void HardDemoThree()
        {
            Console.WriteLine("Hard Demo Three");

            // 1. Setup Keys (1024-bit total)
            BigInteger p = Util.GeneratePrime(512);
            BigInteger q = Util.GeneratePrime(512);
            BigInteger n = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger e = 65537;
            BigInteger d = Util.ModInverse(e, phi);

            // 2. Convert Message to ONE BigInteger
            string message = "RSA is powerful!";
            BigInteger m = new BigInteger(Encoding.UTF8.GetBytes(message), true, true);

            if (m >= n)
            {
                Console.WriteLine("Message too long for key size.");
            }

            // 3. Encrypt: C = M^e mod N
            BigInteger c = BigInteger.ModPow(m, e, n);
            Console.WriteLine($"Encrypted Block: {c.ToString().Substring(0, 30)}...");

            // 4. Decrypt: M = C^d mod N
            BigInteger decryptedM = BigInteger.ModPow(c, d, n);
            string result = Encoding.UTF8.GetString(decryptedM.ToByteArray(true, true));

            Console.WriteLine($"Decrypted: {result}");
            Console.WriteLine("\n");
        }

        private static void RunAllDemos()
        {
            EasyDemoOne();
            HardDemoOne();
            HardDemoTwo();
            HardDemoThree();
        }
    }
}