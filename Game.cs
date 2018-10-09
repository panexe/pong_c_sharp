using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    // Vecotr for 2d Pos
    public struct Vector2
    {
        public double v1 { get; set; }
        public float v2 { get; set; }

        public int x { get; set; }
        public int y { get; set; }



        public Vector2(int a, int b)
        {
            x = a;
            y = b;

            v1 = 0;
            v2 = 0;
        }

        public Vector2(double v1, float v2)
        {
            this.v1 = v1;
            this.v2 = v2;

            x = 0;
            y = 0;
        }
    }
    public class Game
    {
        public Vector2 screen_size { get; set; }
        public Vector2 ball_position { get; set; }


        public int movement_speed { get; set; }
        public int player_1_position { get; set; }
        public int player_2_position { get; set; }

        public bool win_player1 { get; set; }
        public bool win_player2 { get; set; }
        public bool game_over { get; set; }

        Player player1 { get; set; }
        Player player2 { get; set; }
        Ball ball { get; set; }

        Stopwatch cooldown { get; set; }

        Brush brush;


        //todo add brushes for each drawable object 


        public Game(Vector2 _screen_size)
        {
            // init.
            player_1_position = _screen_size.x / 10;
            player_2_position = (_screen_size.x / 10) * 9;
            ball_position = new Vector2(_screen_size.x / 2, _screen_size.y / 2);

            win_player1 = false;
            win_player2 = false;
            game_over = false;

            cooldown = new Stopwatch();
            cooldown.Reset();

            screen_size = _screen_size;

            movement_speed = 5;

            brush = new SolidBrush(Color.Black);

            player1 = new Player(brush, 20, 100, new Vector2(player_1_position, 0), screen_size);
            player2 = new Player(brush, 20, 100, new Vector2(player_2_position, 0), screen_size);
            ball = new Ball(brush,ball_position, 20, 180);
        }

        public void tick()
        {

            // each tick

            ball.updateRadian();
            ball.updateDegree();

            ball.updateHitbox();
            player1.updatePosition();
            player1.updateHitbox();
            player2.updatePosition();
            player2.updateHitbox();


            colision();

            checkforWin();
            if (win_player1)
            {
                game_over = true;
                //MessageBox.Show("player 1 won");
            }
            else if (win_player2)
            {
                game_over = true;
                //MessageBox.Show("player 2 won");
            }

        }

        public void draw(PaintEventArgs e)
        {
            player1.draw(e);
            player2.draw(e);
            ball.draw(e);
        }

        private void checkforWin()
        {
            if(ball.hitbox.X <= 0)
            {
                win_player2 = true;

            }
            if(ball.hitbox.X > screen_size.x)
            {
                win_player1 = true;
            }
        }

        public void move_player1(bool down, bool up)
        {
            if (down == up)
            {
                player1.velocity = new Vector2(0, 0);
            }
            else if (down)
                player1.velocity = new Vector2(0, movement_speed);
            else if (up)
                player1.velocity = new Vector2(0, -movement_speed);
        }

        public void move_player2(bool down, bool up)
        {
            if (down == up)
            {
                player2.velocity = new Vector2(0, 0);
            }
            else if (down)
                player2.velocity = new Vector2(0, movement_speed);
            else if (up)
                player2.velocity = new Vector2(0, -movement_speed);
        }

        public void colision()
        {

            if (player1.hitbox.IntersectsWith(ball.hitbox))
            {
                if (cooldown.ElapsedMilliseconds > 100 || cooldown.ElapsedMilliseconds == 0)
                {
                    ball.turnAroundLeft();
                    ball.updateDegree();
                    ball.updateRadian();
                    cooldown.Reset();
                    cooldown.Start();
                }
                else
                {
                    //MessageBox.Show("hey");
                }

            }
            // collision with right player
            else if (player2.hitbox.IntersectsWith(ball.hitbox))
            {

                
                    if (cooldown.ElapsedMilliseconds > 100 || cooldown.ElapsedMilliseconds == 0)
                    {
                        cooldown.Reset();
                        cooldown.Start();
                        ball.turnAroundRight();
                        ball.updateDegree();
                        ball.updateRadian();
                        
                    }
                    else
                    {
                    }

            }
            // upper screen boundary 
            else if(ball.hitbox.Location.Y <= 0)
            {
                if (cooldown.ElapsedMilliseconds > 100 || cooldown.ElapsedMilliseconds == 0)
                {
                    cooldown.Reset();
                    cooldown.Start();
                    ball.turnAroud_boarder();
                    ball.updateDegree();
                    ball.updateRadian();

                }
                else
                {
                }
            }

            else if(ball.hitbox.Location.Y + 3*ball.size >= screen_size.y)
            {
                if (cooldown.ElapsedMilliseconds > 500 || cooldown.ElapsedMilliseconds == 0)
                {
                    cooldown.Reset();
                    cooldown.Start();
                    ball.turnAroud_boarder();
                    ball.updateDegree();
                    ball.updateRadian();

                }
                else
                {
                }
            }
            
        }
    }
}
