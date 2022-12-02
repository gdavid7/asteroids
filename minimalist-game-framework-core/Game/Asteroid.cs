using System;

	class Asteroid
	{
		private Vector2 move;
		private float rotation;
		private Vector2 size;

		public Asteroid(Vector2 move, float rot, Vector2 size)
		{
		this.move = move;
		this.rotation = rot;
		this.size = size;
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


		
	}


