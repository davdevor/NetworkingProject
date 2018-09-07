using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConnectFour;
namespace WebAPI.Controllers
{
    public class ConnectFourController : ApiController
    {
        private static ConnectFourGame _gameObject = new ConnectFourGame();
        private static int _players = 0;
        private static int _winner = 0;
        private static int _currentPlayer = 1;
        private static readonly object _lock = new object();

        [AcceptVerbs("Get")]
        public int GetPlayerId()
        {
            lock (_lock)
            {
                return ++_players;
            }
        }

        [AcceptVerbs("Get")]
        public bool PlayMove(int col, int playerId)
        {
            
            bool validMove = _gameObject.PlayMove(col, playerId);
            if (validMove)
            {
                if (_gameObject.CheckForWinState(playerId))
                {
                    _winner = playerId;
                }
                lock (_lock)
                {
                    if (_currentPlayer == 1)
                    {
                        _currentPlayer = 2;
                    }
                    else
                    {
                        _currentPlayer = 1;
                    }
                }
            }
            return validMove;
        }

        [AcceptVerbs("Get")]
        public int[,] GetGameState()
        {
            return _gameObject.GetBoard();
        }

        [AcceptVerbs("Get")]
        public int CheckWinner()
        {
            return _winner;
        }

        [AcceptVerbs("Get")]
        public bool IsTurn(int playerID)
        {
            lock (_lock)
            {
                return _currentPlayer == playerID;
            }
        }

        [AcceptVerbs("Get")]
        public bool ResetGame()
        {
            _players = 0;
            _winner = 0;
            _currentPlayer = 1;
            _gameObject = new ConnectFourGame();
            return true;
        }
    }
}
