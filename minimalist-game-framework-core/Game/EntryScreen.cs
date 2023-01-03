﻿using System;
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
    Bounds2 startBounds;
    Bounds2 highBounds;

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

        themeMenu = false;
    }

    /// <summary>
    /// Draws the entry screen and handles changing the theme
    /// </summary>
    public void draw()
    {
        Theme currentTheme = themes[Theme.current];

        //currentTheme.drawBackground();
        //currentTheme.drawTitle(titleLocation);

        Engine.DrawString("Asteroids", titleLocation, Color.White, titleFont, TextAlignment.Center);
        startBounds = Engine.DrawString("START", startLocation, Color.White, buttonFont, TextAlignment.Center);
        highBounds = Engine.DrawString("HIGH SCORE", highLocation, Color.White, buttonFont, TextAlignment.Center);
        themeMenuBounds = Engine.DrawString("THEME", Vector2.Zero, Color.White, buttonFont);

        drawHoverRect();


        // temp
        Engine.DrawString("Current Theme: " + (Theme.current+1), new Vector2(resolution.X, 0), Color.White, buttonFont, TextAlignment.Right);
       
        // show theme menu if it is clicked on
        if (isThemeMenuClicked())
        {
            themeMenu = true;
        }

        if (themeMenu)
        {
            drawThemeMenu();

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

    /// <summary>
    /// Draws a rectangle around buttons if the mouse is hovered over
    /// </summary>
    private void drawHoverRect()
    {
        drawHoverRect(startBounds);
        drawHoverRect(highBounds);
        drawHoverRect(themeMenuBounds);
    }

    /// <summary>
    /// Draws a rectangle if the mouse is hovered over a button
    /// </summary>
    /// <param name="bounds"> the bounds of the button</param>
    private void drawHoverRect(Bounds2 bounds)
    {
        if (bounds.Contains(Engine.MousePosition))
        {
            Bounds2 rectBounds = new Bounds2(bounds.Position.X-5, bounds.Position.Y+5, bounds.Size.X+10, bounds.Size.Y-10);
            Engine.DrawRectEmpty(rectBounds, Color.White);
        }
    }

    public void drawThemeMenu()
    {
        for(int i=0; i<themes.Count; i++)
        {
            Theme t = themes[i];
            t.setBounds(t.drawThemeIcon(new Vector2(0,70*(i+1))));
            drawHoverRect(t.getBounds());
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
