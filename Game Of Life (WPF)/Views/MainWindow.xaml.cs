using Game_Of_Life__WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game_Of_Life__WPF_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Universe uni;
        private DispatcherTimer dTimer = new DispatcherTimer();

        public MainWindow()
        {

            InitializeComponent();
            dTimer.Tick += new EventHandler(dTimer_Tick);
            dTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);

            int row = 50;
            int col = 100;
            
            uni = new Universe(row, col, 50);

            this.DataContext = uni;

            LifeGrid.Rows = row;
            LifeGrid.Columns = col;

            initGrid();
            drawGrid();

        }

        public void initGrid()
        {

            for (int i = 0; i < (uni.Length * uni.Width); i++)
            {

                Rectangle r = new Rectangle() { Fill = Brushes.White };

                LifeGrid.Children.Add(r);

            }

        }

        private void dTimer_Tick(object sender, EventArgs e)
        {
            Step();
        }

        public void drawGrid()
        {

            for (int i = 0; i < (uni.Length * uni.Width); i++)
            {

                this.Dispatcher.Invoke(() =>
                {

                    Rectangle r = (Rectangle)LifeGrid.Children[i];

                    if (r.Fill != uni.Life[i].Brush)
                    {
                        r.Fill = (Brush)uni.Life[i].Brush;
                    }

                });

            }

        }

        private void Step()
        {
            uni.CalculateGeneration();
            drawGrid();           
        }

        public void ToggleTimer()
        {

            if (dTimer.IsEnabled)
            {
                dTimer.Stop();
            }
            else
            {
                dTimer.Start();
            }

        }

        public void DoGenerations(int n)
        {
            for (int i = 0; i < n; i++)
            {                
                uni.CalculateGeneration();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           ToggleTimer();
        }
    }
}
