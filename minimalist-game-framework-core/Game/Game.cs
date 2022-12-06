    using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1920, 1080);
    EntryScreen es = new EntryScreen(Resolution);
    

    Texture ship = Engine.LoadTexture("ship.png");
    Texture asteroid = Engine.LoadTexture("asteroid.png");
    Texture shot = Engine.LoadTexture("projectile.png");

    float time = 0;

    //ship vars
    float rot = 0;
    Vector2 mov = new Vector2(100, 100);
    float inertia = 100;
    bool fly = false;
    
    //shot variables
    Vector2 smov = new Vector2(400, 400);
    bool shoot = false;
    float rotLock = 0;


    Asteroid a = new Asteroid( new Vector2(600, 600),100,new Vector2(100,100));
    Asteroid b = new Asteroid(new Vector2(400, 800), 60, new Vector2(100, 100));



    public Game()
    {

    }


    public void log(int score)
    {
        // log score, print top 10 highest scores, print score history of user


        System.Diagnostics.Debug.WriteLine("Your Score is: " + score + ". If you would like to save your score, please enter your name in the console. If not, type N.");
        String name = Console.ReadLine();
        if(name.Equals("N") == false)
        {
            scoreboard.append(name, score.ToString());
            System.Diagnostics.Debug.WriteLine(name + "'s Scores:");
            System.Diagnostics.Debug.WriteLine(scoreboard.retrieve(name));
        }
        System.Diagnostics.Debug.WriteLine("HIGH SCORES: ");
        String[] scores = scoreboard.getScoreboard();
        for(int i = 0; i <scores.Length; i++)
        {
            System.Diagnostics.Debug.WriteLine(scores[i]);
        }

    }

    public void Update()
    {
        time += Engine.TimeDelta;
        es.draw();

        
        Engine.DrawTexture(ship, mov, size: new Vector2(100, 100), rotation: rot);
        Engine.DrawTexture(asteroid,a.getMov(), size: a.getSize());
        Engine.DrawTexture(asteroid, b.getMov(), size: b.getSize());
        Engine.DrawTexture(shot, smov, size: new Vector2(100, 100));


        


        // SHOT SHOOTING //


        if (Engine.GetKeyDown(Key.Space) && !shoot)
        {
            shoot = true;
            rotLock = rot;
            
        }

        if (shoot)
        {
            smov = getDirectionalVector(smov, rotLock, 30);
        } else
        {
            smov = mov;
        }

        if ((time % .5 > -0.08 && time % .5 < 0.08) && shoot)
        {
            shoot = false;
            smov = mov;
        }



        // SHIP MOVEMENT //


        //moves ship with intertia

        if (Engine.GetKeyHeld(Key.Left))
        {
            rot -= 7;

        }

        else if (Engine.GetKeyHeld(Key.Right))
        {
            rot += 7;
        }

        if (Engine.GetKeyHeld(Key.Up))
        {
            mov = getDirectionalVector(mov, rot, 10);

        }

        if (Engine.GetKeyUp(Key.Up))
        {

            fly = true;
            inertia = 100;

        }

        if (fly == true)
        {
            mov = getDirectionalVector(mov, rot, inertia/10);
            inertia--;
            Console.WriteLine(inertia);
            if (inertia <= 1)
            {
                fly = false;
            }
        }

        //x wraparound
        if (mov.X <= -80)
        {
            mov.X = 1810;
        } else if(mov.X >= 1820)
        {
            mov.X = -50;
        }

        //y wraparound
        if (mov.Y <= -80)
        {
            mov.Y = 970;
        }
        else if (mov.Y >= 980)
        {
            mov.Y = -50;
        }

        // ASTEROID MOVEMENT //

        a.setMov(getDirectionalVector(a.getMov(), 120, 2));
        a.wraparound();

        b.setMov(getDirectionalVector(b.getMov(), 40, 2));
        b.wraparound();



    }


    public Boolean isCollide(Vector2 pos1, Vector2 size1, Vector2 po2, Vector2 size2)
    {

        return false;
    }


    public Vector2 getDirectionalVector(Vector2 cur, float rotation, float moveFactor)
    {
        float x = (float)(Convert.ToDouble(cur.X) + moveFactor * Math.Cos(ConvertDegreesToRadians(rotation)));
        float y = (float)(Convert.ToDouble(cur.Y) + moveFactor * Math.Sin(ConvertDegreesToRadians(rotation)));
        Vector2 ret = new Vector2(x,y);
        return ret;

    }

    

    public static double ConvertDegreesToRadians(double degrees)
    {
        double radians = (Math.PI / 180) * degrees;
        return (radians);

    }

}

