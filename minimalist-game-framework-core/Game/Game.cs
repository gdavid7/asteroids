using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(640, 480);

    Texture ship = Engine.LoadTexture("ship.png");


    float rot = 0;
    Vector2 mov = Vector2.Zero;

    public Game()
    {

    }

    public void Update()
    {

        Engine.DrawTexture(ship, mov, size: new Vector2(100, 100), rotation: rot);

        if(Engine.GetKeyHeld(Key.Up))
        {
            mov = new Vector2((float)(Convert.ToDouble(mov.X) + 10*Math.Cos(ConvertDegreesToRadians(rot))), (float)(Convert.ToDouble(mov.Y) + 10 * Math.Sin(ConvertDegreesToRadians(rot))));
        }

        if (Engine.GetKeyHeld(Key.Left))
        {
            rot -= 7;
        }
        else if (Engine.GetKeyHeld(Key.Right))
        {
            rot += 7;
        }

    }


    public Vector2 getDirectionalVector(Vector2 cur, float rotation, float adv)
    {


        return Vector2.Zero;
    }

    public static double ConvertDegreesToRadians(double degrees)
    {
        double radians = (Math.PI / 180) * degrees;
        return (radians);
    }
}

