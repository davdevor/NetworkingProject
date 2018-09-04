using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFourClient.Interfaces;

namespace ConnectFourClient
{
    class ConnectFourAPIRepository : IConnectFourAPIRepository
    {
        private readonly IHttpDeserializer _httpDeserializer;
        private readonly IHttpClient _httpClient;
        private readonly string  _baseUrl;
        public ConnectFourAPIRepository(string baseUrl, IHttpDeserializer httpDeserializer, IHttpClient httpClient)
        {
            _baseUrl = baseUrl;
            _httpDeserializer = httpDeserializer;
            _httpClient = httpClient;
        }

        public async Task<bool> IsMyTurnAsync(int playerId)
        {
            string url = _baseUrl + "IsTurn?playerid=" + playerId;
            return await _httpDeserializer.GetObjectAsync<bool>(url);
        }

        public async Task PlayMoveAsync(int col, int playerId)
        {
            string url = _baseUrl + string.Format("PlayMove?col={0}&playerid={1}", col, playerId);
            await _httpClient.MakeRequestAsync(url);
        }

        public async Task<int> GetPlayerIdAsync()
        {
            string url = _baseUrl + "GetPlayerId";
            return await _httpDeserializer.GetObjectAsync<int>(url);
        }

        public async Task<int> CheckForWinnerAsync()
        {
            string url = _baseUrl + "CheckWinner";
            return await _httpDeserializer.GetObjectAsync<int>(url);
        }

        public async Task<int[][]> GetGameStateAsync()
        {
            string url = _baseUrl + "GetGameState";
            return await _httpDeserializer.GetObjectAsync<int[][]>(url);
        }

        public async Task ResetGameAsync()
        {
            string url = _baseUrl + "ResetGame";
            await _httpClient.MakeRequestAsync(url);
        }

    }
}
