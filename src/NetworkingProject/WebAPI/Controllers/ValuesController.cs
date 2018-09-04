using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConnectFour;
namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        private static ConnectFourGame _gameObject = new ConnectFourGame();
        private static int _players = 0;
        private int _winner = 0;
        private bool _won = false;
        private bool _busy = false;
        public int GetPlayerId()
        {
            return ++_players;
        }

        public void PlayMove(int col, int playerId)
        {

            _busy = true;
            _gameObject.PlayMove(col, playerId);
            if (_gameObject.CheckForWinState(playerId))
            {
                _winner = playerId;
                _won = true;
            }
            _busy = false;

        }
        public ConnectFourGame GetGameState()
        {
            return _gameObject;
        }
        public bool IsTurn(int playerId)
        {
            return _busy;
        }
    }
}
