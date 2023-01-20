﻿using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using SDL2;

class Game
{
    public static readonly string Title = "DRAW-STROY";
    public static readonly Vector2 Resolution = new Vector2(1280, 720);

    scoreboard s = new scoreboard();
    inputText i = new inputText();
    Theme theme;
    EntryScreen es;
    highscorescreen hs;
    public static IntPtr controller;

    public static Dictionary<String, Key> keyboard_bindings = new Dictionary<String, Key>();
    public static Dictionary<String,SDL.SDL_GameControllerButton> controller_bindings = new Dictionary<String, SDL.SDL_GameControllerButton>();



    Texture shot = Engine.LoadTexture("projectile.png");

    float time = 0;
    float asteroidTime = 0;
    float invinTime = 0;
    float powerupTime = 0;
        
    //ship vars
    float rot = 180;
    Vector2 mov = new Vector2(100, 100);
    float inertia = 100;
    bool fly = false;
    Bounds2 shipBounds = new Bounds2(100, 100, 100, 100);
    int lives = 3;
    bool invincible = false;



    //shot variables
    Vector2 smov = new Vector2(400, 400);
    bool shoot = false;
    float rotLock = 0;
    Bounds2 shotBounds = new Bounds2(400,400, 100, 100);

    //powerup vars
    
    //asteroid inits
    Asteroid a = new Asteroid( new Vector2(600, 600),100,new Vector2(100,100),1);
    Asteroid b = new Asteroid(new Vector2(400, 800), 60, new Vector2(100,100),1);

    
    //powerup vars
    bool powerUp1Engaged = false;
    bool powerUp2Engaged = false;
    bool powerUp3Engaged = false;
    float delay = 10;
    Vector2 pPos = new Vector2(300, 300);
    Bounds2 pBounds = new Bounds2(300,300,100,100);
    bool pVis = true;

    //additional constants
    public double shotCoolDownTime = 0.3;
    public float shotBoundSizeFactor = 10;
    public float powerUpCounter = 0;

    //game vars
    bool spawnAst = true;
    bool entry = true;
    bool highScore = false;
    bool end = false;
    int score = 0;
    int spawnDelay = 5;

    Random rd = new Random();

    //font & location for powerup ui notifs
    Font buttonFont = Engine.LoadFont("Oswald-Regular.ttf", 20);
    Vector2 location = new Vector2(10, Resolution.Y* 10/11);
    
    //score vars
    bool scoreDisplay = false;
    String name = "";
    bool leaderboardRefreshed = false;
    Dictionary<String, String> userScores = null;

