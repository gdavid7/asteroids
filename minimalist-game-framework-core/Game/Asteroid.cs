using System;

	class Asteroid
	{
		private Vector2 move;
		private float rotation;
		private Vector2 size;
	private Bounds2 bounds;
	private bool spawn;
    Texture asteroid = Engine.LoadTexture("asteroid.png");


    public Asteroid(Vector2 move, float rot, Vector2 size)
		{
		this.move = move;
		this.rotation = rot;
		this.size = size;
		this.bounds = new Bounds2(move,size);
		this.spawn = true;
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

	public void handleSpawns()
	{
        if (getSpawn())
        {
            resetBounds();
            Engine.DrawTexture(asteroid, getMov(), size:getSize());

        }
    }

    public Bounds2 getBounds()
    {
        return bounds;
    }

	public void resetBounds()
	{
		bounds = new Bounds2(move, size);
	}

	public void killBounds()
	{
		bounds = new Bounds2(Vector2.Zero, Vector2.Zero);
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


