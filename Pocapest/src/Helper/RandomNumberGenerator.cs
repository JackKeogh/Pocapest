using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocapest.src.Helper
{
	public class RandomNumberGenerator
	{
		private static Random rng = new Random();

		public static void UpdateSeed(int seed)
		{
			rng = new Random(seed);
		}

		public static void RandomiseSeed()
		{
			rng = new Random(((int)DateTime.Now.Ticks));
		}

		public static int GetRandomInt(int min, int max)
		{
			return rng.Next(min, max);
		}
	}
}
