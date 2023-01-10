﻿using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1280, 720);
    EntryScreen es = new EntryScreen(Resolution);
    

    Texture ship = Engine.LoadTexture("ship.png");
    Texture asteroid = Engine.LoadTexture("asteroid.png");
    Texture shot = Engine.LoadTexture("projectile.png");
    Texture bg = Engine.LoadTexture("background.png");

    float time = 0;
    float asteroidTime = 0;

    //ship vars
    float rot = 0;
    Vector2 mov = new Vector2(100, 100);
    float inertia = 100;
    bool fly = false;
    Bounds2 shipBounds = new Bounds2(100, 100, 100, 100);
    int lives = 3;


    //shot variables
    Vector2 smov = new Vector2(400, 400);
    bool shoot = false;
    float rotLock = 0;
    Bounds2 shotBounds = new Bounds2(400,400, 100, 100);

    //asteroid inits
    Asteroid a = new Asteroid( new Vector2(600, 600),100,new Vector2(100,100),1);
    Asteroid b = new Asteroid(new Vector2(400, 800), 60, new Vector2(100,100),1);
    
    

    //game vars
    bool spawnAst = true;
    bool entry = true;
    bool end = false;
    int score = 0;

    public Game()
    {

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
        Engine.DrawTexture(bg, Vector2.Zero);
        time += Engine.TimeDelta;
        asteroidTime += Engine.TimeDelta;
        Engine.DrawString("Score: " + score, new Vector2(100, 10), Color.White, Engine.LoadFont("Starjedi.ttf", 20), TextAlignment.Center);

        if (entry)
        {
            es.draw();
            if (es.isStartClicked())
            {
                entry = false;
            }
        } else if (end)
        {
            Engine.DrawString("game over",new Vector2 (640,360) , Color.White, Engine.LoadFont("Starjedi.ttf", 72), TextAlignment.Center);
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
            Engine.DrawTexture(ship, mov, size: new Vector2(100, 100), rotation: rot);
            //creates a set of bounds simulating the shots for hitboxes
            shotBounds = new Bounds2(smov, new Vector2(10, 10));
            Engine.DrawTexture(shot, smov, size: new Vector2(10, 10));


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

        if (time > 0.3 && shoot)
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
            lives--;
            if (lives <= 0)
            {
                end = true;
            } 
            
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

