﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFourClient.Interfaces;
using System.Threading;
namespace ConnectFourClient
{
    public class ConnectFourClientGame 
    {
        private int[][] _gameState;
        private int _playerId;

        IConnectFourAPIRepository _repo;
        public ConnectFourClientGame(IConnectFourAPIRepository connectFourAPIRepository)
        {
            _repo = connectFourAPIRepository;
        }
        public async Task StartAsync()
        {
            try
            {
                _gameState = await _repo.GetGameStateAsync();
                _playerId = await _repo.GetPlayerIdAsync();
                int winner = await _repo.CheckForWinnerAsync();
                int col = 0;
                while (true)
                {
                    while (!await _repo.IsMyTurnAsync(_playerId))
                    {
                        Console.WriteLine("waiting...");
                        Thread.Sleep(5000);
                    }
                    winner = await _repo.CheckForWinnerAsync();
                    if (winner != 0)
                    {
                        Console.WriteLine("Player {0} won", winner);
                        break;
                    }
                    else
                    {
                        _gameState = await _repo.GetGameStateAsync();
                        Console.WriteLine("Enter in col");
                        int.TryParse(Console.ReadLine(), out col);
                        await _repo.PlayMoveAsync(col, _playerId);
                    }
                }
                Console.Read();
            }
            catch(Exception e)
            {
                await _repo.ResetGameAsync();
                throw e;
            }
            
        }
    }
}
