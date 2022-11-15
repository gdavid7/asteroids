using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(640, 480);

    Texture ship = Engine.LoadTexture("ship.png");


    public Game()
    {

    }

    public void Update()
    {

        Engine.DrawTexture(ship, new Vector2(100, 100), size: new Vector2(100, 100));

    }
}