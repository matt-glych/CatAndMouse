using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    public  class GameObject
    {
        //sprite
        protected Sprite _sprite;

        //position
        protected Point2D _position = new Point2D();
        protected Point2D _startingPosition = new Point2D();

        public GameObject(Point2D position)
        {
            _position = position;
            _startingPosition = position;
        }

        //initialise 
        protected void Initialise(Point2D position)
        {
            MoveToInstantly(Convert.ToInt16(position.X), Convert.ToInt16(position.Y));
            _position = position;
        }
        //respawn
        public void Respawn()
        {
            Random rnd = new Random();
            int xPos = rnd.Next(1, 6);
            int yPos = rnd.Next(1, 6);
            Point2D randomPosition = new Point2D();
            randomPosition.X = (xPos * 100) + 50;
            randomPosition.Y = (yPos * 100) + 50;
            Initialise(randomPosition);
        }

        //move to x & y taking z seconds
        public void MoveToTakingSeconds(Point2D position, int seconds)
        {
            _sprite.MoveTo(position, seconds);
            _position = position;
        }

        //move instantly to x and y
        public void MoveToInstantly(int x, int y)
        {
            _sprite.MoveTo(x - (_sprite.Width / 2), y - (_sprite.Height / 2));
            _position.X = x;
            _position.Y = y;
        }

        //getters and setters
        public Sprite Sprite
        {
            get { return _sprite; }
        }

        public Point2D Position
        {
            get { Point2D pos = new Point2D(); pos.X = X; pos.Y = Y; return pos; }
        }
        public float X
        {
            get
            {
                return _sprite.X;
            }
            set
            {
                _sprite.X = value;
                _position.X = _sprite.X;
            }
        }

        public float Y
        {
            get
            {
                return _sprite.Y;
            }
            set
            {
                _sprite.Y = value;
                _position.Y = _sprite.Y;
            }
        }

        //draw and update sprite
        public void DrawAndUpdate()
        {
            _sprite.Update();
            _sprite.Draw();
        }
    }
}
