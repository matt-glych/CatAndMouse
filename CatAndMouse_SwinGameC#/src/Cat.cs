using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    public class Cat : GameObject
    {

        public Cat(Point2D position) : base(position)
        {
            _sprite = SwinGame.CreateSprite(SwinGame.BitmapNamed("Cat"));
            _startingPosition = position;
            Initialise(position);
        }
        
        // chase
        public void ChaseMouse(Mouse mouse)
        {
            Follow(mouse.Position);
        }

        public new void Respawn()
        {
            Initialise(_startingPosition);
        }

        // find simple path
        public void Follow(Point2D target)
        {
            if(X < (target.X - 20))
            {
                X += 100;
            }
            else if (X > (target.X + 20))
            {
                X -= 100;
            }
            else if (Y < (target.Y - 20))
            {
                Y += 100;
            }
            else if (Y > (target.Y + 20))
            {
                Y -= 100;
            }
        }
    }
}
