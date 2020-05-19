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

        public void UpdateLiveNeighbours()
        {

            //int pW = Environment.ProcessorCount; //available worker threads (assume at least one)
            ////ThreadPool.GetAvailableThreads(out pW, out pP);
            //int n = _length; // number of rows per thread
            //int m = 0; // modulus, if needed
            //List<Thread> threads = new List<Thread>();
            //int currentStartRow = 0;
            //int currentEndRow = 0;

            //if (_length > pW)  // Check to ensure it's even worth multithreading
            //{
            //    n = _length / pW;
            //    m = _length % pW;
            //}
            

            //for (int i = 0; i < pW; i++)
            //{
            //    Thread t = new Thread(() => CheckRowLiveNeighbours(currentStartRow, i + 1 == pW ? currentEndRow += n + m : currentEndRow += n));
            //    currentStartRow += n;
            //    t.Start();
            //    threads.Add(t);
            //}

            //// Make a new thread per row. We're going to need:
            //// A list of threads


            int n = _length / 2;

            Thread t1 = new Thread(() => CheckRowLiveNeighbours(0, n));
            Thread t2 = new Thread(() => CheckRowLiveNeighbours(n, _length));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            //for (int i = 0; i < _length; i++)
            //{

            //    Thread t = new Thread(() => CheckRowLiveNeighbours(i - 1));
            //    t.Start();

            //    threads.Add(t);
            //}

            //foreach (Thread t in threads)
            //{
            //    t.Join();
            //}

            //slow method for debuggin

            //for (int i = 0; i < _length; i++)
            //{
            //    CheckRowLiveNeighbours(i);
            //}

        }

        public void CheckRowLiveNeighbours(int rowStart, int rowEnd)
        {
            for (int row = rowStart; row < rowEnd; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    SetLiveNeighbours(row, col);
                }
            }

        }

        public void SetLiveNeighbours(int row, int col)
        {

            lock (tLock) { 

                int count = 0;
                int rowOffset;
                int colOffset;

                //MessageBox.Show("Evaluating cell: " + row.ToString() + "," + col.ToString());

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

                        //if (count >= 4) break;
                    
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

                //MessageBox.Show(count.ToString());
                _cells[row,col].AliveNeighbours = count;

            }
        }
    

        public int GetLiveNeighbours(int row, int col)
        {

            int count = 0;
            int rowOffset;
            int colOffset;

           // MessageBox.Show("Evaluating cell: " + row.ToString() + "," + col.ToString());

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
                        //MessageBox.Show("Cell " + rowOffset.ToString() + "," + colOffset.ToString() + " is alive.");
                        
                        count++;
                    }
                    else
                    {
                        //MessageBox.Show("Cell " + rowOffset.ToString() + "," + colOffset.ToString() + " is dead.");
                    }
                }
            }

            //MessageBox.Show(count.ToString());
            return count;
        }

    }
}
