using System;
using System.Collections.Generic;
using System.Text;
class Theme
{
    static List<Theme> list;
    static Font font = Engine.LoadFont("Oswald-Regular.ttf", 48);
    static int count = 0; // total number of themes
    public static int current = 0; // the current theme applied
    public static Boolean drawGrid = false;

    private int num;  
    private Bounds2 b; // bounds of the text in menu

    private String name;
    private Texture title;
    private Texture backgroundNoGrid;
    private Texture backgroundGrid;
    private Texture rocketShip;
    private Texture asteroid;
    private Color color;

    public Theme() // only for testing
    {
        num = count;
        count++;
    }

    /// <summary>
    /// Creates a theme object with the art
    /// </summary>
    /// <param name="title"> the path of the title</param>
    /// <param name="background"> the path of the background</param>
    /// <param name="rocketShip"> the path of the rocket ship</param>
    /// <param name="asteroid"> the path of the asteroid</param>
    /// <param name="color"> the color of the text, preferably high contrast to background color</param>
    public Theme(String name,  String title, String backgroundNoGrid, String backgroundGrid, String rocketShip, String asteroid, Color color)
    {
        num = count;
        this.name = name;
        this.title = Engine.LoadTexture(title);
        this.backgroundNoGrid = Engine.LoadTexture(backgroundNoGrid);
        this.backgroundGrid = Engine.LoadTexture(backgroundGrid);
        this.rocketShip = Engine.LoadTexture(rocketShip);
        this.asteroid = Engine.LoadTexture(asteroid);
        this.color = color;
        count++;
    }

    public static void setThemes(List<Theme> list)
    {
        Theme.list = list;
    }

    
    /// <summary>
    /// The method draws the option for the menu choosing the theme
    /// </summary>
    /// <param name="position"> the position of the text</param>
    /// <returns></returns>
    public Bounds2 drawThemeIcon(Vector2 position, Color currentColor)
    {
        return Engine.DrawString(name, position, currentColor, font);
    }


    public int getThemeNum()
    {
        return num;
    }

    public void setBounds(Bounds2 b)
    {
        this.b = b;
    }

    public Bounds2 getBounds()
    {
        return b;
    }

    /// <summary>
    /// draws the title ASTEROIDS
    /// </summary>
    /// <param name="position"> the center of the title</param>
    public void drawTitle(Vector2 position)
    {
        Engine.DrawTexture(title, new Vector2(position.X-title.Width/2, position.Y));
    }

    // game background
    // end screen
    public static void drawCurrentBackground()
    {
        list[current].drawBackground();
    }
    private void drawBackground()
    {
        if (drawGrid)
        {
            Engine.DrawTexture(backgroundGrid, Vector2.Zero);
        }
        else
        {
            Engine.DrawTexture(backgroundNoGrid, Vector2.Zero);
        }
    }

    /// <summary>
    /// draws a rocket ship
    /// </summary>
    /// <param name="position"> the upper left hand corner of the rocket ship</param>
    /// <param name="rotationDegrees"> degrees of rotation clockwise</param>
    public static void drawCurrentRocketShip(Vector2 position, float rotationDegrees = 0)
    {
        list[current].drawRocketShip(position, rotationDegrees);
    }
    private void drawRocketShip(Vector2 position, float rotationDegrees = 0)
    {
        Engine.DrawTexture(rocketShip, 
            new Vector2(position.X, position.Y), 
            null, null, rotationDegrees);
    }

    /// <summary>
    /// Draws an asteroid
    /// </summary>
    /// <param name="position"> the upper left hand corner of the asteroid</param>
    /// <param name="radius"> the radius of the asteroid</param>
    /// <param name="rotationDegrees"> degrees of rotation</param>
    public static void drawCurrentAsteroid(Vector2 position, int radius, float rotationDegrees = 0)
    {
        list[current].drawAsteroid(position, radius, rotationDegrees);
    }
    private void drawAsteroid(Vector2 position, int radius, float rotationDegrees)
    {
        Engine.DrawTexture(asteroid, 
            new Vector2(position.X, position.Y), 
            null, new Vector2(radius, radius), rotationDegrees);
    }
    
    public static Color getCurrentColor()
    {
        return list[current].color;
    }

}

