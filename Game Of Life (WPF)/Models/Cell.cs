using System;
using System.Collections.Generic;
using System.Text;

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
            set { _isAlive = State; }
        }

    }
}
