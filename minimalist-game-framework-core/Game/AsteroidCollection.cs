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

    public static void handleAsteroidMoving()
    {
        foreach (Asteroid ast in collection)
        {
            ast.handleMoves();
        }
    }

	public static bool handleAsteroidShotCollisions(Bounds2 shot)
	{
        foreach (Asteroid ast in collection)
        {
            if (ast.handleShotCollisions(shot)){
				return true;
			}
        }
        return false;
    }

    public static bool handleAsteroidShipCollisions(Bounds2 ship)
    {
        foreach (Asteroid ast in collection)
        {
            if (ast.handleShipCollisions(ship))
            {
                return true;
            }
        }
        return false;
    }

    public static void add(Asteroid a)
		{
			collection.Add(a);
		}

	}


