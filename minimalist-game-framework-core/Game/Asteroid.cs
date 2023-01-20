using System;

	class Asteroid : Sprite
	{
		private float rotation;
		private int assetNum = new Random().Next(0,4);

		public static float asteroidMovFactor = 2;

	// splitting vars
	int stage;
    

    public Asteroid(Vector2 pos, float rot, Vector2 size, int stage) : base( pos,size)
		{
		this.rotation = rot;
		this.stage = stage;
		AsteroidCollection.add(this);
		}
		
		
	

		public float getRot()
	{
		return rotation;
	}


	public int getStage()
	{
		return stage;
	}

	
	//spawns the asteroid every frame in its new position
	public void handleSpawns()
	{
            resetBounds();
			Theme.drawAsteroid(getPos(), assetNum, getSize());
    }

	//moves the asteroid by a factor of 2
	public void handleMoves()
	{
        setPos(Game.getDirectionalVector(getPos(), rotation, asteroidMovFactor));
        wraparound();
    }

	//returns bounds
    public Bounds2 getBounds()
    {
        return bounds;
    }

	//resets bounds to asteroid position
	public void resetBounds()
	{
		bounds = new Bounds2(pos, size);
	}

	//deletes the bound
	public void killBounds()
	{
		bounds = new Bounds2(Vector2.Zero, Vector2.Zero);
    }

	//returns if a shot has collided with the asteroid
	public bool handleShotCollisions(Bounds2 shot)
	{
        if (getBounds().Overlaps(shot))
        {
			return true;
        }
		return false;
    }

	//returns if a ship has collided with the asteroid
	public bool handleShipCollisions(Bounds2 ship)
	{
        if (getBounds().Overlaps(ship))
        {
			return true;
        }
		return false;
    }

    public void wraparound()
	{
        //x wraparound asteroid
        if (this.getPos().X <= -80)
        {
            this.setPos(new Vector2(1170, this.getPos().Y));
        }
        else if (this.getPos().X >= 1180)
        {
            this.setPos(new Vector2(-50, this.getPos().Y));
        }

        //y wraparound asteroid 
        if (this.getPos().Y <= -80)
        {
            this.setPos(new Vector2(this.getPos().X, 610));
        }
        else if (this.getPos().Y >= 620)
        {
            this.setPos(new Vector2(this.getPos().X, -50));
        }

    }


}


