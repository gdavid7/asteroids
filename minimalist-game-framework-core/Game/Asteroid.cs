using System;

	class Asteroid
	{
		private Vector2 move;
		private float rotation;
		private Vector2 size;
		private Bounds2 bounds;
		private bool spawn;
		Texture asteroid = Engine.LoadTexture("awhite.png");

		public float asteroidMovFactor = 2;

	// splitting vars
	int stage;
    

    public Asteroid(Vector2 move, float rot, Vector2 size, int stage)
		{
		this.move = move;
		this.rotation = rot;
		this.size = size;
		this.bounds = new Bounds2(move,size);
		this.spawn = true;
		this.stage = stage;
		AsteroidCollection.add(this);
		}
		
		public Vector2 getMov()
	{
		return move;
	}

		public void setMov(Vector2 v)
	{
		move = v;
	}

		public float getRot()
	{
		return rotation;
	}

		public Vector2 getSize()
	{
		return size;
	}

	public int getStage()
	{
		return stage;
	}

	public Boolean getSpawn()
	{
		if (!spawn)
		{
			killBounds();
		}
		return spawn;
	}

	public void setSpawn(Boolean a)
	{
		spawn = a;
	}

	//spawns the asteroid every frame in its new position
	public void handleSpawns()
	{
        if (getSpawn())
        {
            resetBounds();
			
            Engine.DrawTexture(asteroid, getMov(), size:getSize());

        }
    }

	//moves the asteroid by a factor of 2
	public void handleMoves()
	{
        setMov(Game.getDirectionalVector(getMov(), rotation, asteroidMovFactor));
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
		bounds = new Bounds2(move, size);
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
            setSpawn(false);
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
        if (this.getMov().X <= -80)
        {
            this.setMov(new Vector2(1170, this.getMov().Y));
        }
        else if (this.getMov().X >= 1180)
        {
            this.setMov(new Vector2(-50, this.getMov().Y));
        }

        //y wraparound asteroid 
        if (this.getMov().Y <= -80)
        {
            this.setMov(new Vector2(this.getMov().X, 610));
        }
        else if (this.getMov().Y >= 620)
        {
            this.setMov(new Vector2(this.getMov().X, -50));
        }

    }


}


