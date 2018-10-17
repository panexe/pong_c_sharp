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
        public int ball_init_angle { get; set; }
        public Point score_1_position { get; set; }
        public Point score_2_position { get; set; }

        public bool win_player1 { get; set; }
        public bool win_player2 { get; set; }
        public bool game_over { get; set; }
        public bool reseted { get; set; }

        Random random { get; set; }
        Player player1 { get; set; }
        Player player2 { get; set; }
        public Ball ball { get; set; }

        Font font { get; set; }

        Stopwatch cooldown { get; set; }
        public Font VsFont { get; private set; }

        Brush brush;


        //todo add brushes for each drawable object 


        public Game(Vector2 _screen_size)
        {
            random = new Random();
            ball_init_angle = random.Next(0, 360);

            // init.
            player_1_position = _screen_size.x / 10;
            player_2_position = (_screen_size.x / 10) * 9;
            ball_position = new Vector2(_screen_size.x / 2, _screen_size.y / 2);
            score_1_position = new Point((_screen_size.x / 10) * 3, (_screen_size.y / 10) * 2);
            score_2_position = new Point((_screen_size.x / 10) * 6, (_screen_size.y / 10) * 2);


            win_player1 = false;
            win_player2 = false;
            game_over = false;

            cooldown = new Stopwatch();
            cooldown.Reset();

            font = new Font("Impact", 46, FontStyle.Regular, GraphicsUnit.Point);

            screen_size = _screen_size;

            movement_speed = 5;

            brush = new SolidBrush(Color.HotPink);

            player1 = new Player(brush, 20, 100, new Vector2(player_1_position, 0), screen_size);
            player2 = new Player(brush, 20, 100, new Vector2(player_2_position, 130), screen_size);
            ball = new Ball(brush,ball_position, 20, ball_init_angle, 10);
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
            if (!win_player1 && !win_player2 )
                checkforWin();

            /*if (win_player1 && !reseted)
            {
                reseted = true;
                game_over = true;
                player1.score++;
                restart();
                //MessageBox.Show("player 1 won");
            }
            else if (win_player2 && !reseted)
            {
                reseted = true;
                game_over = true;
                player2.score++;
                restart();
                //MessageBox.Show("player 2 won");
            }*/

        }

        public void restart()
        {
            player_1_position = screen_size.x / 10;
            player_2_position = (screen_size.x / 10) * 9;
            ball_position = new Vector2(screen_size.x / 2, screen_size.y / 2);
            ball.position = ball_position;
            //MessageBox.Show("hey");

            cooldown = new Stopwatch();
            cooldown.Reset();

            reseted = false;
            win_player1 = false;
            win_player2 = false;

            ball_init_angle = random.Next(0, 360);
            if(ball_init_angle % 90 == 0)
            {
                ball_init_angle *= 2;
                if (ball_init_angle >= 360)
                    ball_init_angle /= 3;
            }
            ball = new Ball(brush, ball_position, 20, ball_init_angle, 10);
        }

        public void draw(PaintEventArgs e)
        {
            player1.draw(e);
            player2.draw(e);
            ball.draw(e);
        }

        public void draw_score(PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(brush), new Point(screen_size.x / 2, 0), new Point(screen_size.x /2, screen_size.y));
            e.Graphics.DrawString(player1.score.ToString(), font, brush, score_1_position);
            e.Graphics.DrawString(player2.score.ToString(), font, brush, score_2_position);

        }

        private void checkforWin()
        {
            if(ball.hitbox.X <= 0)
            {
                win_player2 = true;
                player2.score++;
                restart();

            }
            else if(ball.hitbox.X > screen_size.x)
            {
                win_player1 = true;
                player1.score++;
                restart();
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
            if(player2.hitbox.IntersectsWith(ball.hitbox))
            {
                if (cooldown.ElapsedMilliseconds > 100 || cooldown.ElapsedMilliseconds == 0)
                {
                    cooldown = new Stopwatch();
                    cooldown.Start();
                    ball.turnAroundRight();
                    ball.updateDegree();
                    ball.updateRadian();
                    
                }
                else
                {
                    //MessageBox.Show("hey");
                }

            }
            // upper screen boundary 
            if (ball.hitbox.Location.Y <= 0)
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
            // lower screen boundary 
            else if(ball.hitbox.Location.Y + ball.size >= screen_size.y)
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
            
        }
    }
}
