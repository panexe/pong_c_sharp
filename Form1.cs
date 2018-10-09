using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    public partial class Form1 : Form
    {

        Game game;
        public Form1()
        {
            InitializeComponent();
            Vector2 size = new Vector2(this.Width, this.Height);
            game = new Game(size);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void game_timer_Tick(object sender, EventArgs e)
        {
            screen.Invalidate();
            screen.Update();
            
            game.tick();

            if (game.game_over)
            {
                game_timer.Stop();
            }
        }

        private void screen_Paint(object sender, PaintEventArgs e)
        {
            game.draw(e);

            typeof(Panel).InvokeMember("DoubleBuffered",
    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
    null, screen, new object[] { true });
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                game.move_player2(false, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                game.move_player2(true, false);
            }
            if (e.KeyCode == Keys.W)
            {
                game.move_player1(false, true);
            }
            if (e.KeyCode == Keys.D)
            {
                game.move_player1(true, false);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                game.move_player2(false, false);
            }
            if (e.KeyCode == Keys.Down)
            {
                game.move_player2(false, false);
            }
            if (e.KeyCode == Keys.W)
            {
                game.move_player1(false, false);
            }
            if (e.KeyCode == Keys.D)
            {
                game.move_player1(false, false);
            }
        }
    }
}
