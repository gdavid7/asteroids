using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

class highscorescreen
{
    Dictionary<String, String> dict;
    Vector2 resolution;
    Font buttonFont = Engine.LoadFont("Oswald-Regular.ttf", 30);
    Vector2 startLocation;
    Vector2 highLocation;
    Vector2 gridLocation;

    
    Bounds2 startBounds;
    Bounds2 highBounds;
    Bounds2 gridBounds;

 

   

    public highscorescreen(Vector2 resolution, Dictionary<String, String> dict)
    {
        this.resolution = resolution;
        this.dict = dict;
     
        startLocation = new Vector2(resolution.X / 2, resolution.Y / 6);
        highLocation = new Vector2(resolution.X / 2, resolution.Y * 1 / 3);
        gridLocation = new Vector2(25, resolution.Y * 9/10);

        startBounds = Engine.DrawString("HIGH SCORES", startLocation, Theme.getColor(), buttonFont, TextAlignment.Center);
        highBounds = Engine.DrawString("IDK", highLocation, Theme.getColor(), buttonFont, TextAlignment.Center);

    }
    public void refresh(Dictionary<String, String> dict)
    {
        // refresh the dict
        this.dict = dict;
        
    }
    /// <summary>
    /// Draws the entry screen and handles changing the theme
    /// </summary>
    public void draw()
    {
        Theme.drawGameBackground();

        // draws buttons
        Engine.DrawString("HIGH SCORES:", startLocation, Theme.getColor(), buttonFont, TextAlignment.Center);
        //Engine.DrawString("IDK", highLocation, color, buttonFont, TextAlignment.Center);
        float y = resolution.Y * 1 / 4;
        foreach (KeyValuePair<string, string> entry in dict)
        {
            // do something with entry.Value or entry.Key
            String value = entry.Value;
            String place = entry.Key; // 1, 2, 3, 4...
            String score = value.Split(";")[0];
            String name = value.Split(";")[1];
            //String date = timestamp_to_string(value.Split(";")[2]);
            String date = timestamp.convert(value.Split(";")[2]);
            // draw the string
            String drawThis = place + ") " + score + " | " + date + " | " + name;
            //System.Diagnostics.Debug.WriteLine(drawThis);
            Engine.DrawString(drawThis, new Vector2(resolution.X/2, y), Theme.getColor(), buttonFont, TextAlignment.Center);
            y = y + 50;
            //break;
            //highLocation.Y = highLocation.Y + (resolution.Y * 1 / 6);
        }
        // draws button for changing grid layout
        gridBounds = Engine.DrawString("BACK [CON-X]", gridLocation, Theme.getColor(), buttonFont);
        //if (isGridClicked())
        //{
          //  theme.drawStartBackground();
        //}




        drawHoverRect(Theme.getColor());


    }




    public Boolean isGridClicked()
    {
        return (Engine.GetMouseButtonDown(MouseButton.Left) && gridBounds.Contains(Engine.MousePosition)) || SDL.SDL_GameControllerGetButton(Game.controller, Game.controller_bindings["HighScore_Back"]) > 0;
    }


    /// <summary>
    /// Draws a rectangle around buttons if the mouse is hovered over
    /// </summary>
    private void drawHoverRect(Color color)
    {
        drawHoverRect(startBounds, Theme.getColor());
        drawHoverRect(highBounds, Theme.getColor());
        drawHoverRect(gridBounds, Theme.getColor());
    }

    /// <summary>
    /// Draws a rectangle if the mouse is hovered over a button
    /// </summary>
    /// <param name="bounds"> the bounds of the button</param>
    private void drawHoverRect(Bounds2 bounds, Color color)
    {
        if (bounds.Contains(Engine.MousePosition))
        {
            Bounds2 rectBounds = new Bounds2(bounds.Position.X-5, bounds.Position.Y+5, bounds.Size.X+10, bounds.Size.Y-10);
            Engine.DrawRectEmpty(rectBounds, Theme.getColor());
        }
    }



    
}
