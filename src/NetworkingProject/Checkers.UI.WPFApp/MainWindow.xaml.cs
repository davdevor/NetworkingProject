﻿using System;
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
using Checkers.Domain.Interfaces;
using Checkers.Domain.Objects;

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
        private Queue<int> _moves = new Queue<int>();

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(ICheckersRepository checkersRepository)
        {
            _repository = checkersRepository;
            InitializeComponent();
        }

        private void UpdateGrid()
        {
            UIElementCollection gridChilds = this.CheckersGrid.Children;
            for (int i = 0; i < _gameState.Length; ++i)
            {
                for (int j = 0; j < _gameState.Length; ++j)
                {
                    Button b = new Button()
                    {
                        MinHeight = 40,
                        MinWidth = 40
                    };
                    b.Click += ButtonGrid_Click;
                    gridChilds.Add(b);
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    if (_gameState[i][j] == 1)
                    {
                        b.Content = new Ellipse() { Width = 30, Height = 30, Fill = Brushes.Red };

                    }
                    else if (_gameState[i][j] == 2)
                    {
                        b.Content = new Ellipse() { Width = 30, Height = 30, Fill = Brushes.Black };

                    }
                }
            }
        }
        private async Task WaitForPlayer()
        {
            CheckersGrid.IsEnabled = false;
            StatusLabel.Content = "Waiting on other player";
            await Task.Run(async () =>
            {
                while (!await _repository.IsMyTurnAsync(_playerId))
                {
                    Thread.Sleep(1000);
                }
            });
            CheckersGrid.IsEnabled = true;
            StatusLabel.Content = "Your Turn";
        }
        private async void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).Visibility = Visibility.Hidden;
            _playerId = await _repository.GetPlayerIdAsync();
            if(_playerId == 1)
            {
                PlayerLabel.Content = "You are Red";
            }
            else
            {
                PlayerLabel.Content = "You are black";
            }
            _gameState = await _repository.GetGameStateAsync();
            UpdateGrid();
            CheckersGrid.IsEnabled = false;
            await WaitForPlayer();
            _gameState = await _repository.GetGameStateAsync();
            UpdateGrid();
            CheckersGrid.IsEnabled = true;
        }

        private async void ButtonGrid_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int row = Grid.GetRow(b);
            int col = Grid.GetColumn(b);
            _moves.Enqueue(row);
            _moves.Enqueue(col);
            if(_moves.Count() == 4)
            {
                await PlayMove();
            }
        }

        private async Task PlayMove()
        {
            int row = _moves.Dequeue();
            int col = _moves.Dequeue();
            if (_gameState[row][col] != _playerId)
            {
                StatusLabel.Content = "Invalid Move";
                return;
            }
            Move move = await _repository.PlayMoveAsync(
                row,
                col,
                _moves.Dequeue(),
                _moves.Dequeue()
                );
            string text = "Invlaid move";
            bool waitForPlayer = false;
            if (move.ValidMove)
            {
                _gameState = await _repository.GetGameStateAsync();
                UpdateGrid();
                if (move.AvailableMoves.Any())
                {
                    text = "You can jump again";
                }
                else
                {
                    waitForPlayer = true;
                }
            }
            if (waitForPlayer)
            {
                await WaitForPlayer();
                _gameState = await _repository.GetGameStateAsync();
                UpdateGrid();
                await CheckForWinner();
            }
            else
            {
                StatusLabel.Content = text;
            }
        }
        private async Task CheckForWinner()
        {
            int winner = await _repository.CheckForWinnerAsync();
            if(winner == 0)
            {
                return;
            }
            else if(winner == _playerId)
            {
                StatusLabel.Content = "You won";
            }
            else
            {
                StatusLabel.Content = "You lost";
            }
            CheckersGrid.IsEnabled = false;
        }
    }
}
