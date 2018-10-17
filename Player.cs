using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    class Player
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }

        public Vector2 screen_size { get; set; }

        public int sizex { get; set; }
        public int sizey { get; set; }
        public int score { get; set; }

        public Rectangle hitbox { get; set; }

        Brush brush;

        public Player(Brush _brush, int _sizex, int _sizey, Vector2 _position, Vector2 _screen_size)
        {
            brush = _brush;
            sizex = _sizex;
            sizey = _sizey;
            screen_size = _screen_size;
            score = 0;

            position = _position;
            velocity = new Vector2(0, 0);


            updateHitbox();
        }

        public void draw(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(brush, hitbox);
        }
        public void updateHitbox()
        {
            hitbox = new Rectangle(position.x, position.y , sizex, sizey);
        }

        public void updatePosition()
        {
            
            position = new Vector2(position.x + velocity.x, position.y + velocity.y);

            if (position.y <= 0)
                position = new Vector2(position.x + velocity.x, 0);
            if(position.y > screen_size.y - this.sizey)
                position = new Vector2(position.x + velocity.x, screen_size.y - this.sizey);


        }


    }
}
