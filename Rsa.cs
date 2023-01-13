using System.Numerics;
using System.Text;

namespace RSA;

public class Rsa
{
    private const int MinimalPrime = 100;
    private const int MaximalPrime = 5000;

    public int[] GeneratePrimes()
    {
        var isPrime = new bool[MaximalPrime + 1];
        for (int i = 0; i <= MaximalPrime; i++) 
            isPrime[i] = true;

        for (int i = 2; i <= MaximalPrime; i++)
        {
            if (!isPrime[i]) continue;
            
            for (int j = 2 * i; j <= MaximalPrime; j += i) 
                isPrime[j] = false;
        }
        
        var primes = new List<int>();
        for (int i = MinimalPrime; i <= MaximalPrime; i++)
        {
            if (isPrime[i])
            {
                primes.Add(i);
            }
        }
        var rand = new Random();
        int first = rand.Next() % primes.Count / 2;
        int second = rand.Next() % primes.Count / 2 + primes.Count / 2;
        return new[] { primes[first], primes[second] };
    }

    public string Encrypt(string m, int e, int n)
    {
        var encodedMessage = new StringBuilder();
        foreach (char c in m)
        {
            BigInteger k = BigInteger.ModPow(c, e, n);
            encodedMessage.Append(k + " ");
        }
        return encodedMessage.ToString();
    }

    public string Decrypt(string input, int d, int n)
    {
        var result = new StringBuilder();
        var arr = input.Split(' ');
        foreach (string item in arr)
        {
            if(string.IsNullOrEmpty(item)) continue;
            
            BigInteger k = BigInteger.ModPow(BigInteger.Parse(item), d, n);
            result.Append((char)k);
        }
        return result.ToString();
    }

    public int GenerateEncryptor(int m)
    {
        for (int e = 10; e < m; e++)
        {
            if (BigInteger.GreatestCommonDivisor(m, e) == 1) return e;
        }

        return m - 1;
    }

    public int GenerateDecryptor(int e, int m)
    {
        //int d = (int)BigInteger.ModPow(e, m - 2, m);

        for (int X = 1; X < m; X++)
            if (((e % m) * (X % m)) % m == 1)
                return X;
        return 1;
        
        //return d;
    }
}