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

namespace Game_Of_Life__WPF_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Universe uni;

        public MainWindow()
        {
            
            int row = 25;
            int col = 50;
            
            uni = new Universe(row, col, 50);

            this.DataContext = uni;

            InitializeComponent();

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
            uni.ToggleTimer();  
        }
    }
}
