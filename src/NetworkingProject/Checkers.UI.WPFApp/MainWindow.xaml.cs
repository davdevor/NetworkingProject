using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Checkers.Domain.Interfaces;
using System.Net.Http;

namespace Checkers.UI.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[][] _gameState;
        private int _playerId;
        private ICheckersRepository _repository;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(ICheckersRepository checkersRepository)
        {
            _repository = checkersRepository;
            InitializeComponent();
        }

        private async void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            _playerId = await _repository.GetPlayerIdAsync();
            _gameState = await _repository.GetGameStateAsync();
            UIElementCollection gridChilds = this.CheckersGrid.Children;
            for (int i = 0; i < _gameState.Length; ++i)
            {
                for (int j = 0; j < _gameState.Length; ++j)
                {
                    Button b = new Button();
                    if (_gameState[i][j] == 1)
                    {
                        b.Content = "R";
                    }
                    else if (_gameState[i][j] == 2)
                    {
                        b.Content = "B";

                    }
                    gridChilds.Add(b);
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                }
            }
            (sender as Button).Visibility = Visibility.Hidden;
        }
    }
}
