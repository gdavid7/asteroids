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

        if (Engine.GetKeyDown(Key.Left,allowAutorepeat: true))
        {
            rot -= 7;
        } else if (Engine.GetKeyDown(Key.Right, allowAutorepeat: true))
        {
            rot += 7;
        }


        if(Engine.GetKeyDown(Key.Up,allowAutorepeat: true))
        {
            mov = new Vector2(mov.X + 10, mov.Y + 10);
        }
    }
}