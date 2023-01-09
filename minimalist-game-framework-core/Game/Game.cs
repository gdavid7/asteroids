using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1120, 630);
    List<Theme> themes;
    EntryScreen es;

    public Game()
    {
        themes = new List<Theme>();
        themes.Add(new Theme("Dark Mode", "title.PNG", "background.png", "backgroundGrid2.png", "rocket.png", "asteroid2.png", Color.White));
        themes.Add(new Theme("Light Mode", "title.PNG", "background2.png", "backgroundGrid1.png", "rocket2.png", "asteroid.png", Color.Black));
        Theme.setThemes(themes);

        es = new EntryScreen(Resolution, themes);
    }

    public void Update()
    {
        es.draw();
    }
}
