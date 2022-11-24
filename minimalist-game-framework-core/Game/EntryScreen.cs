using System;
using System.Collections.Generic;
using System.Text;

class EntryScreen
{
    Vector2 resolution;
    Font titleFont;
    Vector2 titleLocation;
    Font buttonFont;
    Vector2 startLocation;
    //Vector2 highLocation;
    Bounds2 startBounds;
    //Bounds2 highBounds;


    public EntryScreen(Vector2 resolution)
    {
        this.resolution = resolution;
        titleFont = Engine.LoadFont("Starjedi.ttf", 72);
        titleLocation = new Vector2(resolution.X / 2, resolution.Y / 5);

        buttonFont = Engine.LoadFont("Oswald-Regular.ttf", 36);
        startLocation = new Vector2(resolution.X / 2, resolution.Y / 2);
        //highLocation = new Vector2(resolution.X / 2, resolution.Y * 2 / 3);
    }

    public void draw()
    {
        Engine.DrawString("Asteroids", titleLocation, Color.White, titleFont, TextAlignment.Center);
        startBounds = Engine.DrawString("START", startLocation, Color.White, buttonFont, TextAlignment.Center);
        //highBounds = Engine.DrawString("HIGH SCORE", highLocation, Color.White, buttonFont, TextAlignment.Center);
    }

    public Boolean isStartClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && startBounds.Contains(Engine.MousePosition);
    }

    /*public Boolean isHighScoreClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && highBounds.Contains(Engine.MousePosition);
    }*/

}
