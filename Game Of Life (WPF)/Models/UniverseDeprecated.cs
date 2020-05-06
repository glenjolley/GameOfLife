using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Game_Of_Life__WPF_.Models
{
    public class UniverseDeprecated
    {
        private Random _rand;
        private int _length;
        private Cell[,] _currentGeneration;
        private Cell[,] _nextGeneration;

        public UniverseDeprecated(int length, int seed)
        {
            _length = length;
            _currentGeneration = new Cell[length, length];
            _rand = new Random(seed);
        }

        public void CalculateLife()
        {
            for (int row = 0; row < _length; row++)
            {
                for (int col = 0; col < _length; col++)
                {
                    _currentGeneration[row, col] = new Cell(_rand.NextDouble() > 0.5 ? true : false);
                }
            }
        }

        public void DoGeneration()
        {

            _nextGeneration = new Cell[_length, _length];

            // all rows
            for (int row = 0; row < _length; row++)
            {

                // all columns
                for (int col = 0; col < _length; col++)
                {

                    int count = CheckCells(row, col);
                    Cell cell = _currentGeneration[row, col];

                    if ((cell.State && count >= 2 && count <= 3) || (!cell.State && count == 3))
                    {
                        _nextGeneration[row, col] = new Cell(true); // cell alive this next generation
                    }
                    else
                    {
                        _nextGeneration[row, col] = new Cell(false); // cell is dead this generation
                    }
                }
            }

            _currentGeneration = _nextGeneration;

        }

        public void PrintCurrentGeneration()
        {

            Console.Clear();

            for (int row = 0; row < _length; row++)
            {
                for (int col = 0; col < _length; col++)
                {
                    Console.Write(_currentGeneration[row, col].State ? "O" : " ");
                }
                Console.WriteLine();
            }
        }

        private int CheckCells(int row, int col)
        {
            int count = 0;
            int rowOffset;
            int colOffset;

            for (int i = row - 1; i <= row + 1; i++)
            {

                if (i < 0 || i >= _length)
                {
                    rowOffset = i < 0 ? _length - 1 : 0;
                }
                else
                {
                    rowOffset = i;
                }

                for (int j = col - 1; j <= col + 1; j++)
                {

                    if (j < 0 || j >= _length)
                    {
                        colOffset = j < 0 ? _length - 1 : 0;
                    }
                    else
                    {
                        colOffset = j;
                    }

                    if (row == j && col == i) continue; //skip the originating cell

                    if (_currentGeneration[rowOffset, colOffset].State)

                    {
                        count++;
                    }

                }
            }
            return count;
        }

        public Boolean GetCellState(int row, int column)
        {
            return _currentGeneration[row, column].State;
        }
        
        public Cell[,] Cells
        {
            get { return _currentGeneration; }
        }

    }
}
