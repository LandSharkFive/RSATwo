
using System.Numerics;
using System.Security.Cryptography;

namespace RSATwo
{
    public class Util
    {
        // Standard Modular Inverse function (e * d % phi == 1)
        public static BigInteger ModInverse(BigInteger e, BigInteger n)
        {
            BigInteger t = 0, newt = 1;
            BigInteger r = n, newr = e;

            while (newr != 0)
            {
                BigInteger quotient = r / newr;
                (t, newt) = (newt, t - quotient * newt);
                (r, newr) = (newr, r - quotient * newr);
            }

            if (t < 0) t = t + n;
            return t;
        }

        /// <summary>
        /// Generate a prime with n bits.
        /// </summary>
        public static BigInteger GeneratePrime(int bits)
        {
            while (true)
            {
                // 1. Generate random bytes
                byte[] data = new byte[bits / 8];
                RandomNumberGenerator.Fill(data);

                // Ensure the number is positive and has the correct bit-length
                data[data.Length - 1] |= 0x80; // Set top bit to ensure bit-length
                data[0] |= 0x01;              // Set bottom bit to ensure it is odd

                BigInteger p = new BigInteger(data, isUnsigned: true, isBigEndian: true);

                // 2. Primality Test
                // In .NET 7+, BigInteger has IsProbablePrime. 
                // For older versions, you'd implement Miller-Rabin.
                if (IsProbablePrime(p, 20))
                {
                    return p;
                }
            }
        }

        /// <summary>
        /// Miller-Rabin Primality Test.
        /// </summary>
        public static bool IsProbablePrime(BigInteger source, int k)
        {
            if (source < 2) return false;
            if (source == 2 || source == 3) return true;
            if (source % 2 == 0) return false;

            // Write n-1 as 2^s * d
            BigInteger d = source - 1;
            int s = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            // Witness loop
            for (int i = 0; i < k; i++)
            {
                BigInteger a = GetRandomInRange(2, source - 2);
                BigInteger x = BigInteger.ModPow(a, d, source);

                if (x == 1 || x == source - 1) continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == source - 1) break;
                }

                if (x != source - 1) return false;
            }
            return true;
        }

        /// <summary>
        /// Get a Random Number between min and max.
        /// </summary>
        private static BigInteger GetRandomInRange(BigInteger min, BigInteger max)
        {
            byte[] data = max.ToByteArray();
            BigInteger res;
            do
            {
                RandomNumberGenerator.Fill(data);
                res = new BigInteger(data);
            } while (res < min || res > max);
            return res;
        }

    }
}
