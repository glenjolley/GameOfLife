using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;

namespace Game_Of_Life__WPF_.Models
{
    class Grid
    {

        private object tLock = new object(); 
        private int _length;
        private int _width;
        private Cell[,] _cells;

        public Grid(int length, int width)
        {
            _length = length;
            _width = width;
            _cells = new Cell[length, width];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _cells[i, j] = new Cell(false);
                }
            }

        }

        public Cell GetCell(int row, int col)
        {
            return _cells[row, col];
        }

        public bool GetCellState(int row, int col)
        {
            return _cells[row, col].State;
        }

        public void SetCellState(int row, int col, bool state)
        {
            _cells[row, col].State = state;
        }

        public int GetLiveNeighbours(int row, int col)
        {

            int count = 0;
            int rowOffset;
            int colOffset;

           // MessageBox.Show("Evaluating cell: " + row.ToString() + "," + col.ToString());

            for (int i = row - 1; i <= row + 1; i++)
            {
                if (count >= 4) break;

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
                    if (count >= 4) break;
                    
                    if (j < 0 || j >= _width)
                    {
                        colOffset = j < 0 ? _width - 1 : 0;
                    }
                    else
                    {
                        colOffset = j;
                    }

                    if (row == i && col == j) continue; //skip the originating cell

                    if (this._cells[rowOffset, colOffset].State)
                    {                      
                        count++;
                    }
                }
            }
            return count;
        }

    }
}