    public Game()
    {
        List<String> startBackgrounds = new List<String>() { "startBackgroundD.png", "startBackgroundL.png", "startBackgroundDG.png", "startBackgroundLG.png" };
        List<String> gameBackgrounds = new List<String>() { "gameBackgroundD.png", "gameBackgroundL.png", "gameBackgroundDG.png", "gameBackgroundLG.png" };
        List<String> endBackgrounds = new List<String>() { "endBackgroundD.png", "endBackgroundL.png" };
        List<String> rocketShips = new List<String>() { "rocketshipD.png", "rocketshipL.png" };
        List<String> asteroidsD = new List<String>() { "asteroidD1.png", "asteroidD2.png", "asteroidD3.png", "asteroidD4.png" };
        List<String> asteroidsL = new List<String>() { "asteroidL1.png", "asteroidL2.png", "asteroidL3.png", "asteroidL4.png" };
        List<List<String>> asteroids = new List<List<String>>() {asteroidsD, asteroidsL};
        List<String> powerups = new List<String>() { "powerupD.png", "powerupL.png"};
        List<String> projectiles = new List<String>() { "projectileD.png", "projectileL.png" }; 

       Theme.setUp(Resolution, startBackgrounds, gameBackgrounds, endBackgrounds, rocketShips, asteroids, powerups, projectiles);



        // CONTROLLER & KEYBOARD BINDINGS: EDIT TO CUSTOMIZE AND CONFIGURE, ALSO EDIT TEXTS


       controller = SDL.SDL_GameControllerOpen(0);


        keyboard_bindings["Shoot"] = Key.Space;
        keyboard_bindings["Left"] = Key.Left;
        keyboard_bindings["Right"] = Key.Right;
        keyboard_bindings["Up"] = Key.Up;
        keyboard_bindings["SaveScore"] = Key.Backspace;
        keyboard_bindings["ExitGame"] = Key.LeftShift;


        controller_bindings["Shoot"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A;
        controller_bindings["Left"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT;
        controller_bindings["Right"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT;
        controller_bindings["Up"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y;
        controller_bindings["SaveScore"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B;
        controller_bindings["ExitGame"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X;
        controller_bindings["HighScore_Back"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X;
        controller_bindings["Grid"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSHOULDER;
        controller_bindings["Theme"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSHOULDER;
        controller_bindings["Start"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y;
        controller_bindings["HighScore"] = SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A;

        es = new EntryScreen(Resolution);
        hs = new highscorescreen(Resolution, s.getScoreboard());

    }

    public void Update()
    {
        //timers to handle different game elements
        time += Engine.TimeDelta;
        asteroidTime += Engine.TimeDelta;
        invinTime += Engine.TimeDelta;
        powerupTime += Engine.TimeDelta;

        // screen switching code
       
        if (entry)
        {
            es.draw();
            if (es.isStartClicked())
            {
            entry = false;
            }
            if (es.isHighScoreClicked())
            {
                entry = false;
                highScore = true;
            }

        }else if (highScore)
        {
            if(leaderboardRefreshed == false)
            {
                // refresh
                hs.refresh(s.getScoreboard());
                leaderboardRefreshed = true;
            }
            hs.draw();
            if (hs.isGridClicked())
            {
                leaderboardRefreshed = false;
                highScore = false;
                entry = true;
            }
        }
        else if (end)
        {
            Theme.drawEndBackground();
            Engine.DrawString("Score: " + score, Vector2.Zero, Theme.getColor(), Engine.LoadFont("Starjedi.ttf", 40));
            Engine.DrawString("L_SHIFT to exit game [CON-X]", new Vector2(640, 280), Theme.getColor(), Engine.LoadFont("Oswald-Regular.ttf",25), TextAlignment.Center);
            Engine.DrawString("username + [BACKSPACE] to save score [CON-B]", new Vector2(1000, 25), Theme.getColor(), Engine.LoadFont("Oswald-Regular.ttf", 20), TextAlignment.Center);
            i.drawTextBox();
            String t = i.getLatestInput();
            Engine.DrawString(t, new Vector2(800, 70), Theme.altColor(), Engine.LoadFont("Starjedi.ttf", 40));
            Engine.DrawString("Made with Firebase & Newtonsoft", new Vector2(50, 680), Theme.altColor(), Engine.LoadFont("Oswald-Regular.ttf", 20), TextAlignment.Left);
            if ((Engine.GetKeyDown(keyboard_bindings["SaveScore"]) || SDL.SDL_GameControllerGetButton(controller, controller_bindings["SaveScore"]) > 0) && scoreDisplay == false)
            {

                scoreDisplay = true;
                userScores = null;
                
                // display past scores below text box
                name = i.getLatestInput();
                s.updateUser(name.ToLower(), timestamp.get_time(), score.ToString());
                
            }
            if(scoreDisplay == true)
            {
                // display past scores
                if(userScores == null)
                {
                    userScores = s.retrieveUser(name);
                }
                int y = 200;
                foreach (KeyValuePair<string, string> entry in userScores)
                {
                    //String time = hs.timestamp_to_string(entry.Key);
                    String time = timestamp.convert(entry.Key);
                    String score = entry.Value;
                    Engine.DrawString(time + " | " + score, new Vector2(1000, y), Theme.getColor(), Engine.LoadFont("Starjedi.ttf", 15), TextAlignment.Center);
                    y = y + 30;
                }

            }
            if ((Engine.GetKeyDown(keyboard_bindings["ExitGame"]) || SDL.SDL_GameControllerGetButton(controller, controller_bindings["ExitGame"]) > 0))
            {
                i.text = "";
                scoreDisplay = false;
                userScores = null;
                end = false;
                entry = true;
                score = 0;
                lives = 3;
                AsteroidCollection.clearAll();
                AsteroidCollection.spawn();

            }
        } else
        {
            //game play screen drawing

            Theme.drawGameBackground();
            Engine.DrawString("Lives: " + lives, new Vector2(100, 50), Theme.getColor(), Engine.LoadFont("Starjedi.ttf", 20), TextAlignment.Center);
            Engine.DrawString("Score: " + score, new Vector2(100, 10), Theme.getColor(), Engine.LoadFont("Starjedi.ttf", 20), TextAlignment.Center);
            shipBounds = new Bounds2(mov, new Vector2(100, 100));
            Theme.drawRocketShip(mov, 100, rot);
            //creates a set of bounds simulating the shots for hitboxes
            if (shoot)
            {
                shotBounds = new Bounds2(smov, new Vector2(shotBoundSizeFactor, shotBoundSizeFactor));
                Theme.drawProjectile(smov, shotBoundSizeFactor);
            }
            
            
            AsteroidCollection.handleAsteroidSpawning();
        }




        // SHOT SHOOTING //

        if ((Engine.GetKeyDown(keyboard_bindings["Shoot"]) || SDL.SDL_GameControllerGetButton(controller, controller_bindings["Shoot"]) > 0) && !shoot)
        {
            shoot = true;
            rotLock = rot;
            time = 0;

        }

        if (shoot)
        {
            smov = getDirectionalVector(smov, rotLock, 30);
           
        } else
        {
            smov = new Vector2(mov.X+50,mov.Y+50);
        }

        if (time > shotCoolDownTime && shoot)
        {
            shoot = false;
            smov = mov;
        }



        // SHIP MOVEMENT //


        //moves ship with intertia

        if (Engine.GetKeyHeld(keyboard_bindings["Left"]) || SDL.SDL_GameControllerGetButton(controller, controller_bindings["Left"]) > 0)
        {
            rot -= 7;

        }

        else if (Engine.GetKeyHeld(Key.Right) || SDL.SDL_GameControllerGetButton(controller, controller_bindings["Right"]) > 0)
        {
            rot += 7;
        }

        if (Engine.GetKeyHeld(Key.Up)|| SDL.SDL_GameControllerGetButton(controller, controller_bindings["Up"]) > 0)
        {
            mov = getDirectionalVector(mov, rot, 10);

        }

        //starts inertia when up is released
        if (Engine.GetKeyUp(keyboard_bindings["Up"]) || SDL.SDL_GameControllerGetButton(controller, controller_bindings["Up"]) > 0)
        {

            fly = true;
            inertia = 100;

        }

        //handles inertia movement
        if (fly == true)
        {
            mov = getDirectionalVector(mov, rot, inertia/10);
            inertia--;
            Console.WriteLine(inertia);
            if (inertia <= 1)
            {
                fly = false;
            }
        }

        //x wraparound
        if (mov.X <= -80)
        {
            mov.X = 1170;
        } else if(mov.X >= 1180)
        {
            mov.X = -50;
        }

        //y wraparound
        if (mov.Y <= -80)
        {
            mov.Y = 610;
        }
        else if (mov.Y >= 620)
        {
            mov.Y = -50;
        }

        // ASTEROID MOVEMENT //
        AsteroidCollection.handleAsteroidMoving();

        // COLLISION HANDLING //
        if (AsteroidCollection.handleAsteroidShotCollisions(shotBounds))
        {
            score++;
            shoot = false;
        }

        if (AsteroidCollection.handleAsteroidShipCollisions(shipBounds))
        {
            if (!invincible)
            {
                lives--;
                invincible = true;
                if (lives <= 0)
                {
                    end = true;
                }
            }
            
            
        }

        if (invincible)
        {
            mov = new Vector2(640, 360);
            if (invinTime > 5)
            {
                invinTime = 0;
                invincible = false;
                
            }
        }

        //ASTEROID RESPAWNING//
        if (asteroidTime > spawnDelay && !highScore && !entry)
        {
            System.Diagnostics.Debug.WriteLine("This is a log");
            AsteroidCollection.spawn();
            asteroidTime = 0;
        }

        if (score > 30)
        {
            spawnDelay=2;
        } else if (score > 20)
        {
            spawnDelay = 3;
        } else if (score > 10)
        {
            spawnDelay = 4;
        }

        // POWERUP HANDLING //
        
        if (powerupTime > delay && !end && !entry && !highScore)
        {
            powerupTime = 0;
            delay = rd.Next(10, 20);
            pPos = new Vector2(rd.Next(100, 1180), rd.Next(100, 620));
            pBounds = new Bounds2(pPos, new Vector2(100,100));
            pVis = true;
            spawnPowerup(pPos);
        } else if(pVis && !end && !entry && !highScore)
        {
            spawnPowerup(pPos);
        }

        //powerup checks
        if (!powerUp1Engaged && !powerUp2Engaged && !powerUp3Engaged)
        {
            powerUpCounter = 0;
        }
        else
        {
            powerUpCounter++;

            //ui display for powerup info
            if (powerUp1Engaged && !powerUp2Engaged && !powerUp3Engaged)
            {
                Engine.DrawString("Powerup Activated: FASTER SHOOTING", location, Theme.getColor(), buttonFont, TextAlignment.Left);
            } else if (powerUp2Engaged && !powerUp1Engaged && !powerUp3Engaged)
            {
                Engine.DrawString("Powerup Activated: BIGGER HITS", location, Theme.getColor(), buttonFont, TextAlignment.Left);
            } else if (powerUp3Engaged && !powerUp2Engaged && !powerUp2Engaged)
            {
                Engine.DrawString("Powerup Activated: SLOWDOWN", location, Theme.getColor(), buttonFont, TextAlignment.Left);
            }

            if (powerUpCounter > 400)
            {
                powerUp1Engaged = false;
                powerUp2Engaged = false;
                powerUp3Engaged = false;
                powerUpCounter = 0;
            }
        }

        // UPON PICKUP CONDITION:
        if (shipBounds.Overlaps(pBounds))
        {
            pVis = false;
            pBounds = new Bounds2(0, 0, 0, 0);
            Random rnd = new Random();
            int whichPowerUp = rnd.Next(1, 4);
            if (whichPowerUp == 1)
            {
                powerUp1Engaged = true;
                powerUp2Engaged = false;
                powerUp3Engaged = false;
            }
            else if (whichPowerUp == 2)
            {
                powerUp2Engaged = true;
                powerUp1Engaged = false;
                powerUp3Engaged = false;
            }
            else
            {
                powerUp3Engaged = true;
                powerUp1Engaged = false;
                powerUp2Engaged = false;
            }
        }

            if (powerUp1Engaged)
            {
                shotCoolDownTime = 0.25;
            }
            else 
            {
                shotCoolDownTime = 0.4;
            }

            if (powerUp2Engaged)
            {
                shotBoundSizeFactor = 25;
            }
            else
            {
                shotBoundSizeFactor = 15;
            }

            if (powerUp3Engaged)
            {
                Asteroid.asteroidMovFactor = 1;
            }
            else 
            {
                Asteroid.asteroidMovFactor = 2;
            }
        
    }




    //returns a new vector with movement in a direction of choice
    public static Vector2 getDirectionalVector(Vector2 cur, float rotation, float moveFactor)
    {
        float x = (float)(Convert.ToDouble(cur.X) + moveFactor * Math.Cos(ConvertDegreesToRadians(rotation)));
        float y = (float)(Convert.ToDouble(cur.Y) + moveFactor * Math.Sin(ConvertDegreesToRadians(rotation)));
        Vector2 ret = new Vector2(x,y);
        return ret;

    }

    

    public static double ConvertDegreesToRadians(double degrees)
    {
        double radians = (Math.PI / 180) * degrees;
        return (radians);

    }


    public static void spawnPowerup( Vector2 pos)
    {
        
        Theme.drawPowerup(pos, 100, 0);
    }

}

