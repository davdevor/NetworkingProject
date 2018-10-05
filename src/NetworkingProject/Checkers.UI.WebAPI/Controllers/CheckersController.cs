using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Checkers.Domain.Objects;

namespace Checkers.UI.WebAPI.Controllers
{
    public class CheckersController : ApiController
    {
        private static CheckersGame _gameObject = new CheckersGame();
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
        public Move PlayMove(int fromRow, int fromCol, int toRow, int toCol)
        {

            Move move = _gameObject.Move(fromRow, fromCol, toRow, toCol);
            if (move.ValidMove)
            {
                int winner = _gameObject.CheckForWinState();
                if (winner != 0)
                {
                    _winner = winner;
                }
                if (!move.AvailableMoves.Any())
                {
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
            }
            return move;
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
            _gameObject = new CheckersGame();
            return true;
        }
    }
}
