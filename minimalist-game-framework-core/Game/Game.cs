﻿using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "DRAW-STROY";
    public static readonly Vector2 Resolution = new Vector2(1280, 720);
    Theme theme;
    EntryScreen es;
    

    Texture shot = Engine.LoadTexture("projectile.png");
    Texture bg = Engine.LoadTexture("background.png");

    float time = 0;
    float asteroidTime = 0;

    //ship vars
    float rot = 180;
    Vector2 mov = new Vector2(100, 100);
    float inertia = 100;
    bool fly = false;
    Bounds2 shipBounds = new Bounds2(100, 100, 100, 100);


    //shot variables
    Vector2 smov = new Vector2(400, 400);
    bool shoot = false;
    float rotLock = 0;
    Bounds2 shotBounds = new Bounds2(400,400, 100, 100);

    //asteroid inits
    Asteroid a = new Asteroid( new Vector2(600, 600),100,new Vector2(100,100),1);
    Asteroid b = new Asteroid(new Vector2(400, 800), 60, new Vector2(100,100),1);
    
    //powerup vars
    bool powerUp1Engaged = false;
    bool powerUp2Engaged = false;
    bool powerUp3Engaged = false;

    //additional constants
    public double shotCoolDownTime = 0.3;
    public float shotBoundSizeFactor = 10;

    public float powerUpCounter = 0;

    //game vars
    bool spawnAst = true;
    bool entry = true;
    bool end = false;
    int score = 0;

    public Game()
    {
        List<String> startBackgrounds = new List<String>() { "startBackgroundD.png", "startBackgroundL.png", "startBackgroundDG.png", "startBackgroundLG.png" };
        List<String> gameBackgrounds = new List<String>() { "gameBackgroundD.png", "gameBackgroundL.png", "gameBackgroundDG.png", "gameBackgroundLG.png" };
        List<String> rocketShips = new List<String>() { "rocketshipD.png", "rocketshipL.png" };
        List<String> asteroids = new List<String>() {"awhite.png", "ablack.png"};

        theme = new Theme(Resolution, startBackgrounds, gameBackgrounds, startBackgrounds, rocketShips, asteroids, asteroids);

        
        

        es = new EntryScreen(Resolution, theme);
    }


    public void log(int score)
    {
        // log score, print top 10 highest scores, print score history of user


        System.Diagnostics.Debug.WriteLine("Your Score is: " + score + ". If you would like to save your score, please enter your name in the console. If not, type N.");
        String name = Console.ReadLine();
        if(name.Equals("N") == false)
        {
            scoreboard.append(name, score.ToString());
            System.Diagnostics.Debug.WriteLine(name + "'s Scores:");
            System.Diagnostics.Debug.WriteLine(scoreboard.retrieve(name));
        }
        System.Diagnostics.Debug.WriteLine("HIGH SCORES: ");
        String[] scores = scoreboard.getScoreboard();
        for(int i = 0; i <scores.Length; i++)
        {
            System.Diagnostics.Debug.WriteLine(scores[i]);
        }


    }

    public void Update()
    {
        theme.drawGameBackground();
        time += Engine.TimeDelta;
        asteroidTime += Engine.TimeDelta;
        Engine.DrawString("Score: " + score, new Vector2(100, 10), Color.White, Engine.LoadFont("Starjedi.ttf", 20), TextAlignment.Center);

        //powerup checks
        if(!powerUp1Engaged && !powerUp2Engaged && !powerUp3Engaged)
        {
            powerUpCounter = 0;
        } else
        {
            powerUpCounter ++;
            if(powerUpCounter > 1000)
            {
                powerUp1Engaged = false;
                powerUp2Engaged = false;
                powerUp3Engaged = false;
                powerUpCounter = 0;
            }
        }

        // UPON PICKUP CONDITION:
        Random rnd = new Random();
        int whichPowerUp = rnd.Next(1, 4);
        if (whichPowerUp == 1)
        {
            powerUp1Engaged = true;
        }
        else if (whichPowerUp == 2)
        {
            powerUp2Engaged = true;
        }
        else
        {
            powerUp3Engaged = true;
        }

        if (powerUp1Engaged)
        {
            shotCoolDownTime = 0.15;
        } else
        {
            shotCoolDownTime = 0.3;
        }

        if (powerUp2Engaged)
        {
            shotBoundSizeFactor = 15;
        } else
        {
            shotBoundSizeFactor = 15;
        }

        if (powerUp3Engaged)
        {
            Asteroid.asteroidMovFactor = 1;
        } else
        {
            Asteroid.asteroidMovFactor = 2;
        }

        if (entry)
        {
            es.draw();
            if (es.isStartClicked())
            {
                entry = false;
            }
        } else if (end)
        {
            Engine.DrawString("GAME OVER",new Vector2 (640,200) , Color.White, Engine.LoadFont("Starjedi.ttf", 77), TextAlignment.Center);
            Engine.DrawString("Score: " + score, new Vector2(640, 450), Color.White, Engine.LoadFont("Starjedi.ttf", 40), TextAlignment.Center);
            Engine.DrawString("SPACE to exit game", new Vector2(640, 320), Color.White, Engine.LoadFont("Starjedi.ttf", 30), TextAlignment.Center);
            if (Engine.GetKeyDown(Key.Space))
            {
                end = false;
                entry = true;
                score = 0;
                AsteroidCollection.clearAll();
                AsteroidCollection.spawn();

            }
        } else
        {
            shipBounds = new Bounds2(mov, new Vector2(100, 100));
            //Engine.DrawTexture(ship, mov, size: new Vector2(100, 100), rotation: rot);
            theme.drawRocketShip(mov, 100, rot);
            //creates a set of bounds simulating the shots for hitboxes
            shotBounds = new Bounds2(smov, new Vector2(shotBoundSizeFactor, shotBoundSizeFactor));
            Engine.DrawTexture(shot, smov, size: new Vector2(shotBoundSizeFactor, shotBoundSizeFactor));


            AsteroidCollection.handleAsteroidSpawning();
        }




        // SHOT SHOOTING //

        if (Engine.GetKeyDown(Key.Space) && !shoot)
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

        if (Engine.GetKeyHeld(Key.Left))
        {
            rot -= 7;

        }

        else if (Engine.GetKeyHeld(Key.Right))
        {
            rot += 7;
        }

        if (Engine.GetKeyHeld(Key.Up))
        {
            mov = getDirectionalVector(mov, rot, 10);

        }

        //starts inertia when up is released
        if (Engine.GetKeyUp(Key.Up))
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
            end = true;
        }

        //ASTEROID RESPAWNING//
        if (asteroidTime > 5)
        {
            System.Diagnostics.Debug.WriteLine("This is a log");
            AsteroidCollection.spawn();
            asteroidTime = 0;
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

}

