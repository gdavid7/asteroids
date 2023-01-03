using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1280, 760);
    List<Theme> themes;
    EntryScreen es;

    public Game()
    {
        themes = new List<Theme>();
        themes.Add(new Theme());
        themes.Add(new Theme());


        es = new EntryScreen(Resolution, themes);
    }

    public void Update()
    {
        es.draw();
    }
}
