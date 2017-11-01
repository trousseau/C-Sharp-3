using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab03
{
	public static class MathStuff
	{
		/// <summary>
		/// Determines the approximate number of primes less than the given number
		/// </summary>
		/// <param name="x">Number to approximate number of primes below it</param>
		/// <returns>Approximate number of primes</returns>
		public static ulong ApproximateNumberOfPrimes(ulong x)
		{
			double num = ((double)x / (Math.Log((double)x, Math.E) - 1)) * 1.05;
			return (ulong)num;
		}


		/// <summary>
		/// Brute force algorithm to determine if a number is prime
		/// </summary>
		/// <param name="number">Number to test</param>
		/// <returns>true if prime, false otherwise</returns>
		public static bool IsPrime(ulong number)
		{
			if (number <= 1)
			{
				return false;
			}
			else if (number == 2 || number == 3 || number == 5)
			{
				return true;
			}
			else if ((number & 1) == 0)
			{
				return false;
			}
			else if (number % 5 == 0)
			{
				return false;
			}
			else
			{
				ulong max = (uint)Math.Sqrt(number);
				int counter = 3;

				for (ulong i = 3; i <= max; i += 2)
				{
					if (counter == 4)
					{
						counter = 0;
						continue;
					}
					counter++;

					if (number % i == 0)
					{
						return false;
					}
				}
				return true;
			}
		}
	}
}
