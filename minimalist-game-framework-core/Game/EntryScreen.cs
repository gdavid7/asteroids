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
    Vector2 highLocation;
    Vector2 gridLocation;
    Bounds2 startBounds;
    Bounds2 highBounds;
    Bounds2 gridBounds;

    Bounds2 themeMenuBounds;
    Boolean themeMenu; // whether to show the theme menu or not

    List<Theme> themes = new List<Theme>();

    public EntryScreen(Vector2 resolution, List<Theme> themes)
    {
        this.resolution = resolution;
        this.themes = themes;
        titleFont = Engine.LoadFont("Starjedi.ttf", 128);
        titleLocation = new Vector2(resolution.X / 2, resolution.Y / 5);

        buttonFont = Engine.LoadFont("Oswald-Regular.ttf", 48);
        startLocation = new Vector2(resolution.X / 2, resolution.Y / 2);
        highLocation = new Vector2(resolution.X / 2, resolution.Y * 2 / 3);
        gridLocation = new Vector2(0, resolution.Y * 10/11);

        themeMenu = false;
    }

    /// <summary>
    /// Draws the entry screen and handles changing the theme
    /// </summary>
    public void draw()
    {
        Color currentColor = Theme.getCurrentColor();

        Theme.drawCurrentBackground();
        //Engine.DrawRectSolid(new Bounds2(Vector2.Zero, resolution), Color.Black) ;
        //currentTheme.drawTitle(titleLocation);

        Engine.DrawString("Asteroids", titleLocation, currentColor, titleFont, TextAlignment.Center);
        startBounds = Engine.DrawString("START", startLocation, currentColor, buttonFont, TextAlignment.Center);
        highBounds = Engine.DrawString("HIGH SCORE", highLocation, currentColor, buttonFont, TextAlignment.Center);
        themeMenuBounds = Engine.DrawString("THEME", Vector2.Zero, currentColor, buttonFont);
        if (Theme.drawGrid)
        {
            gridBounds = Engine.DrawString("GRID: ON", gridLocation, currentColor, buttonFont);
        }
        else
        {
            gridBounds = Engine.DrawString("GRID: OFF", gridLocation, currentColor, buttonFont);
        }

        Theme.drawCurrentAsteroid(Vector2.Zero, 250);
        Theme.drawCurrentRocketShip(Vector2.Zero);

        drawHoverRect(currentColor);

        if (isGridClicked())
        {
            Theme.drawGrid = !Theme.drawGrid;
        }
       
        // show theme menu if it is clicked on
        if (isThemeMenuClicked())
        {
            themeMenu = true;
        }

        if (themeMenu)
        {
            drawThemeMenu(currentColor);

            int action = isThemeOptionsClicked();
            if (action == -1) // outside is clicked
            {
                themeMenu = false;
            } else if (action >= 0) // one of the options is clicked
            {
                themeMenu = false;
                Theme.current = action;
            }
        }

    }

    public Boolean isStartClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && startBounds.Contains(Engine.MousePosition);
    }

    public Boolean isHighScoreClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && highBounds.Contains(Engine.MousePosition);
    }

    public Boolean isThemeMenuClicked()
    {
        return Engine.GetMouseButtonDown(MouseButton.Left) && themeMenuBounds.Contains(Engine.MousePosition);
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
        drawHoverRect(themeMenuBounds, color);
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

    public void drawThemeMenu(Color color)
    {
        for(int i=0; i<themes.Count; i++)
        {
            Theme t = themes[i];
            t.setBounds(t.drawThemeIcon(new Vector2(0,70*(i+1)), color));
            drawHoverRect(t.getBounds(), color);
        }
    }

    public int isThemeOptionsClicked()
    {
        if (Engine.GetMouseButtonDown(MouseButton.Left))
        {
            if (themeMenuBounds.Contains(Engine.MousePosition))
            {
                return -2; // if theme menu is clicked, acts as if no clicking happened
                // prevents immediately closing the menu once opened
            }
            for (int i=0; i<themes.Count; i++)
            {
                if (themes[i].getBounds().Contains(Engine.MousePosition))
                {
                    return i; // one of the themes is clicked
                }
            }

            return -1; // outside is clicked
        }

        return -2; // no clicking
    }

    
}
