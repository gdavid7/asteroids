using System;
using System.Collections.Generic;
using System.Text;

class EntryScreen
{
    Vector2 resolution;
    Font buttonFont = Engine.LoadFont("Oswald-Regular.ttf", 48);
    Vector2 startLocation;
    Vector2 highLocation;
    Vector2 gridLocation;
    Vector2 darkModeLocation;
    
    Bounds2 startBounds;
    Bounds2 highBounds;
    Bounds2 gridBounds;
    Bounds2 darkModeBounds;

    Color color;

    float degrees;

    public EntryScreen(Vector2 resolution)
    {
        this.resolution = resolution;
        color = Theme.getColor();
        startLocation = new Vector2(resolution.X / 2, resolution.Y * 11 / 21);
        highLocation = new Vector2(resolution.X / 2, resolution.Y * 2 / 3);
        gridLocation = new Vector2(25, resolution.Y * 9/10);
        darkModeLocation = new Vector2(20, 17);

        startBounds = Engine.DrawString("START", startLocation, color, buttonFont, TextAlignment.Center);
        highBounds = Engine.DrawString("HIGH SCORE", highLocation, color, buttonFont, TextAlignment.Center);

        degrees = 0;
    }

    /// <summary>
    /// Draws the entry screen and handles changing the theme
    /// </summary>
    public void draw()
    {
        Theme.drawStartBackground();

        // draws buttons
        Engine.DrawString("START", startLocation, color, buttonFont, TextAlignment.Center);
        Engine.DrawString("HIGH SCORE", highLocation, color, buttonFont, TextAlignment.Center);
        
        // draws button for changing grid layout
        if (Theme.isGridOn())
        {
            gridBounds = Engine.DrawString("GRID: ON", gridLocation, color, buttonFont);
        }
        else
        {
            gridBounds = Engine.DrawString("GRID: OFF", gridLocation, color, buttonFont);
        }
        if (isGridClicked())
        {
            Theme.changeGridLayout();
        }
        
        // draws button for changing color mode
        if (Theme.isDarkMode())
        {
            darkModeBounds = Engine.DrawString("Dark Mode", darkModeLocation, color, buttonFont);
        }
        else
        {
            darkModeBounds = Engine.DrawString("Light Mode", darkModeLocation, color, buttonFont);
        }
        if (isDarkModeClicked())
        {
            color = Theme.changeColorMode();
        }

        
        drawHoverRect(color);


    }

    public Boolean isStartClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && startBounds.Contains(Engine.MousePosition);
    }

    public Boolean isHighScoreClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && highBounds.Contains(Engine.MousePosition);
    }

    public Boolean isGridClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && gridBounds.Contains(Engine.MousePosition);
    }

    public Boolean isDarkModeClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && darkModeBounds.Contains(Engine.MousePosition);
    }
    /// <summary>
    /// Draws a rectangle around buttons if the mouse is hovered over
    /// </summary>
    private void drawHoverRect(Color color)
    {
        drawHoverRect(startBounds, color);
        drawHoverRect(highBounds, color);
        drawHoverRect(gridBounds, color);
        drawHoverRect(darkModeBounds, color);
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
