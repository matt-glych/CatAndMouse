using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{

    public class Grid
    {
        private List<Line> _gridLines = new List<Line>();

        private Color _lineColour = Color.DodgerBlue;
        private Color _defaultLineColour = Color.DodgerBlue;

        private int _cellSize = 100;
        private int _lines = 6;

        private float _x = 0;
        private float _y = 0;

        // constructor
        public Grid(int cellSize)
        {
            _cellSize = cellSize;
            _x = _cellSize;
            _y = _cellSize;

            CreateGrid(_lines);
        }

        // create grid
        private void CreateGrid(int numCells)
        {
            for (int i = 0; i < numCells + 1; ++i)
            {
                // horizontal
                Line line_h = new Line(_lineColour);
                line_h.X = _x;
                line_h.Y = _y + _cellSize * i + 1;
                line_h.XEnd = numCells * _cellSize;
                line_h.YEnd = _y + _cellSize * i + 1;
                _gridLines.Add(line_h);

                // vertical
                Line line_v = new Line(_lineColour);
                line_v.X = _x + _cellSize * i + 1;
                line_v.Y = _y;
                line_v.XEnd = _x + _cellSize * i + 1;
                line_v.YEnd = numCells * _cellSize;
                _gridLines.Add(line_v);
            }
        }

        // get and set grid line colour
        public Color LineColour
        {
            get { return _lineColour; }
            set { _lineColour = value; }
        }

        // set line colour to black 
        public void SetToBlack()
        {
            foreach(Line l in _gridLines)
            {
                l.Color = Color.Black;
            }
        }

        // draw the grid
        public void Draw()
        {
            foreach (Line line in _gridLines)
            {
                line.Draw();
            }
        }
    }
}
