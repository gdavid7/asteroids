using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1920, 1080);
    EntryScreen es = new EntryScreen(Resolution);
    

    Texture ship = Engine.LoadTexture("ship.png");


    float rot = 0;
    Vector2 mov = new Vector2(100,100);
    float inertia = 100;
    bool fly = false;

    public Game()
    {

    }

    public void Update()
    {
        es.draw();

        
        Engine.DrawTexture(ship, mov, size: new Vector2(100, 100), rotation: rot);

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
            mov = getDirectionalVector(mov, rot, 5);
            
        }

        if (Engine.GetKeyUp(Key.Up))
        {

            fly = true;
            inertia = 100;

        }


        //moves with intertia
        if(fly == true)
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

