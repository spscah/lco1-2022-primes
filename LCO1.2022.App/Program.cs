using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCO1._2022.App
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Func<int, List<int>>> functions = new List<Func<int, List<int>>> { Nat, DanielH, Charles2, Victor, Jude, DannyC, Shashwat, George, Henry, Stefano, Charles};

            for (int i = 3; i < 8; ++i) {
                int target = (int)Math.Pow(10, i);

                // IList<ValueTuple<int, string, int, double>> times = new List<ValueTuple<int, string, int, double>>();
                Console.WriteLine($"primes up to {target}");
                foreach (Func<int, List<int>> fn in functions)
                {
                    DateTime start = DateTime.Now;
                    List<int> results = fn(target);
                    double ms = (DateTime.Now - start).TotalMilliseconds;
                    Console.WriteLine($"{fn.Method.Name,10}: {results.Count,5} in {ms,10}ms");
                }
            }
            Console.ReadKey();
        }


        static List<int> Victor(int n)
        {

            var mainlist = Enumerable.Range(2, (n - 1)).ToList();
            List<int> primes = new List<int> { 2 };

            while (mainlist.Count() > 0)
            {
                mainlist.RemoveAll(i => i % primes[primes.Count() - 1] == 0);
                //spent about 30 minutes trying to find an alternative to
                //finding the length of the list and then taking away 1
                //because I figured it would be slow but I found absolutely
                //nothing that made any sense so I think this will do for
                //now. Is there a way to easily and quickly get the last
                //element of a list?
                if (mainlist.Count() > 0)
                {
                    primes.Add(mainlist[0]);
                }
            }

            return primes;




        }
        static List<int> Jude(int n)
        {
            int i, p, k;
            List<int> primelist = new List<int> { };

            for (i = 2; i <= n; i++)
            {
                k = 2;
                p = 1;
                while (k < i)
                {
                    if (i % k == 0)
                    {
                        p = 0;
                        break;
                    }
                    k++;
                }
                if (p == 1)
                {
                    primelist.Add(i);
                }
            }
            return primelist;
        }
        static List<int> DannyC(int X)
        {
            if (X <= 2)
            {
                return new List<int>();
            }
            else if (X <= 3)
            {
                return new List<int>() { 2 };
            }
            else
            {
                List<int> ListOfPrimes = new List<int>() { 2, 3 };
                bool isPrime = true;
                for (int i = 5; i < X; i += 2)
                {
                    isPrime = true;
                    for (int n = 1; n < ListOfPrimes.Count; ++n)
                    {
                        if (i % ListOfPrimes[n] == 0)
                        {
                            isPrime = false;
                        }
                    }
                    if (isPrime) ListOfPrimes.Add(i);
                }
                return ListOfPrimes;
            }
        }
        static List<int> Shashwat(int bound)
        {
            List<int> Primes = new List<int> { 2 };
            for (int i = 3; i <= bound; i++)
            {
                bool is_prime = true;
                foreach (int j in Primes)
                {
                    if (i == j) { continue; }
                    if (i % j == 0)
                    {

                        is_prime = false;
                        break;
                    }
                }
                if (is_prime)
                {

                    Primes.Add(i);
                }

            }

            return Primes;
        }
        static List<int> Shashwat2(int n)
        {
            return
                Enumerable
                    .Range(2, (int)(n * (Math.Log(n) + Math.Log(System.Math.Log(n)) - 0.5)))
                    .Where(x => Enumerable.Range(2, x - 2).All(y => x % y != 0))
                    .TakeWhile((p, index) => index < n).ToList();
        }
        static List<int> George(int limit)
        {
            List<int> primes = new List<int>();
            for (int a = 2; a < limit; a++)
            {
                bool p = true;
                for (int b = 0; b < primes.Count; b++)
                {
                    if (a % primes[b] == 0) p = false;
                }
                if (p) primes.Add(a);
            }
            return primes;
        }
        static List<int> Henry(int topNum)
        {
            List<int> primeNumbers = new List<int>();
            int primeLength = 0;
            bool notPrime;
            bool correct = true;
            if (topNum > 2)
            {
                primeNumbers.Add(2);
                for (int i = 3; i < topNum + 1; i++)
                {
                    notPrime = false;
                    primeLength = primeNumbers.Count;
                    for (int j = 0; j < primeLength; j++)
                    {
                        if (i % primeNumbers[j] == 0)
                        {
                            notPrime = true;
                        }
                    }
                    if (notPrime == false)
                    {
                        primeNumbers.Add(i);
                    }
                }
            }
            return primeNumbers;
        }
        static List<int> Nat(int n)
        {
            List<int> primelist = new List<int> { 2, 3 };
            int counter = 0;
            for (int x = 4; x <= n; x++)
            {
                Int64 sqr = Convert.ToInt64(Math.Sqrt(x));
                bool found = false;
                for (int i = 0; i < primelist.Count; i++)
                {
                    if (primelist[i] > sqr) break;
                    if (x % primelist[i] == 0) { found = true; break; }
                }
                if (!found) primelist.Add(x);
            }
            return primelist;
        }
        static List<int> Stefano(int i)
        {
            List<int> Primes = new List<int>();
            // Only checks odd numbers to save performance
            for (int n = 3; n < i; n += 2)
            {
                if (IsPrime(n))
                {
                    Primes.Add(n);
                }
            }
            return Primes;

            // https://stackoverflow.com/a/21176886
            bool IsPrime(int n)
            {
                if (n > 1)
                {
                    return Enumerable.Range(1, n).Where(x => n % x == 0)
                                     .SequenceEqual(new[] { 1, n });
                }
                return false;
            }
        }
        static List<int> Charles(int n)
        {
            // Uses sieve of Eratosthenes (https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes), a very efficient algorithm for generating primes up to n
            // Runs in O(n log n) time
            // Worst case: low numbers of n will have a higher distribution of primes, meaning that it will take slightly longer relative to the size of n
            // Best case: n is a large number where all the large numbers are already removed
            // This algorithm will have a difficult time running on small processors when n is very large, as it requires a long list of numbers up to n
            // This algorithm is also pretty computationally intensive (for large values of n) as there are many numbers to divide by
            List<int> nums = Enumerable.Range(2, n).Select(i => (int)i).ToList();
            List<int> primes = new List<int>();
            while (nums.Count != 0)
            {
                int current = nums[0];
                nums.RemoveAt(0);
                primes.Add(current);
                while (true)
                {
                    bool breakFor = false;
                    for (int i = 0; i < nums.Count; i++)
                    {
                        if (nums[i] % current == 0)
                        {
                            nums.RemoveAt(i);
                            breakFor = true;
                            break;
                        }
                    }
                    if (!breakFor) break;
                }
            }
            return primes;
        }

        static List<int> Charles2(int n)
        {
            List<int> primes = new List<int>() { 2, 3 };

            for (int num = 5; num <= n; num += 2)
            {
                int maxNum = (int)Math.Sqrt((double)num);
                bool isPrime = true;
                foreach (int i in primes)
                {
                    if (i > maxNum)
                    {
                        break;
                    }
                    if (num % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(num);
                }

            }
            return primes;
        }

        static List<int> DanielH(int num)
        {
            List<int> Return = new List<int> { 2, 3 };
            for (int i = 4; i <= num; i++)
            {
                Return.Add(i);
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        Return.Remove(i);
                    }
                }
            }
            return Return;
        }
    }
}
