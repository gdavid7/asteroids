 using System;

	class Powerup : Sprite{
	Random r;
	bool vis = false;
	public bool oneEngaged;
	public bool twoEngaged;
	public bool threeEngaged;


	public Powerup(Vector2 position, Vector2 size): base(position,size){
		r = new Random();
	}

	//moves powerup to random location
	public void handleRespawn()
	{
		setPos(new Vector2(r.Next(100, 1180), r.Next(100, 620)));
		bounds = new Bounds2(pos, new Vector2(100, 100));
        vis = true;
        Theme.drawPowerup(pos, 100, 0);

    }

	//draws texture in same location
	public void staticSpawn()
	{
        Theme.drawPowerup(pos, 100, 0);

    }

    public Boolean isVis()
	{
		return vis;
	}

	public void setVis(Boolean b)
	{
		vis = b;
	}

	public bool isAllDisengaged()
	{
		return !oneEngaged && !twoEngaged && !threeEngaged;
	}

	public void disengageAll()
	{
		oneEngaged = false;
		twoEngaged = false;
		threeEngaged = false;
	}

	//displays powerup status
	public void displayUI()
	{
        Vector2 location = new Vector2(10, Game.Resolution.Y * 10 / 11);

        if (oneEngaged && !twoEngaged && !threeEngaged)
        {
            Engine.DrawString("Powerup Activated: FASTER SHOOTING", location, Theme.getColor(), Game.buttonFont, TextAlignment.Left);
        }
        else if (twoEngaged && !oneEngaged && !threeEngaged)
        {
            Engine.DrawString("Powerup Activated: BIGGER HITS", location, Theme.getColor(), Game.buttonFont, TextAlignment.Left);
        }
        else if (threeEngaged && !twoEngaged && !oneEngaged)
        {
            Engine.DrawString("Powerup Activated: SLOWDOWN", location, Theme.getColor(), Game.buttonFont, TextAlignment.Left);
        }
    }

	// does actions when powerup is collected
	public void handleCollection()
	{
		vis = false;
		bounds = new Bounds2(0, 0, 0, 0);
		int p = r.Next(1, 4);
		if(p == 1)
		{
			disengageAll();
			oneEngaged = true;
			
		} else if (p == 2)
		{
			disengageAll();
			twoEngaged = true;
		} else
		{
			disengageAll();
			threeEngaged = true;
		}
	}


	
	
}

