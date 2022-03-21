using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    public class Cheese : GameObject
    {

        // constructor
        public Cheese(Point2D position): base(position)
        {
            _sprite = SwinGame.CreateSprite("Cheese", SwinGame.BitmapNamed("Cheese"));
            Initialise(position);
        }
    }
}
