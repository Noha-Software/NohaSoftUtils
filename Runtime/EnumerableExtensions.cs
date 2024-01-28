using System;
using System.Collections.Generic;
using System.Linq;

namespace NohaSoftware.Utilities
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			return source.Shuffle(new Random());
		}

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (rng == null) throw new ArgumentNullException(nameof(rng));

			return source.ShuffleIterator(rng);
		}

		private static IEnumerable<T> ShuffleIterator<T>(
			this IEnumerable<T> source, Random rng)
		{
			var buffer = source.ToList();
			for (int i = 0; i < buffer.Count; i++)
			{
				int j = rng.Next(i, buffer.Count);
				yield return buffer[j];

				buffer[j] = buffer[i];
			}
		}

		/// <summary>Get a random element from the IEnumerable</summary>
		public static T GetRandom<T>(this IEnumerable<T> source)
		{
			int x = new Random().Next(source.Count());
			int i = 0;
			foreach (T t in source)
			{
				if (i == x) return t;
				else i++;
			}
			return default;
		}
		/// <summary>Get a random element from the IEnumerable where the predicate is true</summary>
		public static T GetRandom<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			return source.Where(predicate).GetRandom();
		}
	}
}