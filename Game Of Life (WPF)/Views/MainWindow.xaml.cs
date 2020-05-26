using Game_Of_Life__WPF_.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game_Of_Life__WPF_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private Universe uni;
        private DispatcherTimer dTimer = new DispatcherTimer();
        private bool isGenerated = false;
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private string _l;
        private string _w;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsGenerated
        {
            get { return isGenerated; }
        }

        public string Rows
        {
            get { return _l; }
            set { 
                this._l = value;
                NotifyPropertyChanged("Rows");
                }
        }
        public string Columns
        {
            get { return _w; }
            set { 
                this._w = value;
                NotifyPropertyChanged("Columns");
                }
        
        }

        public MainWindow()
        {

            InitializeComponent();
            dTimer.Tick += new EventHandler(dTimer_Tick);
            dTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);

            Binding b = new Binding() { Source = this, Path = new PropertyPath("IsGenerated") };
            ToggleGenButton.SetBinding(Button.IsEnabledProperty, b);

            Binding bRow = new Binding() { Source = this, Path = new PropertyPath("Rows") };
            LifeGrid.SetBinding(UniformGrid.RowsProperty, bRow);
            txtLength.SetBinding(TextBox.TextProperty, bRow);

            Binding bCol = new Binding() { Source = this, Path = new PropertyPath("Columns") };
            LifeGrid.SetBinding(UniformGrid.RowsProperty, bCol);
            txtWidth.SetBinding(TextBox.TextProperty, bCol);

            //int row = 50;
            //int col = 100;

            //uni = new Universe(row, col, 50);

            //this.DataContext = uni;

            //LifeGrid.Rows = row;
            //LifeGrid.Columns = col;

        }

        public void initGrid()
        {

            LifeGrid.Children.Clear();

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           ToggleTimer();
        }

        private void GenerateGrid_Click(object sender, RoutedEventArgs e)
        {

            if (txtLength != null && txtWidth != null)
            {
                
                uni = new Universe(int.Parse(Rows), int.Parse(Columns));
                initGrid();
                drawGrid();
                Binding b = new Binding() { Source = uni, Path = new PropertyPath("Generation") };
                lblGeneration.SetBinding(Label.ContentProperty, b);
                isGenerated = true;
                NotifyPropertyChanged("IsGenerated");
            }

        }

        private void txtValidation(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
