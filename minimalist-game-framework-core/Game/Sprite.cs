using System;

	class Sprite
	{
	protected Bounds2 bounds;
	protected Vector2 pos;
	protected Vector2 size;
		public Sprite(Vector2 position, Vector2 sizes)
		{
        pos = position;
        size = sizes;
        bounds = new Bounds2(position, sizes);
		}


    public Vector2 getPos()
    {
        return pos;
    }

    public void setPos(Vector2 position)
    {
        pos = position;
    }

    public Vector2 getSize()
    {
        return size;
    }

    public void setSize(int s)
    {
        size = new Vector2(s, s);
    }
}

	


