using System;

	class Asteroid
	{
		private Vector2 move;
		private float rotation;
		private Vector2 size;
	private Bounds2 bounds;

	public Asteroid(Vector2 move, float rot, Vector2 size)
		{
		this.move = move;
		this.rotation = rot;
		this.size = size;
		this.bounds = new Bounds2(move,size);
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

    public Bounds2 getBounds()
    {
        return bounds;
    }

	public void resetBounds()
	{
		bounds = new Bounds2(move, size);
	}


    public void wraparound()
	{
        //x wraparound asteroid
        if (this.getMov().X <= -80)
        {
            this.setMov(new Vector2(1810, this.getMov().Y));
        }
        else if (this.getMov().X >= 1820)
        {
            this.setMov(new Vector2(-50, this.getMov().Y));
        }

        //y wraparound asteroid 
        if (this.getMov().Y <= -80)
        {
            this.setMov(new Vector2(this.getMov().X, 970));
        }
        else if (this.getMov().Y >= 980)
        {
            this.setMov(new Vector2(this.getMov().X, -50));
        }

    }


}


