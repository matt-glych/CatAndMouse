using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    public class Mouse : GameObject
    {
        //lives
        private int _lives;
        //alive boolean
        private bool _alive;


        //Constructor
        public Mouse(Point2D position) : base(position)
        {
            _sprite = SwinGame.CreateSprite("Mouse",SwinGame.BitmapNamed("Mouse"));
            _lives = 3;
            _alive = true;
            Initialise(position);
        }
        
        //lose a life
        public void LoseALife()
        {
            if(_lives >1)
            {
                _lives--;
            }else
            {
                _lives = 0;
                _alive = false;
            }
        }

        public new void Respawn()
        {
            Initialise(_startingPosition);
        }

        //getters and setters
        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }
        public bool isAlive
        {
            get { return _alive; }
            set { _alive = value; }
        }
    }
}
