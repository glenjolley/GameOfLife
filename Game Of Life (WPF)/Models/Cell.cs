using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Game_Of_Life__WPF_.Models
{

    public class Cell
    {
        private bool _isAlive;

        public Cell(bool initState)
        {
            _isAlive = initState;
        }

        public bool State
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        public object Brush
        {
            get { return _isAlive ? Brushes.Black : Brushes.White; }
        }

        public int AliveNeighbours { get; set; }

    }
}
