using Game_Of_Life__WPF_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;

namespace Game_Of_Life__WPF_.ViewModels
{
    class Universe : INotifyPropertyChanged
    {

        private int _length;
        private int _width;
        private Grid _next;
        private Grid _current;
        private Random _rand;

        public event PropertyChangedEventHandler PropertyChanged;

        public Universe(int length, int width, int seed)
        {

            _length = length;
            _width = width;
            _next = new Grid(length, width);
            _current = new Grid(length, width);
            _rand = new Random(seed);

            SeedLife();

        }

        public Grid Grid
        {
            get { return _current; }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SeedLife()
        {
            for (int row = 0; row < _length; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    _current.SetCellState(row, col, _rand.NextDouble() > 0.5 ? true : false);
                }
            }

        }

        public void CalculateGeneration()
        {

            // all rows
            for (int row = 0; row < _length; row++)
            {

                // all columns
                for (int col = 0; col < _width; col++)
                {

                    int count = _current.GetLiveNeighbours(row, col);
                    bool state = _current.GetCellState(row, col);

                    if ((state && count >= 2 && count <= 3) || (!state && count == 3))
                    {
                        _next.SetCellState(row, col, true); // cell alive this next generation
                    }
                    else
                    {
                        _next.SetCellState(row, col, true); // cell is dead this generation
                    }
                }
            }

            FlipGeneration();

        }

        private void FlipGeneration()
        {

            _current = _next;
            NotifyPropertyChanged("Grid");

        }

    }
}
