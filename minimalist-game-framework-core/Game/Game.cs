using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1280, 720);
    Theme theme;
    EntryScreen es;

    public Game()
    {
        List<String> startBackgrounds = new List<String>() { "startBackgroundD.png", "startBackgroundL.png", "startBackgroundDG.png", "startBackgroundLG.png" };
        List<String> gameBackgrounds = new List<String>() { "gameBackgroundD.png", "gameBackgroundL.png", "gameBackgroundDG.png", "gameBackgroundLG.png" };
        List<String> rocketShips = new List<String>() { "rocketshipD.png", "rocketshipL.png" };
        List<String> asteroids = new List<String>() {"asteroid2.png", "asteroid.png"};

        theme = new Theme(Resolution, startBackgrounds, gameBackgrounds, startBackgrounds, rocketShips, asteroids, asteroids);

        
        

        es = new EntryScreen(Resolution, theme);
    }

    public void Update()
    {
        es.draw();
    }
}
