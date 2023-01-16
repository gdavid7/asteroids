using System;
using System.Collections.Generic;
using System.Text;
class Theme
{
    public int darkMode = 0; // 0 = dark, 1 = light
    public int gridOn = 0; // 0 = no grid, 2 = grid
    // 0 = dark no grid, 1 = light no grid, 2 = dark grid, 3 = light grid


    private Vector2 resolution; 

    private List<Texture> startBackgrounds;
    private List<Texture> gameBackgrounds;
    private List<Texture> endBackgrounds;
    private List<Texture> rocketShips;
    private List<Texture> asteroids;
    private List<Texture> powerups;
    private List<Color> colors = new List<Color>() {Color.White, Color.Black};


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
    public Theme(Vector2 resolution, List<String> startBackgrounds, List<String> gameBackgrounds, List<String> endBackgrounds, List<String> rocketShips, List<String> asteroids, List<String> powerups)
    {
        this.resolution = resolution;

        this.startBackgrounds = loadTextures(startBackgrounds);
        this.gameBackgrounds = loadTextures(gameBackgrounds);
        this.endBackgrounds = loadTextures(endBackgrounds);
        this.rocketShips = loadTextures(rocketShips);
        this.asteroids = loadTextures(asteroids);
        this.powerups = loadTextures(powerups);
    }

    /// <summary>
    /// Load every texture in a list of filenames
    /// </summary>
    /// <param name="files"> a list of filenames </param>
    /// <returns> a list of loaded textures</returns>
    private List<Texture> loadTextures(List<String> files)
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
    public void drawStartBackground()
    {
        drawBackground(startBackgrounds);
    }

    /// <summary>
    /// draws the background during gameplay
    /// </summary>
    public void drawGameBackground()
    {
        drawBackground(gameBackgrounds);
    }

    /// <summary>
    /// draws the end screen background
    /// </summary>
    public void drawEndBackground()
    {
        Engine.DrawTexture(endBackgrounds[darkMode], Vector2.Zero, null, resolution);
    }

    /// <summary>
    /// draws background based on the current color mode and grid layout
    /// </summary>
    /// <param name="backgrounds"> a list of filenames for backgrounds to be chosen from</param>
    private void drawBackground(List<Texture> backgrounds)
    {
        Engine.DrawTexture(backgrounds[darkMode + gridOn], Vector2.Zero, null, resolution);
    }

    /// <summary>
    /// draws a rocket ship
    /// </summary>
    /// <param name="position"> the upper left hand corner of the rocket ship</param>
    /// <param name="radius"> the radius of the rocketship</param>
    /// <param name="rotationDegrees"> degrees of rotation clockwise</param>
    public void drawRocketShip(Vector2 position, int radius, float rotationDegrees = 0)
    {
        Engine.DrawTexture(rocketShips[darkMode], position, null, new Vector2(radius, radius), rotationDegrees);
    }


    /// <summary>
    /// Draws an asteroid
    /// </summary>
    /// <param name="position"> the upper left hand corner of the asteroid</param>
    /// <param name="radius"> the radius of the asteroid</param>
    /// <param name="rotationDegrees"> degrees of rotation</param>
    public void drawAsteroid(Vector2 position, int radius, float rotationDegrees = 0)
    {
        Engine.DrawTexture(asteroids[darkMode], position, null, new Vector2(radius, radius), rotationDegrees);
    }

    /// <summary>
    /// Draws a powerup
    /// </summary>
    /// <param name="position"> the uper left hand corner of the powerup</param>
    /// <param name="radius"> the radius of the powerup</param>
    /// <param name="rotationDegrees"> degrees of rotation</param>
    public void drawPowerup(Vector2 position, int radius, float rotationDegrees = 0)
    {
        Engine.DrawTexture(powerups[darkMode], position, null, new Vector2(radius, radius), rotationDegrees);
    }

    
    public Color getColor()
    {
        return colors[darkMode];
    }

    public Boolean isGridOn()
    {
        return gridOn == 2;
    }

    public Boolean isDarkMode()
    {
        return darkMode == 0;
    }

    /// <summary>
    /// Switches the current grid layout (grid/no grid)
    /// </summary>
    public void changeGridLayout()
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
    public Color changeColorMode()
    {
        darkMode = (darkMode + 1) % 2;
        return getColor();
    }
}

