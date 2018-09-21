using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersConsole.Interfaces;
using CheckersGame;
namespace CheckersConsole
{
    public class CheckersRepository : ICheckersRepository
    {
        private readonly IHttpDeserializer _httpDeserializer;
        private readonly IHttpClient _httpClient;
        private readonly string _baseUrl;
        public CheckersRepository(string baseUrl, IHttpDeserializer httpDeserializer, IHttpClient httpClient)
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

        public async Task<Move> PlayMoveAsync(int fromX, int fromY, int toX, int toY)
        {
            string url = _baseUrl + string.Format("PlayMove?fromX={0}&fromY={1}&toX={2}&toY={3}",fromX,fromY,toX,toY);
            return await _httpDeserializer.GetObjectAsync<Move>(url);
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
