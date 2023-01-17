/*using System;

using System.Collections.Generic;

using System.Text;

public class highscorescreen
{
    Vector2 resolution;
    Font buttonFont = Engine.LoadFont("Oswald-Regular.ttf", 48);
    Vector2 titleLocation;
    Vector2 highLocation;
    Vector2 backLocation;


    Bounds2 startBounds;
    Bounds2 highBounds;
    Bounds2 backBounds;
    Bounds2 darkModeBounds;

    Theme theme;
    Color color;

    float degrees;

    Dictionary<String, String> board;

    public highscorescreen(Dictionary<String, String> board)
	{
        this.resolution = resolution;
        this.theme = theme;
        color = theme.getColor();
        titleLocation = new Vector2(resolution.X / 2, resolution.Y / 4);
        highLocation = new Vector2(resolution.X / 2, resolution.Y * 1 / 3); //+ y * 1/5

        this.board = board;

        degrees = 0;


    }
    public String timestamp_to_string(String timestamp)
    {
        Double ts;
        if (Double.TryParse(timestamp, out ts) == true)
        {
            System.DateTime dat_Time = new System.DateTime(1965, 1, 1, 0, 0, 0, 0);
            dat_Time = dat_Time.AddSeconds(ts);
            string print_the_Date = dat_Time.ToShortDateString() + " " + dat_Time.ToShortTimeString();
            return print_the_Date;
        }
        return "EMPTY";
    }
    /// <summary>
    /// Draws the entry screen and handles changing the theme
    /// </summary>
    public void draw()
    {
        theme.drawGameBackground();

        // draws buttons
        Engine.DrawString("START", titleLocation, color, buttonFont, TextAlignment.Center);

        foreach (KeyValuePair<string, string> entry in board)
        {
            // do something with entry.Value or entry.Key
            String value = entry.Value;
            String place = entry.Key; // 1, 2, 3, 4...
            String score = value.Split(";")[0];
            String name = value.Split(";")[1];
            String date = timestamp_to_string(value.Split(";")[2]);
            // 1) score | date | name
            String scoreLine = place + ") " + score + date + name;
            Engine.DrawString(scoreLine, highLocation, color, buttonFont, TextAlignment.Center);
            highLocation = new Vector2(resolution.X / 2, highLocation.Y + (resolution.Y * 1 / 3));
            

        }
        backBounds = Engine.DrawString("BACK", backLocation, color, buttonFont);

        if (isBackClicked())
        {
            theme.changeGridLayout(); //change to go back to entry screen
        }



        drawHoverRect(color);


    }




    public Boolean isBackClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && backBounds.Contains(Engine.MousePosition);
    }

    /// <summary>
    /// Draws a rectangle around buttons if the mouse is hovered over
    /// </summary>
    private void drawHoverRect(Color color)
    {
        drawHoverRect(backBounds, color);
    }

    /// <summary>
    /// Draws a rectangle if the mouse is hovered over a button
    /// </summary>
    /// <param name="bounds"> the bounds of the button</param>
    private void drawHoverRect(Bounds2 bounds, Color color)
    {
        if (bounds.Contains(Engine.MousePosition))
        {
            Bounds2 rectBounds = new Bounds2(bounds.Position.X - 5, bounds.Position.Y + 5, bounds.Size.X + 10, bounds.Size.Y - 10);
            Engine.DrawRectEmpty(rectBounds, color);
        }
    }
}
*/




using System;
using System.Collections.Generic;
using System.Text;

class highscorescreen
{
    Dictionary<String, String> dict;
    Vector2 resolution;
    Font buttonFont = Engine.LoadFont("Oswald-Regular.ttf", 48);
    Vector2 startLocation;
    Vector2 highLocation;
    Vector2 gridLocation;

    
    Bounds2 startBounds;
    Bounds2 highBounds;
    Bounds2 gridBounds;

    Theme theme;
    Color color;

    float degrees;

    public highscorescreen(Vector2 resolution, Theme theme, Dictionary<String, String> dict)
    {
        this.resolution = resolution;
        this.theme = theme;
        this.dict = dict;
        color = theme.getColor();
        startLocation = new Vector2(resolution.X / 2, resolution.Y / 6);
        highLocation = new Vector2(resolution.X / 2, resolution.Y * 1 / 3);
        gridLocation = new Vector2(0, resolution.Y * 10/11);

        startBounds = Engine.DrawString("HIGH SCORES", startLocation, color, buttonFont, TextAlignment.Center);
        highBounds = Engine.DrawString("IDK", highLocation, color, buttonFont, TextAlignment.Center);

        degrees = 0;
    }

    /// <summary>
    /// Draws the entry screen and handles changing the theme
    /// </summary>
    public void draw()
    {
        theme.drawGameBackground();

        // draws buttons
        Engine.DrawString("HIGH SCORES:", startLocation, color, buttonFont, TextAlignment.Center);
        //Engine.DrawString("IDK", highLocation, color, buttonFont, TextAlignment.Center);
        float y = resolution.Y * 1 / 3;
        foreach (KeyValuePair<string, string> entry in dict)
        {
            // do something with entry.Value or entry.Key
            String value = entry.Value;
            String place = entry.Key; // 1, 2, 3, 4...
            String score = value.Split(";")[0];
            String name = value.Split(";")[1];
            String date = timestamp_to_string(value.Split(";")[2]);
            // draw the string
            String drawThis = place + ") " + score + " | " + date + " | " + name;
            //System.Diagnostics.Debug.WriteLine(drawThis);
            Engine.DrawString(drawThis, new Vector2(resolution.X/2, y), color, buttonFont, TextAlignment.Center);
            y = y + 50;
            //break;
            //highLocation.Y = highLocation.Y + (resolution.Y * 1 / 6);
        }
        // draws button for changing grid layout
        gridBounds = Engine.DrawString("BACK", gridLocation, color, buttonFont);
        //if (isGridClicked())
        //{
          //  theme.drawStartBackground();
        //}




        drawHoverRect(color);


    }

    public String timestamp_to_string(String timestamp)
    {
        Double ts;
        if (Double.TryParse(timestamp, out ts) == true)
        {
            System.DateTime dat_Time = new System.DateTime(1965, 1, 1, 0, 0, 0, 0);
            dat_Time = dat_Time.AddSeconds(ts);
            string print_the_Date = dat_Time.ToShortDateString() + " " + dat_Time.ToShortTimeString();
            return print_the_Date;
        }
        return "EMPTY";
    }


    public Boolean isHighScoreClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && highBounds.Contains(Engine.MousePosition);
    }

    public Boolean isGridClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && gridBounds.Contains(Engine.MousePosition);
    }


    /// <summary>
    /// Draws a rectangle around buttons if the mouse is hovered over
    /// </summary>
    private void drawHoverRect(Color color)
    {
        drawHoverRect(startBounds, color);
        drawHoverRect(highBounds, color);
        drawHoverRect(gridBounds, color);
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
            Engine.DrawRectEmpty(rectBounds, color);
        }
    }



    
}
