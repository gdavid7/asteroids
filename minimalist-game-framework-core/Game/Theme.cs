using System;
using System.Collections.Generic;
using System.Text;
class Theme
{
    static Font font = Engine.LoadFont("Oswald-Regular.ttf", 36);
    static int count = 0; // total number of themes
    public static int current = 0; // the current theme applied
    private int num;  
    private Bounds2 b; // bounds of the text in menu

    private Texture title;
    private Texture background;
    private Texture rocketShip;
    private Texture asteroid;
    private Color textColor;

    public Theme() // only for testing
    {
        num = count;
        //title = Engine.LoadTexture("title.PNG");
        //rocketShip = Engine.LoadTexture("rocketship.jfif");
        //asteroid = Engine.LoadTexture("asteroid.jfif");
        count++;
    }
    
    /// <summary>
    /// Creates a theme object with the art
    /// </summary>
    /// <param name="title"> the path of the title</param>
    /// <param name="background"> the path of the background</param>
    /// <param name="rocketShip"> the path of the rocket ship</param>
    /// <param name="asteroid"> the path of the asteroid</param>
    /// <param name="textColor"> the color of the text, preferably high contrast to background color</param>
    public Theme(String title, String background, String rocketShip, String asteroid, Color textColor)
    {
        num = count;
        this.title = Engine.LoadTexture(title);
        this.background = Engine.LoadTexture(background);
        this.rocketShip = Engine.LoadTexture(rocketShip);
        this.asteroid = Engine.LoadTexture(asteroid);
        this.textColor = textColor;
        count++;
    }

    /// <summary>
    /// The method draws the option for the menu choosing the theme
    /// </summary>
    /// <param name="position"> the position of the text</param>
    /// <returns></returns>
    public Bounds2 drawThemeIcon(Vector2 position)
    {
        return Engine.DrawString("Theme " + num, position, Color.White, font);
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
    public void drawBackground()
    {
        Engine.DrawTexture(background, Vector2.Zero);
    }

    /// <summary>
    /// draws a rocket ship
    /// </summary>
    /// <param name="position"> the upper left hand corner of the rocket ship</param>
    /// <param name="rotationDegrees"> degrees of rotation clockwise</param>
    public void drawRocketShip(Vector2 position, float rotationDegrees = 0)
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
    public void drawAsteroid(Vector2 position, int radius, float rotationDegrees = 0)
    {
        Engine.DrawTexture(asteroid, 
            new Vector2(position.X, position.Y), 
            null, new Vector2(radius, radius), rotationDegrees);
    }

    public Color getTextColor()
    {
        return textColor;
    }
}

