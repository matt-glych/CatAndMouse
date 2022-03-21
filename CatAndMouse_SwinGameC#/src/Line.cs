using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    public class Line : Shape
    {
        private float _xEnd, _yEnd;
        private Color previousColor;

        //default constructor
        public Line() : this(Color.Red)
        {

        }
        //constructor
        public Line(Color color) : base(color)
        {
            Color = color;
        }

        // getters & setters
        public float XEnd
        {
            get
            {
                return _xEnd;
            }
            set
            {
                _xEnd = value;
            }
        }

        public float YEnd
        {
            get
            {
                return _yEnd;
            }
            set
            {
                _yEnd = value;
            }
        }
        // draw
        public override void Draw()
        {
            SwinGame.DrawLine(Color, X, Y , _xEnd, _yEnd);
        }
        // disappear
        public void Disappear()
        {
            previousColor = this.Color;
            this.Color = Color.Transparent;
        }
        // re-appear
        public void Reappear()
        {
            if (previousColor != null)
            {
                this.Color = previousColor;
            }
        }
    }
}
