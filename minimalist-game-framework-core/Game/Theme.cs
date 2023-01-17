using System;
using System.Collections.Generic;
using System.Text;
class Theme
{
    public static int darkMode = 0; // 0 = dark, 1 = light
    public static int gridOn = 0; // 0 = no grid, 2 = grid
    // 0 = dark no grid, 1 = light no grid, 2 = dark grid, 3 = light grid


    static Vector2 resolution; 

    public static List<Texture> startBackgrounds;
    static List<Texture> gameBackgrounds;
    static List<Texture> endBackgrounds;
    static List<Texture> rocketShips;
    static List<List<Texture>> asteroids;
    static List<Texture> powerups;
    static List<Color> colors = new List<Color>() {Color.White, Color.Black};

    static Font f = Engine.LoadFont("Starjedi.ttf", 48);




    /// <summary>
    /// Create a theme object
    /// </summary>
    /// <param name="resolution"> the resolution of the screen</param>
    /// <param name="startBackgrounds"> a list of filenames for entry screen backgrounds, order D, L, DG, LG</param>
    /// <param name="gameBackgrounds"> a list of filenames for backgrounds during gameplay, order D, L, DG, LG</param>
    /// <param name="endBackgrounds"> a list of filenames for end screen backgrounds, order D, L, DG, LG</param>
    /// <param name="rocketShips"> a list of filenames for rocketships, order D, L</param>
    /// <param name="asteroids"> a list of filenames for asteroids, order D, L</param>
    /// <param name="powerups"> a list of filenames for powerups</param>
    public static void setUp(Vector2 resolutionT, List<String> startBackgroundsT, List<String> gameBackgroundsT, List<String> endBackgroundsT, List<String> rocketShipsT, List<List<String>> asteroidsT, List<String> powerupsT)
    {
        resolution = resolutionT;

        startBackgrounds = loadTextures(startBackgroundsT);
        gameBackgrounds = loadTextures(gameBackgroundsT);
        endBackgrounds = loadTextures(endBackgroundsT);
        rocketShips = loadTextures(rocketShipsT);
        asteroids = new List<List<Texture>>() { loadTextures(asteroidsT[0]), loadTextures(asteroidsT[1]) };
        powerups = loadTextures(powerupsT);
    }

    /// <summary>
    /// Load every texture in a list of filenames
    /// </summary>
    /// <param name="files"> a list of filenames </param>
    /// <returns> a list of loaded textures</returns>
    private static List<Texture> loadTextures(List<String> files)
    {
        List<Texture> textures = new List<Texture>();
        foreach(String file in files)
        {
            textures.Add(Engine.LoadTexture(file));
        }

        return textures;

    }
    /// <summary>
    /// draws the entry screen background
    /// </summary>
    public static void drawStartBackground()
    {
        drawBackground(startBackgrounds);
    }

    /// <summary>
    /// draws the background during gameplay
    /// </summary>
    public static void drawGameBackground()
    {
        drawBackground(gameBackgrounds);
    }

    /// <summary>
    /// draws the end screen background
    /// </summary>
    public static void drawEndBackground()
    {
        Engine.DrawTexture(endBackgrounds[darkMode], Vector2.Zero, null, resolution);
    }

    /// <summary>
    /// draws background based on the current color mode and grid layout
    /// </summary>
    /// <param name="backgrounds"> a list of filenames for backgrounds to be chosen from</param>
    private static  void drawBackground(List<Texture> backgrounds)
    {
        Engine.DrawTexture(backgrounds[darkMode + gridOn], Vector2.Zero, null, resolution);
    }

    /// <summary>
    /// draws a rocket ship
    /// </summary>
    /// <param name="position"> the upper left hand corner of the rocket ship</param>
    /// <param name="radius"> the radius of the rocketship</param>
    /// <param name="rotationDegrees"> degrees of rotation clockwise</param>
    public static void drawRocketShip(Vector2 position, int radius, float rotationDegrees = 0)
    {
        Engine.DrawTexture(rocketShips[darkMode], position, null, new Vector2(radius, radius), rotationDegrees);
    }


    /// <summary>
    /// Draws an asteroid
    /// </summary>
    /// <param name="position"> the upper left hand corner of the asteroid</param>
    /// <param name="radius"> the radius of the asteroid</param>
    /// <param name="rotationDegrees"> degrees of rotation</param>
    public static void drawAsteroid(Vector2 position, int num, Vector2 size, float rotationDegrees = 0)
    {
        Engine.DrawTexture(asteroids[darkMode][num], position, null, size, rotationDegrees);
    }

    /// <summary>
    /// Draws a powerup
    /// </summary>
    /// <param name="position"> the uper left hand corner of the powerup</param>
    /// <param name="radius"> the radius of the powerup</param>
    /// <param name="rotationDegrees"> degrees of rotation</param>
    public static void drawPowerup(Vector2 position, int radius, float rotationDegrees = 0)
    {
        Engine.DrawTexture(powerups[darkMode], position, null, new Vector2(radius, radius), rotationDegrees);
    }

    
    public static Color getColor()
    {
        return colors[darkMode];
    }

    public static Boolean isGridOn()
    {
        return gridOn == 2;
    }

    public static Boolean isDarkMode()
    {
        return darkMode == 0;
    }

    /// <summary>
    /// Switches the current grid layout (grid/no grid)
    /// </summary>
    public static void changeGridLayout()
    {
        switch (gridOn)
        {
            case 0:
                gridOn = 2;
                break;
            case 2:
                gridOn = 0;
                break;
        }
        
    }
    /// <summary>
    /// Changes the current color mode (dark/light)
    /// </summary>
    /// <returns> the new Color after changing</returns>
    public static Color changeColorMode()
    {
        darkMode = (darkMode + 1) % 2;
        return getColor();
    }
}

