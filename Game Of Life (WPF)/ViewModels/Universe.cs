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

        private System.Timers.Timer timer =
           new System.Timers.Timer(200);
        private int _length;
        private int _width;
        private Grid _next;
        private Grid _current;
        private Random _rand;
        private ObservableCollection<Cell> _life;
        private Cell[] _flatLife;
        private object tLock = new object();
        private object _lifeLock = new object();
        private int _genNo = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructors

        public Universe(int length, int width)
        {

            _length = length;
            _width = width;
            _next = new Grid(length, width);
            _current = new Grid(length, width);
            _rand = new Random();

            _life = new ObservableCollection<Cell>();
            //BindingOperations.EnableCollectionSynchronization(Life, _lifeLock);

            SeedLife();
            setupTimer();

        }

        public Universe(int length, int width, int seed)
        {

            _length = length;
            _width = width;
            _next = new Grid(length, width);
            _current = new Grid(length, width);
            _rand = new Random(seed);

            _life = new ObservableCollection<Cell>();
            //BindingOperations.EnableCollectionSynchronization(Life, _lifeLock);

            SeedLife();
            setupTimer();

        }

        #endregion

        #region Properties
        //public ObservableCollection<Cell> Life
        //{
        //    get { return _life; }
        //}

        public Cell[] Life
        {
            get { return _flatLife; }
        }

        public int Length
        {
            get { return _length; }
        }

        public int Width
        {
            get { return _width; }
        }
        public int Generation
        {
            get { return _genNo; }
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

        private void setupTimer()
        {
            timer.Elapsed += (sender2, e2) =>
            {
                CalculateGeneration();
            };
        }

        public void ToggleTimer()
        {

            if (timer.Enabled)
            {
                timer.Stop();
            } else
            {
                timer.Start();
            }

        }

        private void InitList()
        {

            _flatLife = new Cell[_length * _width];

            //for (int i = 0; i < _length * _width; i++)
            //{
            //    Life.Add(new Cell(false));
            //}
        }

        private void SeedLife()
        {

            InitList();

            for (int row = 0; row < _length; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    _current.SetCellState(row, col, _rand.NextDouble() > 0.5d ? true : false);
                }
            }

            PopulateLife();

        }

        public void CalculateGenerationBetweenRows(int rowStart, int rowEnd)
        {

            // all rows
            for (int row = rowStart; row < rowEnd; row++)
            {

                // all columns
                for (int col = 0; col < _width; col++)
                {

                    Cell cell = _current.GetCell(row, col);
                    cell.AliveNeighbours = _current.GetLiveNeighbours(row, col);

                    if ((cell.State && cell.AliveNeighbours >= 2 && cell.AliveNeighbours <= 3) || (!cell.State && cell.AliveNeighbours == 3))
                    {
                        _next.SetCellState(row, col, true); // cell alive this next generation
                    }

                }
            }

        }

        private void FlipGeneration()
        {

            _current = _next;
            _next = new Grid(_length, _width);

            PopulateLife();

        }

        private void PopulateLife()
        {

            int address = 0;   

            Cell[] tempLife = new Cell[_length * _width];

            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    //_life.Add(_current.GetCell(i, j));

                    lock (_lifeLock) {
                        //Life[address] = _current.GetCell(i, j);
                        //_flatLife[address] = _current.GetCell(i, j);
                        if (tempLife[address] == null)
                            tempLife[address] = _current.GetCell(i, j);
                        else
                            tempLife[address].State = _current.GetCellState(i, j);
                        
                        address++;
                    }

                }
            }

            _flatLife = tempLife;

            _genNo++;

            NotifyPropertyChanged("Life");
            NotifyPropertyChanged("Generation");
        }

        public void CalculateGeneration()
        {

            int n = _length / 2;

            //Thread t1 = new Thread(() => CalculateGenerationBetweenRows(0, n));
            //Thread t2 = new Thread(() => CalculateGenerationBetweenRows(n, _length));
            CalculateGenerationBetweenRows(0, _length); // debug without threads

           // t1.Start();
           // t2.Start();

           // t1.Join();
           // t2.Join();

            FlipGeneration();

        }

        #endregion

    }
}
