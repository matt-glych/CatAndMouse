using System;
using System.Collections.Generic;
using System.IO;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
        // load and name resources
        public static void LoadResources()
        {
            // sprites
            SwinGame.LoadBitmapNamed("Cat", "cat.png");
            SwinGame.LoadBitmapNamed("Mouse", "mouse.png");
            SwinGame.LoadBitmapNamed("Cheese", "cheese01.png");
            SwinGame.LoadBitmapNamed("MouseCaught", "cat_caught_mouse.png");
            SwinGame.LoadBitmapNamed("MouseHole_top", "mouseHole_top.png");
            SwinGame.LoadBitmapNamed("MouseHole_bottom", "mouseHole_bottom.png");

            // sounds
            SwinGame.LoadSoundEffectNamed("Move", "sfx_movement.wav");
            SwinGame.LoadSoundEffectNamed("HoleTravel", "sfx_holeTravel.wav");
            SwinGame.LoadSoundEffectNamed("CollectCheese", "sfx_collectCheese.wav");
            SwinGame.LoadSoundEffectNamed("DeathMouse", "sfx_deathscream2.wav");
        }

        // mouse Controls
        public static void Controls(Mouse mouse)
        {
            // move check
            bool moved = false;
            bool movedByHole = false;

            // move in direction
            bool moveLeft = SwinGame.KeyTyped(KeyCode.LeftKey);
            bool moveRight = SwinGame.KeyTyped(KeyCode.RightKey);
            bool moveUp = SwinGame.KeyTyped(KeyCode.UpKey);
            bool moveDown = SwinGame.KeyTyped(KeyCode.DownKey);

            // move left
            if (moveLeft) {
                if (mouse.X > 150)
                {
                    mouse.X -= 100;
                }
            }
            // move right
            if (moveRight)
            {
                if (mouse.X < 450)
                {
                    mouse.X += 100;
                }
            }
            // move up
            if (moveUp)
            {
                if (mouse.Y > 150)
                {
                    mouse.Y -= 100;
                }
                //detect if going through top hole
                else if ((mouse.X > 300 && mouse.X < 400))
                {
                    mouse.Y += 400;
                    movedByHole = true;
                }
            }
            // move down
            if (moveDown)
            {
                if (mouse.Y < 450)
                {
                    mouse.Y += 100;
                    
                }
                //detect if going through bottom hole
                else if ((mouse.X > 300 && mouse.X < 400))
                {
                    mouse.Y -= 400;
                    movedByHole = true;
                }
            }

            moved = (moveLeft || moveRight || moveUp || moveDown);

            if (movedByHole)
            {
                SwinGame.PlaySoundEffect("HoleTravel");
                moved = false;
            }
            else if(moved)
            {
                SwinGame.PlaySoundEffect("Move");
            }
        }

        // write text centered
        public static void WriteCentered(string text, float yPos, float screenWidth)
        {
            Text.DrawText(text, Color.DodgerBlue, (screenWidth / 2) - (3.5f * text.Length), yPos);
        }


        public static void Main()
        {
            // screen size 
            int w_width = 700;
            int w_height = 700;

            // load game resources & assets
            LoadResources();
            // create game window
            SwinGame.OpenGraphicsWindow("GameMain", w_width, w_height);



            // game variables
            Highscore hs = new Highscore();
            int highscore = hs.score;
            Timer gameTimer = new Timer();
            float ticks;
            int seconds = 0;
            int score = 0;
            bool mouseCaught = false;
            int gameSpeed = 500;
            bool draw = true;
            bool titleScreen = true;
            // TODO - Add player name to highscore
            // string highscore_full_string = ($"{hs.name} - {hs.score}");
            string ui_highscore_txt = highscore.ToString();

            // mouse-caught timer
            Timer mouseCaughtTimer = new Timer();
            float mouseCaughtTicks;
            float mouseCaughtTime = 2500;

            // mouse initial position
            Point2D startingPosition_mouse = new Point2D
            {
                X = 550,
                Y = 550
            };

            // cat initial position
            Point2D startingPosition_cat = new Point2D
            {
                X = 150,
                Y = 150
            };

            // cheese initial position
            Point2D startingPosition_cheese = new Point2D
            {
                X = 250,
                Y = 250
            };

            // create sprites
            Sprite s_mouseCaught = SwinGame.CreateSprite(SwinGame.BitmapNamed("MouseCaught"));
            Sprite mouseHole1 = SwinGame.CreateSprite(SwinGame.BitmapNamed("MouseHole_top"));
            Sprite mouseHole2 = SwinGame.CreateSprite(SwinGame.BitmapNamed("MouseHole_bottom"));

            // position mouse hole sprites
            mouseHole1.X = 350-(mouseHole1.Width/2);
            mouseHole1.Y = 47;
            mouseHole2.X = 350-(mouseHole1.Width / 2);
            mouseHole2.Y = 600;

            // crete the grid instance
            Grid grid = new Grid(100);
            // create the mouse instance
            Mouse mouse = new Mouse(startingPosition_mouse);
            // create the cat instance
            Cat cat = new Cat(startingPosition_cat);
            // create the cheese instance
            Cheese cheese = new Cheese(startingPosition_cheese);
            
            // game loop
            while (false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                //Clear the screen and draw the framerate
                SwinGame.ClearScreen(Color.Black);
                SwinGame.DrawFramerate(0, 0);
                //draw the highscore
                string s_heading = "- HIGH SCORE -";
                //Text.DrawText(s_heading, Color.DodgerBlue, (w_width / 2)  - (3.5f * s_heading.Length), 20 );
                //Text.DrawText(ui_highscore_txt, Color.DodgerBlue, (w_width / 2) - (3.5f * ui_highscore_txt.Length), 30 );
                WriteCentered(s_heading, 20, w_width);
                WriteCentered(ui_highscore_txt, 30, w_width);

                if (titleScreen)
                {
                    WriteCentered("CAT", 350, w_width);
                    WriteCentered("&",  360, w_width);
                    WriteCentered("MOUSE", 370, w_width);
                    WriteCentered("PRESS ANY KEY TO PLAY", 400, w_width);

                    if(SwinGame.AnyKeyPressed())
                    {
                        titleScreen = false;
                        gameTimer.Start();
                    }
                }
                else
                {
                    // level check

                    if (score == 10)
                        gameSpeed = 400;
                    if (score == 20)
                        gameSpeed = 300;
                    if (score == 30)
                        gameSpeed = 200;

                    //time seconds
                    ticks = gameTimer.Ticks*60/100;
                    //console.WriteLine("ticks: " + ticks);
                    if (ticks > gameSpeed)
                    {
                        cat.ChaseMouse(mouse);
                        seconds++;
                        gameTimer.Reset();
                       
                    }

                    //draw and update the game objects
                    if (draw)
                    {
                        Text.DrawText("LIVES: ", Color.DodgerBlue, 100, 50);
                        Text.DrawText(mouse.Lives.ToString(), Color.DodgerBlue, 160, 50);
                        Text.DrawText("SCORE: ", Color.DodgerBlue, 530, 50);
                        Text.DrawText(score.ToString(), Color.DodgerBlue, 590, 50);
                        grid.Draw();
                        cheese.DrawAndUpdate();
                        mouseHole1.Update();
                        mouseHole1.Draw();
                        mouseHole2.Update();
                        mouseHole2.Draw();
                    }
                    
                 
                    if (!mouseCaught)
                    {
                        Controls(mouse);
                        DetectCollisions();
                        mouse.DrawAndUpdate();
                        cat.DrawAndUpdate();

                        if ( score > hs.score)
                        {
                            ui_highscore_txt = "NEW HIGH SCORE!!!";
                        }
                    }
                    else
                    {
                        MouseCaught();
                    }
                }
                //Refresh and redraw on the screen
                SwinGame.RefreshScreen(60);
            }


            // mouse caught routine
            void MouseCaught()
            {
                gameTimer.Stop();
                gameTimer.Reset();
                s_mouseCaught.Update();
                s_mouseCaught.Draw();

                mouseCaughtTicks = mouseCaughtTimer.Ticks;

                if (mouseCaughtTicks > mouseCaughtTime)
                {
                    mouseCaughtTicks = 0;

                    // reset gameobject positions
                    mouse.Respawn();
                    cat.Respawn();

                    if (mouse.isAlive == false)
                    {
                        // stop drawing sprites
                        draw = false;

                        // draw gameover text
                        SwinGame.DrawText("GAME OVER", Color.DodgerBlue, s_mouseCaught.X + 7, s_mouseCaught.Y + 100);

                        // check for new highscore
                        if (score > highscore)
                        {
                            // save highscore
                            hs.Save("PLAYER", score);

                            Text.DrawText("NEW HIGH SCORE: ", Color.DodgerBlue, s_mouseCaught.X -30, s_mouseCaught.Y + 130);
                            Text.DrawText(score.ToString(), Color.DodgerBlue, s_mouseCaught.X + 106, s_mouseCaught.Y + 130);
                            //string playerName = Input.TextReadAsASCII();
                            //Text.DrawText(playerName, Color.DodgerBlue, s_mouseCaught.X - 60, s_mouseCaught.Y + 130);
                        }
                        else
                        {
                            Text.DrawText("SCORE: ", Color.DodgerBlue, s_mouseCaught.X + 8, s_mouseCaught.Y + 130);
                            Text.DrawText(score.ToString(), Color.DodgerBlue, s_mouseCaught.X + 66, s_mouseCaught.Y + 130);
                        }
                        
                        SwinGame.DrawText("REPLAY?", Color.DodgerBlue, s_mouseCaught.X + 15, s_mouseCaught.Y + 160);
                        //press enter or space to replay
                        if (SwinGame.KeyTyped(KeyCode.ReturnKey))
                        {
                            RestartGame();
                        }
                    }
                    else
                    {
                        gameTimer.Start();
                        mouseCaught = false;
                    }
                }
            }

            // collision detection
            void DetectCollisions()
            {
                // detect cat catching mouse
                if (mouse.Sprite.CollisionWithSprite(cat.Sprite))
                {
                    SwinGame.PlaySoundEffect("DeathMouse");
                    s_mouseCaught.X = mouse.X;
                    s_mouseCaught.Y = mouse.Y - 13;
                    mouseCaughtTimer.Start();
                    mouse.LoseALife();
                    mouseCaught = true;
                }
                // detect mouse catching cheese
                if (mouse.Sprite.CollisionWithSprite(cheese.Sprite))
                {
                    SwinGame.PlaySoundEffect("CollectCheese");
                    cheese.Respawn();
                    score++;
                }
            }

            // restart the game
            void RestartGame()
            {
                hs.Read();
                ui_highscore_txt = hs.score.ToString();
                gameSpeed = 500;
                mouseCaught = false;
                mouse.isAlive = true;
                mouse.Lives = 3;
                score = 0;
                draw = true;
                mouse.Respawn();
                cat.Respawn();
                gameTimer.Start();
            }
        }
    }
}