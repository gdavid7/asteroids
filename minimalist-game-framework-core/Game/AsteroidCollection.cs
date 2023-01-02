using System;
using System.Collections;


	static class AsteroidCollection
	{
		private static readonly ArrayList collection =  new ArrayList();
 

		public static void handleAsteroidSpawning()
		{
			foreach (Asteroid ast in collection)
			{
				ast.handleSpawns();
			}
		}

		public static void add(Asteroid a)
		{
			collection.Add(a);
		}

	}


