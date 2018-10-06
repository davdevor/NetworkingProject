using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Domain.Interfaces;
using Checkers.Services.Interfaces;
using Checkers.Domain.Objects;

namespace Checkers.Infrastructure.Data
{
    public class CheckersRepository : ICheckersRepository
    {
        private readonly IHttpDeserializerService _httpDeserializer;
        private readonly IHttpClient _httpClient;
        private readonly string _baseUrl;
        public CheckersRepository(string baseUrl, IHttpDeserializerService httpDeserializer, IHttpClient httpClient)
        {
            _baseUrl = baseUrl;
            _httpDeserializer = httpDeserializer;
            _httpClient = httpClient;
        }
        /// <summary>
        /// checks to see if it is the players turn
        /// </summary>
        /// <param name="playerId">the player to check if it is their turn</param>
        /// <returns>true if it is the players turn</returns>
        public async Task<bool> IsMyTurnAsync(int playerId)
        {
            string url = _baseUrl + "IsTurn?playerid=" + playerId;
            return await _httpDeserializer.GetObjectFromRequestAsync<bool>(url);
        }
        /// <summary>
        /// plays a move for a player
        /// </summary>
        /// <param name="fromX">x location to move from</param>
        /// <param name="fromY">y location to move from</param>
        /// <param name="toX">x location to move to</param>
        /// <param name="toY">y location to move to</param>
        /// <returns>next possible moves if any</returns>
        public async Task<Move> PlayMoveAsync(int fromX, int fromY, int toX, int toY)
        {
            string url = _baseUrl + string.Format("PlayMove?fromRow={0}&fromCol={1}&toRow={2}&toCol={3}", fromX, fromY, toX, toY);
            return await _httpDeserializer.GetObjectFromRequestAsync<Move>(url);
        }
        /// <summary>
        /// get a player id to assign to yourself
        /// </summary>
        /// <returns>playerid</returns>
        public async Task<int> GetPlayerIdAsync()
        {
            string url = _baseUrl + "GetPlayerId";
            return await _httpDeserializer.GetObjectFromRequestAsync<int>(url);
        }
        /// <summary>
        /// checks for a winner
        /// </summary>
        /// <returns>0 if none otherwise player id</returns>
        public async Task<int> CheckForWinnerAsync()
        {
            string url = _baseUrl + "CheckWinner";
            return await _httpDeserializer.GetObjectFromRequestAsync<int>(url);
        }
        /// <summary>
        /// gets the state of the checkers board
        /// </summary>
        /// <returns></returns>
        public async Task<int[][]> GetGameStateAsync()
        {
            string url = _baseUrl + "GetGameState";
            return await _httpDeserializer.GetObjectFromRequestAsync<int[][]>(url);
        }
        /// <summary>
        /// resets the entire game
        /// </summary>
        /// <returns></returns>
        public async Task ResetGameAsync()
        {
            string url = _baseUrl + "ResetGame";
            await _httpClient.MakeRequestAsync(url);
        }

    }
}
