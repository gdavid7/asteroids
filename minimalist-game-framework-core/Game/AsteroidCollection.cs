using System;
using System.Collections;


	static class AsteroidCollection
	{
		private static readonly ArrayList collection =  new ArrayList();
    private static Random r = new Random();

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
                split(ast.getStage(), ast);
                collection.Remove(ast);
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

    public static void spawn()
    {
        Asteroid a = new Asteroid(new Vector2(r.Next(1280),r.Next(720) ), 100, new Vector2(100, 100), 1);
        collection.Add(a);
    }



    private static void split(int stage, Asteroid ast)
    {
        if (stage < 2)
        {
            add(new Asteroid(new Vector2(ast.getMov().X+50, ast.getMov().Y+50), 100, new Vector2(100 / stage+1, 100 / stage+1),stage+1));
            add(new Asteroid(new Vector2(ast.getMov().X - 50, ast.getMov().Y - 50), 20, new Vector2(100 / stage+1, 100 / stage+1),stage+1));
        }       
    }

    

}


