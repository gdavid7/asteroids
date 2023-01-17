using System;

	class Powerup{

	private Bounds2 bounds;
	private Vector2 pos;
	private int size;

	public Powerup(Vector2 position, int size){
		pos = position;
		size = this.size;
		bounds = new Bounds2(position, new Vector2(size, size));
	}

	public Vector2 getPos()
	{
		return pos;
	}

	public void setPos(Vector2 position)
	{
		pos = position;
	}

	
}

