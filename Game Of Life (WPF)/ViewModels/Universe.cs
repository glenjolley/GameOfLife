using Game_Of_Life__WPF_.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Data;

namespace Game_Of_Life__WPF_.ViewModels
{
    class Universe : INotifyPropertyChanged
    {

        /// <summary>
        /// Creates a Game of Life universe accepting Length and Width of the grid
        /// </summary>

        private int _length;
        private int _width;
        private Grid _next;
        private Grid _current;
        private Random _rand;
        private ObservableCollection<Cell> _life = new ObservableCollection<Cell>();
        

        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructors

        public Universe(int length, int width)
        {

            _length = length;
            _width = width;
            _next = new Grid(length, width);
            _current = new Grid(length, width);
            _rand = new Random();

            SeedLife();

        }

        public Universe(int length, int width, int seed)
        {

            _length = length;
            _width = width;
            _next = new Grid(length, width);
            _current = new Grid(length, width);
            _rand = new Random(seed);

            SeedLife();

        }

        #endregion

        #region Properties
        public ObservableCollection<Cell> Life
        {
            get { return _life; }
        }

        public int Length
        {
            get { return _length; }
        }

        public int Width
        {
            get { return _width; }
        }

        #endregion

        #region InterfaceImplementation

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Implementation

        private void SeedLife()
        {
            for (int row = 0; row < _length; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    _current.SetCellState(row, col, _rand.NextDouble() > 0.5d ? true : false);
                }
            }

            _current.UpdateLiveNeighbours();
            PopulateLife();

        }

        public void CalculateGeneration()
        {

            // all rows
            for (int row = 0; row < _length; row++)
            {

                // all columns
                for (int col = 0; col < _width; col++)
                {

                    Cell cell = _current.GetCell(row, col);

                    if ((cell.State && cell.AliveNeighbours >= 2 && cell.AliveNeighbours <= 3) || (!cell.State && cell.AliveNeighbours == 3))
                    {
                        _next.SetCellState(row, col, true); // cell alive this next generation
                    }
                    else
                    {
                        _next.SetCellState(row, col, false); // cell is dead this generation
                    }
                }
            }

            _next.UpdateLiveNeighbours();
            FlipGeneration();

        }

        //public void CalculateGeneration()
        //{

        //    // all rows
        //    for (int row = 0; row < _length; row++)
        //    {

        //        // all columns
        //        for (int col = 0; col < _width; col++)
        //        {

        //            int count = _current.GetLiveNeighbours(row, col);
        //            bool state = _current.GetCellState(row,col);

        //            if ((state && count >= 2 && count <= 3) || (!state && count == 3))
        //            {
        //                _next.SetCellState(row, col, true); // cell alive this next generation
        //            }
        //            else
        //            {
        //                _next.SetCellState(row, col, false); // cell is dead this generation
        //            }
        //        }
        //    }

        //    FlipGeneration();

        //}

        private void FlipGeneration()
        {

            _current = _next;
            _next = new Grid(_length, _width);

            PopulateLife();

        }

        private void PopulateLife()
        {

            _life.Clear();    

            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    _life.Add(_current.GetCell(i, j));
                }
            }

            NotifyPropertyChanged("Life");

        }

        #endregion

    }
}
