using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersConsole.Interfaces;
using System.Threading;
using CheckersGame;
namespace CheckersConsole
{
    public class CheckersClientGame
    {
        private int[][] _gameState;
        private int _playerId;

        ICheckersRepository _repo;
        public CheckersClientGame(ICheckersRepository checkersAPIRepository)
        {
            _repo = checkersAPIRepository;
        }
        private string GetGameString(int[][] gameState)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < gameState.Length; ++i)
            {
                for (int j = 0; j < gameState[i].Length; ++j)
                {
                    stringBuilder.Append(gameState[i][j]);
                    stringBuilder.Append(' ');
                }
                stringBuilder.Append('\n');
            }
            return stringBuilder.ToString();
        }
        public async Task StartAsync()
        {
            try
            {
                _gameState = await _repo.GetGameStateAsync();
                _playerId = await _repo.GetPlayerIdAsync();
                int winner = await _repo.CheckForWinnerAsync();
                int fromX, fromY, toX, toY;
                while (true)
                {
                    while (!await _repo.IsMyTurnAsync(_playerId))
                    {
                        Console.WriteLine("waiting...");
                        Thread.Sleep(1000);
                    }
                    winner = await _repo.CheckForWinnerAsync();
                    if (winner != 0)
                    {
                        Console.WriteLine("Player {0} won", winner);
                        break;
                    }
                    else
                    {
                        Move move;
                        do
                        {
                            _gameState = await _repo.GetGameStateAsync();
                            Console.WriteLine(GetGameString(_gameState));
                            Console.WriteLine("Enter in fromRow");
                            int.TryParse(Console.ReadLine(), out fromX);
                            Console.WriteLine("Enter in fromCol");
                            int.TryParse(Console.ReadLine(), out fromY);
                            Console.WriteLine("Enter in toRow");
                            int.TryParse(Console.ReadLine(), out toX);
                            Console.WriteLine("Enter in toCol");
                            int.TryParse(Console.ReadLine(), out toY);
                            move = await _repo.PlayMoveAsync(fromX -1, fromY -1, toX -1, toY -1);
                        }
                        while (!move.ValidMove);
                        _gameState = await _repo.GetGameStateAsync();
                        Console.WriteLine(GetGameString(_gameState));
                        while (move.AvailableMoves.Any())
                        {
                            Console.WriteLine("You can jump again");
                            Console.WriteLine("Enter in fromRow");
                            int.TryParse(Console.ReadLine(), out fromX);
                            Console.WriteLine("Enter in fromCol");
                            int.TryParse(Console.ReadLine(), out fromY);
                            Console.WriteLine("Enter in toRow");
                            int.TryParse(Console.ReadLine(), out toX);
                            Console.WriteLine("Enter in toCol");
                            int.TryParse(Console.ReadLine(), out toY);
                            move = await _repo.PlayMoveAsync(fromX - 1, fromY - 1, toX - 1, toY - 1);
                        }
                        _gameState = await _repo.GetGameStateAsync();
                        Console.WriteLine(GetGameString(_gameState));
                    }
                }
                Console.Read();
            }
            catch (Exception )
            {
            }
            finally
            {
                await _repo.ResetGameAsync();
            }
        }
    }
}
