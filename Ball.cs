using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    class Ball
    {
        Vector2 position { get; set; }
        Vector2 velocity { get; set; }

        public int size { get; set; }
        public float radian { get; set; }
        public int degree { get; set; }

        public Vector2 radianVector { get; set; }
        public Rectangle hitbox{get;set;}


        Brush brush;

        public Ball(Brush _brush, Vector2 _position , int _size , int _initDegree)
        {

            //init
            degree = _initDegree;

            updateDegree();
            brush = _brush;
            position = _position;
            velocity = new Vector2(5, 0);
            size = _size;

            

            updateHitbox();

        }

        public void draw( PaintEventArgs e)
        {
            e.Graphics.FillRectangle(brush,hitbox);
        }

        public void updateDegree()
        {
            radian = degree * ((float)Math.PI/180);
             
        }

        public void turnAroundLeft()
        {
            // up & left boarder to player 1 ( correct )
            if(degree >= 0 && degree <=180)
            degree = degree - (2 * (90- (180-degree)));

            // down & left boarder to player 1 (correct)
            if (degree > 180)
                degree = degree + (180 - 2* (degree - 180));
        }

        public void turnAroundRight()
        {
            // up & rigthborder to player 2 ( correct )
             if(degree >= 0 && degree <=180)
                degree = degree + (2*(90-degree));
            // down & rightborder to player 2 ( correct)
            if (degree <= 360 && degree >= 180)
                degree = degree - (2 * (90 -(360 - degree)));


        }

        public void turnAroud_boarder()
        {
            // upper screan boundary
            if (degree >= 0 && degree <= 180)
                degree = degree + (180 + 2 * (90 - degree));

            // lower screen boundary 
            else if ( degree > 180 && degree < 360)
            {
                degree = degree - 360 + 2 * (360 - degree);
            }
            
        }

        public void updateRadian()
        {
            radianVector = new Vector2(Math.Cos(radian), -(float)Math.Sin(radian));

        }


        public void updateHitbox()
        {
            updateRadian();


            position = new Vector2(position.x + radianVector.x + Convert.ToInt32(radianVector.v1*5), position.y + radianVector.y + Convert.ToInt32(radianVector.v2*5));
            hitbox = new Rectangle(position.x , position.y, size, size);
        }
    }
}
